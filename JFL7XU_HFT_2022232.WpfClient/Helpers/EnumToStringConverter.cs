using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace JFL7XU_HFT_2022232.WpfClient.Helpers
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var faction = (ShipType)value;
            double Value = double.Parse(((int)faction).ToString());

            switch (Value)
            {
                case 1:
                    return ShipType.Transport.ToString();
                    break;
                case 2:
                    return ShipType.Fregatte.ToString();
                    break;
                case 3:
                    return ShipType.Cruiser.ToString();
                    break;
                case 4:
                    return ShipType.Fighter.ToString();
                    break;
                default:
                    return null;
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
