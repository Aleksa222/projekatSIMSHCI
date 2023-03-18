using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    internal class GuestReview : Entity
    {
        public int ownerId;
        public int reservationId;
        public int cleanliness;
        public int respectingRules;
        public string comment;

        public GuestReview() { }
        public GuestReview(int ownerId, int cleanliness, int respectingRules, string comment,int reservationId)
        {
            this.comment = comment;
            this.cleanliness = cleanliness;
            this.respectingRules = respectingRules;
            this.reservationId = reservationId;
        }

        public int OwnerId
        {
            get { return ownerId; }
            set
            {
                ownerId = value;
                OnPropertyChanged(nameof(OwnerId));
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

        public int ReservationId
        {
            get { return reservationId; }
            set
            {
                reservationId = value;
                OnPropertyChanged(nameof(ReservationId));
            }
        }
        public override string ExportToString()
        {
            return id + "|" + ownerId + " " + reservationId +  "|" + cleanliness + "|" + respectingRules + "|" + comment;
        }

        public override void ImportFromString(string[] parts)
        {

            base.ImportFromString(parts);
            OwnerId = int.Parse(parts[1]);
            ReservationId = int.Parse(parts[2]);
            Cleanliness = int.Parse(parts[3]);
            respectingRules = int.Parse(parts[4]);
            Comment = parts[5];
        }
    }
}

