using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using log4net;

namespace Archimedes.Localisation.WPF
{
    /// <summary>
    /// A simple XAML markup extension to localize WPF views.
    /// 
    /// Example usage:
    /// 
    /// <code>
    /// &lt;TextBox Text=&quot;{loc:Localisation Id=customers.title}&quot;/&gt;
    /// </code>
    /// 
    /// </summary>
    public class LocalisationExtension : MarkupExtension
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public LocalisationExtension()
        {

        }

        public LocalisationExtension(object id)
        {
            Id = id;
        }

        /// <summary>
        /// The text-key for which the translation text should be loaded
        /// </summary>
        [ConstructorArgument("id")]
        public object Id { get; set; }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (!IsInDesignMode())
                {
                    return Translator.GetTranslation(Id.ToString());
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed to provide localized text!", e);
            }

            return Id;
        }

        private static bool? _isInDesignMode;
        public static bool IsInDesignMode()
        {
            if (!_isInDesignMode.HasValue)
            {
                _isInDesignMode = Assembly.GetExecutingAssembly().Location.Contains("VisualStudio");
            }
            return _isInDesignMode.Value;
        }
    }
}
