using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation
{
    public interface ITranslationProvider
    {
        string GetTranslation(string key);

        string GetTranslation(string key, object[] args);
    }
}
