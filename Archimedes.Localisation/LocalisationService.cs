﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Localisation.MessageSources;
using Archimedes.Localisation.Utils;
using log4net;

namespace Archimedes.Localisation
{
    /// <summary>
    /// Manages the localisation of the current application
    /// </summary>
    public class LocalisationService : ILocalisationService
    {
        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        readonly IDictionary<CultureInfo, bool> _hirarchyLoadedFlags = new Dictionary<CultureInfo, bool>();
        private readonly TranslationCache _cache = new TranslationCache();
        private readonly List<IMessageSource> _messageSources = new List<IMessageSource>();

        #endregion

        public LocalisationService() :
            this(new FilePropertiesMessageSource(AppUtil.ApplicaitonBinaryFolder + @"\Resources\messages"))
        {
        }

        public LocalisationService(IMessageSource messageSource)
        {
            MessageSources.Add(messageSource);
        }


        #region Public API

        public string GetTranslation(string key)
        {
            return GetTranslation(CultureInfo.CurrentUICulture, key);
        }

        public string GetTranslation(string key, params object[] args)
        {
            return GetTranslation(CultureInfo.CurrentUICulture, key, args);
        }

        public string GetTranslation(CultureInfo culture, string key, params object[] args)
        {
            var translation = GetTranslation(culture, key);
            try
            {
                return string.Format(translation, args);
            }
            catch (FormatException e)
            {
                Log.Error(string.Format("Failed to format translation for culture: '{0}' key: '{1}', value: '{2}' with {3} parameters!",
                    culture, key, translation, args.Length)
                    , e);

                return translation;
            }
        }

        public string GetTranslation(CultureInfo culture, string key)
        {
            EnsureCultureHirarchyLoaded(culture);

            var translation = _cache.Find(culture, key);

            if (translation != null)
            {
                return translation;
            }

            if (!CultureInfo.InvariantCulture.Equals(culture))
            {
                Log.Debug(string.Format("Could not find translation for [{0}] in Culture {1}. Falling back to culture {2}!", key, culture, CultureInfo.InvariantCulture.Equals(culture.Parent) ? "Invariant" : culture.Parent.ToString()));
                // We go up the culture hirarchy until we find a translation
                return GetTranslation(culture.Parent, key);
            }

            Log.Warn(string.Format("Could not find any translation for key '{0}'!", key));
            return string.Format("[{0}]", key);
        }

        public void Reload()
        {
            _hirarchyLoadedFlags.Clear();
            _cache.Clear();
        }

        /// <summary>
        /// Gets the list which holds all message sources.
        /// </summary>
        public List<IMessageSource> MessageSources
        {
            get { return _messageSources; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns all available cultures from all currently registered message sources
        /// </summary>
        /// <returns></returns>
        private ISet<CultureInfo> GetAllAvailableCultures()
        {
            var availableCultures = new HashSet<CultureInfo>();
            foreach (var messageSource in _messageSources)
            {
                foreach (var mCulture in messageSource.GetAvailableCultures())
                {
                    availableCultures.Add(mCulture);
                }
            }
            return availableCultures;
        }



        /// <summary>
        /// Ensures that the whole hirarchy of the given culture is loaded,
        /// up to the Invariant culture.
        /// 
        /// For example if you want to ensure en-US is loaded, it will ensure that...
        /// 
        /// -> en-US
        /// -> en
        /// -> Invariant
        /// 
        /// ... are all loaded.
        /// </summary>
        /// <param name="culture"></param>
        private void EnsureCultureHirarchyLoaded(CultureInfo culture)
        {
            if (!_hirarchyLoadedFlags.ContainsKey(culture) || _hirarchyLoadedFlags[culture])
            {
                var availables = GetAllAvailableCultures();

                var hirarchy = CultureInfoUtil.FindAllSimilarCultures(availables, culture);
                hirarchy.Add(CultureInfo.InvariantCulture);

                foreach (var hcult in hirarchy)
                {
                    EnsureCultureLoaded(hcult);
                }

                if (!_hirarchyLoadedFlags.ContainsKey(culture))
                {
                    _hirarchyLoadedFlags.Add(culture, true);
                }
                else
                {
                    _hirarchyLoadedFlags[culture] = true;
                }
            }
        }

        private void EnsureCultureLoaded(CultureInfo culture)
        {
            if (!_cache.Exists(culture))
            {
                LoadCulture(culture);
            }
        }

        private void LoadCulture(CultureInfo culture)
        {
            Log.Info(string.Format("Loading localisation culture '{0}' ...", culture == null ? "default" : culture.Name));

            var messages = LoadMessageSources(culture);
            _cache.Update(culture, messages);
        }

        private IDictionary<string, string> LoadMessageSources(CultureInfo culture)
        {
            var ps = new Dictionary<string, string>();
            foreach (var messageSource in _messageSources)
            {
                try
                {
                    var messages = messageSource.Load(culture);
                    DictionaryUtil.MergeLeft(ps, messages);
                }
                catch (Exception e)
                {
                    Log.Error("Failed to load message-source!", e);
                }

            }
            return ps;
        }

        #endregion

    }
}
