using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class AccommodationReservation  : Entity
    {
            private string accommodationName;
            private DateTime startDate;
            private DateTime endDate;
            private int guestCount;


        public AccommodationReservation() { }

        public AccommodationReservation(string accommodation, DateTime startDate, DateTime endDate, int guestCount)
        {
            this.accommodationName = accommodation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.guestCount = guestCount;
        }

        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                accommodationName = value;
                OnPropertyChanged(nameof(AccommodationName));
            }
        }


        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value.Date;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value.Date;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public int GuestCount
        {
            get { return guestCount; }
            set
            {
                guestCount = value;
                OnPropertyChanged(nameof(GuestCount));
            }
        }

      

        public override string ExportToString()
        {
            return id + "|" + accommodationName + "|" + startDate.ToString("yyyy-MM-dd") + "|" + endDate.ToString("yyyy-MM-dd") + "|" + guestCount;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            accommodationName = parts[1];
            startDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            guestCount = int.Parse(parts[4]);

        }


    }

}
