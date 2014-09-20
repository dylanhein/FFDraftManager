using System;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FFDraftManager.Models;

namespace FFDraftManager.Converters {
    class PositionCountToolTipTextConverter : IValueConverter {
        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        /// <returns>
        /// A converted value.If the method returns null, the valid null value is used.A return value of <see cref="T:System.Windows.DependencyProperty" />.<see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding" />.<see cref="F:System.Windows.Data.Binding.DoNothing" /> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var team = value as FantasyTeam;
            if (team != null) {
                var name = team.TeamName;
                string position = parameter.ToString();
                var count = GetCount(position, team);
                if (count == 1) {
                    return string.Format("{0} has {1} {2}", name, count, position);
                }
                return string.Format("{0} has {1} {2}s", name, count, position);
            }
            return "";
        }

        private int GetCount(string position, FantasyTeam team) {
            switch (position) {
                case "QB":
                    return team.QBCount;
                case "RB":
                    return team.RBCount;
                case "WR":
                    return team.WRCount;
                case "TE":
                    return team.TECount;
                case "DST":
                    return team.DSTCount;
                case "PK":
                    return team.PKCount;
                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
