using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model.ModelDto
{
    public class OwnerRatingDto
    {
        public int Id { get; set; }
        public string AccommodationName { get; set; }
        public string GuestName { get; set; }
        public string GuestSurname { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerPoliteness { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }

  
    }
  

}
