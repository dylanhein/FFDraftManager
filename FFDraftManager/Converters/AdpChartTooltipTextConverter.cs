using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFDraftManager.Models;

namespace FFDraftManager.Converters {
    class AdpChartTooltipTextConverter : IMultiValueConverter {
        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture) {
            int adpSum = 0;
            int adpPar = 0;
            if (!int.TryParse(value[0].ToString(), out adpSum)) {
                return 0;
            }
            if (!int.TryParse(value[1].ToString(), out adpPar)) {
                return 0;
            }
            string plusSymbol = adpSum - adpPar > 0 ? "+" : "";
            return string.Format("ADP Sum: {0} | Par: {1} | +/-: {2}{3}", adpSum, adpPar, plusSymbol, adpSum - adpPar);
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
