using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation.Utils
{
    internal static class DictionaryUtil
    {
        /// <summary>
        /// Merge the right dictionary into the left.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public static void MergeLeft(IDictionary<string, string> d1, IDictionary<string, string> d2)
        {
            foreach (var kv in d2)
            {
                if (d1.ContainsKey(kv.Key))
                {
                    d1[kv.Key] = kv.Value;
                }
                else
                {
                    d1.Add(kv.Key, kv.Value);
                }
            }
        } 
    }
}
