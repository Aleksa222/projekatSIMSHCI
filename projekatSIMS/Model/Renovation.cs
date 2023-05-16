using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class Renovation : Entity
    {
        private string accommodationName;
        private DateTime startDate;
        private DateTime endDate;
        private string description;

        private bool isDone;
        private bool isCanceled;

        public Renovation() { }


        public Renovation(string accommmodationName, DateTime startDate, DateTime endDate, string description, bool isDone = false, bool isCanceled = false)
        {
            this.accommodationName = accommmodationName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.description = description;
            this.isDone = isDone;
            this.IsCanceled = isCanceled;
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
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }

        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public bool IsDone
        {
            get { return isDone; }
            set
            {
                isDone = value;
                OnPropertyChanged(nameof(IsDone));
            }
        }

        public bool IsCanceled
        {
            get { return isCanceled; }
            set
            {
                isCanceled = value;
                OnPropertyChanged(nameof(IsCanceled));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + accommodationName + "|" + startDate + "|" + endDate + "|" + description + "|"  + isDone + "|" + isCanceled;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);


            AccommodationName = parts[1];
            StartDate = DateTime.Parse(parts[2]);
            EndDate = DateTime.Parse(parts[3]);
            Description = parts[4];
            IsDone = bool.Parse(parts[5]);
            IsCanceled = bool.Parse(parts[6]);
        }
    }
}
