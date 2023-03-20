using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class AccommodationReservation  : Entity
    {
        private Accommodation accommodation;
        private AccommodationService accommodationService = new AccommodationService();
        private DateTime startDate;
        private DateTime endDate;
        private int numberOfGuests;


        public AccommodationReservation() { }

        public AccommodationReservation(Accommodation accommodation, DateTime startDate, DateTime endDate, int numberOfGuests)
        {
            this.accommodation = accommodation;
            this.startDate = startDate;
            this.endDate = endDate;
            this.numberOfGuests = numberOfGuests;
        }

        public Accommodation Accommodation
        {
            get { return accommodation; }
            set
            {
                accommodation = value;
                OnPropertyChanged(nameof(Accommodation));
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
        public int NumberOfGuests
        {
            get { return numberOfGuests; }
            set
            {
                numberOfGuests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + accommodation.Id + "|" + startDate.Date + "|" + endDate.Date + "|" + numberOfGuests;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Accommodation = (Accommodation)accommodationService.Get(int.Parse(parts[1]));
            StartDate = DateTime.ParseExact(parts[2], "dd.MM.yyyy", null);
            EndDate = DateTime.ParseExact(parts[3], "dd.MM.yyyy", null);
            NumberOfGuests = int.Parse(parts[4]);
            

        }



    }
}