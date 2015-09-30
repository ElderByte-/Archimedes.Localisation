using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation
{
    public static class Translator
    {
        private static ITranslationProvider _translationProvider;

        public static ITranslationProvider TranslationProvider
        {
            get { return _translationProvider; }
            set { _translationProvider = value; }
        }


        public static string GetTranslation(string key)
        {
            if (TranslationProvider != null)
            {
                return TranslationProvider.GetTranslation(key);
            }
            return "[" + key + "]";
        }

        public static string GetTranslation(string key, params object[] args)
        {
            if (TranslationProvider != null)
            {
                return TranslationProvider.GetTranslation(key, args);
            }
            return "[" + key + "]";
        }
    }
}
