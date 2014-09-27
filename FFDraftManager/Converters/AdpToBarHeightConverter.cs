using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFDraftManager.Models;

namespace FFDraftManager.Converters {
    class AdpToBarHeightConverter : IValueConverter {
        private const int AdpBarHeightInPixels = 300;

        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        /// <returns>
        /// A converted value.If the method returns null, the valid null value is used.A return value of <see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int adpSum = 0;
            int maxAdpShown = 0;
            if (!int.TryParse(value.ToString(), out adpSum)) {
                return 0;
            }
            if (!int.TryParse(parameter.ToString(), out maxAdpShown)) {
                return 0;
            }
            return (double.Parse(adpSum.ToString()) / double.Parse(maxAdpShown.ToString())) * AdpBarHeightInPixels;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
