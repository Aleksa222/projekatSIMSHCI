using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public interface IAccommodationRepository : IRepository<Accommodation>
    {
        Accommodation GetAccommodationByType(AccommodationType type);

        Accommodation GetAccommodationById(int id);

        Accommodation GetAccommodationByName(string name);
        List<Accommodation> GetAccommodationsByOwner(int id);

        Accommodation GetAccommodationByLocation(Location location);

        Accommodation GetAccommodationByNameCityAndCountry(Accommodation accommodation);

    }
}
