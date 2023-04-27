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
        int accommodationId;
        DateTime startDate;
        DateTime endDate;
        string description;

        bool isDone;

        public Renovation() { }


        public Renovation(int accommmodationId, DateTime startDate, DateTime endDate, string description, bool isDone = false)
        {
            this.accommodationId = accommmodationId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.description = description;
            this.isDone = isDone;
        }


        public int AccommodationId
        {
            get { return accommodationId; }
            set
            {
                accommodationId = value;
                OnPropertyChanged(nameof(AccommodationId));
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

        public override string ExportToString()
        {
            return id + "|" + accommodationId + "|" + startDate + "|" + endDate + "|" + description + "|"  + isDone;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);


            accommodationId = int.Parse(parts[1]);
            StartDate = DateTime.Parse(parts[2]);
            EndDate = DateTime.Parse(parts[3]);
            Description = parts[4];
            IsDone = bool.Parse(parts[5]);
        }
    }
}
