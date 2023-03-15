using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace projekatSIMS.Model
{
    public class GuestReview : Entity
    {
        public int ownerId; //?
        public int cleanliness;
        public int respectingRules;
        public string comment;

        public GuestReview() { }
        public GuestReview(int ownerId,int cleanliness,int respectingRules,string comment)
        {
            this.comment = comment;
            this.cleanliness = cleanliness;
            this.respectingRules = respectingRules;
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
        public override string ExportToString()
        {
            return id + "|" + ownerId + "|" + cleanliness + "|" + respectingRules + "|" + comment;
        }

        public override void ImportFromString(string[] parts)
        {

            base.ImportFromString(parts);
            OwnerId = int.Parse(parts[1]);
            Cleanliness = int.Parse(parts[2]);
            respectingRules = int.Parse(parts[3]);
            Comment = parts[4];
        }
    }



}

