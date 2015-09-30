using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Archimedes.Localisation.MessageSources.Properties
{
    public class FilePropertiesPropertySource
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string _filepath;

        public FilePropertiesPropertySource(string filepath)
        {
            _filepath = filepath;
        }

        public IDictionary<string, string> Load()
        {
            if (File.Exists(_filepath))
            {
                Log.Info(string.Format("Loading properties from {0}", _filepath));

                var rawProperties = File.ReadAllText(_filepath);
                var src = new PropertiesPropertySource(rawProperties);
                return src.Load();
            }
            else
            {
                Log.Warn(string.Format("Properties file not found ({0})!", _filepath));
            }

            return new Dictionary<string, string>();
        }
    }
}
