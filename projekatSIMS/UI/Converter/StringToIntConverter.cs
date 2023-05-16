using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace projekatSIMS.UI.Converter
{
    public class StringToIntConverter : IValueConverter
    {
      
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is string stringValue)
                {
                    if (int.TryParse(stringValue, out int intValue))
                        return intValue;
                }

                return 0; // Vraća se podrazumevana vrednost ako konverzija nije uspela
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
            if (value is string stringValue)
            {
                if (int.TryParse(stringValue, out int intValue))
                {
                    return intValue;
                }
            }

            // Vratiti neku podrazumevanu vrednost ili prijaviti grešku u konverziji
            // Na primer, možete vratiti null ili 0 kao podrazumevanu vrednost
            return null;
            // Nije potrebno implementirati konverziju unazad ako ne želite da se vrednost iz View-a ažurira u ViewModelu
            
            }
        }
    }

