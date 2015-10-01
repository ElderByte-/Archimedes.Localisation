using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation
{
    /// <summary>
    /// The translator is a easy to use static utility class to access translated texts.
    /// 
    /// </summary>
    public static class Translator
    {
        private static ITranslationProvider _translationProvider;

        /// <summary>
        /// Gets / Sets the current translation provider.
        /// If you do not set this property your self, a default LocalisationService is
        /// being created for you, which supports default properties files.
        /// </summary>
        public static ITranslationProvider TranslationProvider
        {
            get
            {
                if (_translationProvider == null)
                {
                    _translationProvider = new LocalisationService();
                }
                return _translationProvider;
            }
            set { _translationProvider = value; }
        }

        public static string GetTranslation(string key)
        {
            return TranslationProvider.GetTranslation(key);
        }

        public static string GetTranslation(string key, params object[] args)
        {
            return TranslationProvider.GetTranslation(key, args);
        }

        public static string GetTranslation(CultureInfo culture, string key)
        {
            return TranslationProvider.GetTranslation(culture, key);
        }

        public static string GetTranslation(CultureInfo culture, string key, params object[] args)
        {
            return TranslationProvider.GetTranslation(culture, key, args);
        }
    }
}
