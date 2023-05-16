using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class GuestReview : Entity
    {
        public int reservationId;
        public int cleanliness;
        public int respectingRules;
        public string comment;


        public GuestReview() { }
        public GuestReview(int reservationId, int cleanliness, int respectingRules, string comment)
        {
            this.reservationId = reservationId;
            this.comment = comment;
            this.cleanliness = cleanliness;
            this.respectingRules = respectingRules;

        }

        public int ReservationId
        {
            get { return reservationId; }
            set
            {
                reservationId = value;
                OnPropertyChanged(nameof(ReservationId));
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                cleanliness = value;
                OnPropertyChanged(nameof(Cleanliness));
            }
        }
        public int RespectingRules
        {
            get { return respectingRules; }
            set
            {
                respectingRules = value;
                OnPropertyChanged(nameof(RespectingRules));
            }
        }


        public override string ExportToString()
        {
            return id + "|" + reservationId + "|" + cleanliness + "|" + respectingRules + "|" + comment;
        }

        public override void ImportFromString(string[] parts)
        {

            base.ImportFromString(parts);
            ReservationId = int.Parse(parts[1]);
            Cleanliness = int.Parse(parts[2]);
            RespectingRules = int.Parse(parts[3]);
            Comment = parts[4];
        }
    }
}

