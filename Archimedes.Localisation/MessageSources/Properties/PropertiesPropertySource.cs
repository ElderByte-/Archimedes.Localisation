using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Archimedes.Localisation.Utils;
using log4net;

namespace Archimedes.Localisation.MessageSources.Properties
{
    public class PropertiesPropertySource
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Regex KeyValueParser = new Regex("(.*?)=(.*)");
        private readonly string _rawPropertiesData;

        /// <summary>
        /// Creates a new PropertiesPropertySource with the raw properties data.
        /// It will be parsed on load into a property store.
        /// </summary>
        /// <param name="rawPropertiesData"></param>
        public PropertiesPropertySource(string rawPropertiesData)
        {
            if (rawPropertiesData == null) throw new ArgumentNullException("rawPropertiesData");

            _rawPropertiesData = rawPropertiesData;
        }

        [DebuggerStepThrough]
        public IDictionary<string, string> Load()
        {
            var properties = new Dictionary<string, string>();

            try
            {
                var tmp = Parse(_rawPropertiesData);
                DictionaryUtil.MergeLeft(properties, tmp);
            }
            catch (FormatException e)
            {
                throw new FormatException("Failed to parse properties data!", e);
            }

            return properties;
        }

        #region Private methods

        private Dictionary<string, string> Parse(string rawpropertyData)
        {
            string[] lines = Regex.Split(rawpropertyData, @"\r?\n|\r");
            return Parse(lines);
        }

        private Dictionary<string, string> Parse(string[] propertyLines)
        {
            var properties = new Dictionary<string, string>();

            Log.Info(string.Format("Loading {0} raw property lines.", propertyLines.Length));

            foreach (var propertyLine in propertyLines)
            {
                var parsed = ParseLine(propertyLine);

                if (parsed != null)
                {
                    if (!properties.ContainsKey(parsed.Item1))
                    {
                        properties.Add(parsed.Item1, parsed.Item2);
                    }
                    else
                    {
                        properties[parsed.Item1] = parsed.Item2;
                        Log.Warn("Property key '" + parsed.Item1 + "' is defined multiple times. Value got overriden.");
                    }
                }
            }
            return properties;
        }

        private Tuple<string, string> ParseLine(string propertyLine)
        {
            propertyLine = propertyLine.Trim();

            if (string.IsNullOrEmpty(propertyLine) || propertyLine.StartsWith("#")) return null;


            if (KeyValueParser.IsMatch(propertyLine))
            {
                var key = KeyValueParser.Match(propertyLine).Groups[1].Value;
                var value = KeyValueParser.Match(propertyLine).Groups[2].Value;

                return new Tuple<string, string>(key, value);
            }
            else
            {
                throw new FormatException(string.Format("Can not parse properties line, illegal format. '{0}'", propertyLine));
            }
        }

        #endregion
    }
}
