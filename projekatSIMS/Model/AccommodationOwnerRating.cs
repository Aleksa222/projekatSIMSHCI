using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class AccommodationOwnerRating : Entity
    {
        private string accommodationName;
        private int guestId;
        private int cleanliness;
        private int ownerPoliteness;
        private string comment;
        private string imageUrl;

        public AccommodationOwnerRating()
        {

        }
            public AccommodationOwnerRating(int guestId,string accommodationName, int cleanliness, int ownerPoliteness, string comment)
        {
            this.guestId = guestId;
            this.accommodationName = accommodationName;
            this.cleanliness = cleanliness;
            this.ownerPoliteness = ownerPoliteness;
            this.comment = comment;
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

        public int GuestId
        {
            get { return guestId; }
            set
            {
                guestId = value;
                OnPropertyChanged(nameof(GuestId));
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

        public int OwnerPoliteness
        {
            get { return ownerPoliteness; }
            set
            {
                ownerPoliteness = value;
                OnPropertyChanged(nameof(OwnerPoliteness));
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

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }

        }

        public override string ExportToString()
        {
            return id + "|" + accommodationName + "|" + guestId + "|" + cleanliness + "|" + ownerPoliteness + "|" + comment + "|" + imageUrl;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            AccommodationName = parts[1];
            GuestId = int.Parse(parts[2]);
            Cleanliness = int.Parse(parts[3]);
            OwnerPoliteness = int.Parse(parts[4]);
            Comment = parts[5];
            ImageUrl = parts[6];
        }
    }
}
