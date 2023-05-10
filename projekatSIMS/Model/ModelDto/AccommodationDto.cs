using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model.ModelDto
{
    public class AccommodationDto
    {
        public  string Name { get; set; }
        public string LocationCityName { get; set; }
        public string LocationCountryName { get; set; }
        public string Type { get; set; }
        public int GuestLimit { get; set; }
        public int MinimumStayDays { get; set; }
        public int CancellationDays { get; set; }
        public string ImageUrl { get; set; }
    }
}
