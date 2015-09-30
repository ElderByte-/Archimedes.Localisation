using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation.MessageSources
{
    /// <summary>
    /// Represents a source for localized messages
    /// </summary>
    public interface IMessageSource
    {
        /// <summary>
        /// Load all localized messages in the given culture and returns them in a PropertyStore
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        IDictionary<string,string> Load(CultureInfo culture);

        /// <summary>
        /// Gets all cultures which are supported by this message source
        /// </summary>
        /// <returns></returns>
        ISet<CultureInfo> GetAvailableCultures();
    }
}
