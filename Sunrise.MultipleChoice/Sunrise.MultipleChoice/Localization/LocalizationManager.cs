using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise.MultipleChoice.Properties;
using System.Windows;
using System.Threading;

namespace Sunrise.MultipleChoice.Localization
{
    /// <summary>
    /// Localization Manager
    /// </summary>
    public class LocalizationManager : DependencyObject
    {
        // Private members
        private static HashSet<WeakReference> Elements = new HashSet<WeakReference>();

        // Localization Property
        public static DependencyProperty LocalizationProperty = DependencyProperty.RegisterAttached("Localization",typeof(LocalizationInfo),typeof(FrameworkElement));

        /// <summary>
        /// Sets the localization.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="value">The value.</param>
        public static void SetLocalization(DependencyObject d, LocalizationInfo value)
        {
            d.SetValue(LocalizationProperty, value);

            if (d is FrameworkElement)
            {
                Elements.Add(new WeakReference(d));
            }
        }

        /// <summary>
        /// Gets the localization.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        public static LocalizationInfo GetLocalization(DependencyObject d)
        {
            return (LocalizationInfo)d.GetValue(LocalizationProperty);
        }

        /// <summary>
        /// Updates the resources after a culture change has occurred
        /// </summary>
        public static void UpdateResources()
        {
            List<WeakReference> deadReferences = new List<WeakReference>();
           
            foreach (WeakReference reference in Elements)
            {
                FrameworkElement element = reference.Target as FrameworkElement;

                if (element != null)
                {
                    LocalizationInfo localizationInfo = GetLocalization(element);
                    //////////////////////////////////////////
                    //MessageBox.Show(element.ToString());
                    //////////////////////////////////////////
                    if (localizationInfo != null)
                    {
                        element.SetValue(localizationInfo.Property,
                                         Resources.ResourceManager.GetString(
                                            localizationInfo.ResourceKey,
                                            Thread.CurrentThread.CurrentUICulture));
                    }
                }
                else
                {
                    deadReferences.Add(reference);
                }
            }

            foreach (WeakReference reference in deadReferences)
            {
                Elements.Remove(reference);
            }
        }
    }
}
