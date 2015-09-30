using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Localisation.MessageSources;

namespace Archimedes.Localisation
{
    /// <summary>
    /// Provides localisation services such as translations for different cultures.
    /// </summary>
    public interface ILocalisationService : ITranslationProvider
    {

        /// <summary>
        /// Manages all registered message-sources
        /// </summary>
        List<IMessageSource> MessageSources { get; }

        /// <summary>
        /// Ensures that all translations are updated from the currently registered MessageSources
        /// </summary>
        void Reload();

    }
}
