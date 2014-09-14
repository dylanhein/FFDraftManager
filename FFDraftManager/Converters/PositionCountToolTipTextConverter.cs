using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFDraftManager.Models;

namespace FFDraftManager.Converters {
    class PositionCountToolTipTextConverter : IMultiValueConverter {
        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        /// <returns>
        /// A converted value.If the method returns null, the valid null value is used.A return value of <see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            string name = values[0].ToString();
            string position = values[1].ToString();
            int count = 0;
            if (int.TryParse(values[2].ToString(), out count)) {
                if (count == 1) {
                    return string.Format("{0} has {1} {2}", name, count, position);
                }
                return string.Format("{0} has {1} {2}s", name, count, position);
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
