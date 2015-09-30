using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archimedes.Localisation.MessageSources;
using NUnit.Framework;

namespace Archimedes.Localisation.Test
{
    public class FilePropertiesMessageSourceTest
    {

        [TestCase("de", "myResources_de.properties", true)]
        [TestCase("de-DE", "myResources_de_DE", true)]
        [TestCase("en-CH", "myResources_de", false)]
        [TestCase("de-DE", "myResources_de", false)]
        [TestCase("de-CH", "myResources_de_CH.properties", true)]
        [TestCase("de-CH", "myResources_de_DE.properties", false)]
        [TestCase("de", "myResources_de_DE.properties", false)]
        public void TestCultureMatch(string cultureStr, string filename, bool expectedMatch)
        {
            var culture = CultureInfo.GetCultureInfo(cultureStr);
            Assert.AreEqual(expectedMatch, FilePropertiesMessageSource.IsMatch(culture, filename));
        }
    }
}
