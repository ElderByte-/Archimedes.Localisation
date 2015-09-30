using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Archimedes.Localisation.Utils
{
    public static class AppUtil
    {
        public static string AppName
        {
            get
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != (Assembly)null)
                    return FileVersionInfo.GetVersionInfo(entryAssembly.Location).ProductName;
                throw new NotSupportedException("Entry Assembly not available! This may occur in specail circumstances, such as when running as Unit Test.");
            }
        }

        public static string Vendor
        {
            get
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != (Assembly)null)
                    return FileVersionInfo.GetVersionInfo(entryAssembly.Location).CompanyName;
                throw new NotSupportedException("Entry Assembly not available! This may occur in specail circumstances, such as when running as Unit Test.");
            }
        }

        public static string AppDataFolder
        {
            get
            {
                return string.Format("{0}\\{1}\\{2}", (object)Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), (object)AppUtil.Vendor, (object)AppUtil.AppName);
            }
        }

        public static string ApplicaitonBinaryFolder
        {
            get
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly != (Assembly)null)
                    return Path.GetDirectoryName(entryAssembly.Location);
                throw new NotSupportedException("Entry Assembly not available! This may occur in specail circumstances, such as when running as Unit Test.");
            }
        }
    }
}
