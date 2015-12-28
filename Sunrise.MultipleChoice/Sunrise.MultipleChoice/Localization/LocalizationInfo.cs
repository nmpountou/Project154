using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sunrise.MultipleChoice.Localization
{
    public class LocalizationInfo
    {
        /// <summary>
        /// Dependency property that should re-evaluate when culture change happens
        /// </summary>
        /// <value>The property.</value>
        /// Change to static.
        public DependencyProperty Property { get; set; }

        /// <summary>
        /// Resource key
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
    }
}
