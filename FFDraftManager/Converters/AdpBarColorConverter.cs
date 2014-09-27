using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Text;
using System.Windows;
using FFDraftManager.Models;

namespace FFDraftManager.Converters {
    class AdpBarColorConverter : IMultiValueConverter {
        
        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) {
            int adpSum = 0;
            int adpPar = 0;
            if (!int.TryParse(value[0].ToString(), out adpSum)) {
                return Application.Current.Resources["AdpParGradient"];
            }
            if (!int.TryParse(value[1].ToString(), out adpPar)) {
                return Application.Current.Resources["AdpParGradient"];
            }

            if (adpSum == adpPar) {
                return Application.Current.Resources["AdpParGradient"];
            }
            if (adpSum < adpPar) {
                return Application.Current.Resources["AdpBelowParGradient"];
            }
            return Application.Current.Resources["AdpOverParGradient"];
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}

