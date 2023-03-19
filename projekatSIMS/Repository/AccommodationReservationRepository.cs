using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class AccommodationReservationRepository : Repository<AccommodationReservation>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodationReservation = base.Get(entity.Id);

            ((AccommodationReservation)accommodationReservation).Id = ((AccommodationReservation)entity).Id;
            ((AccommodationReservation)accommodationReservation).Accommodation = ((AccommodationReservation)entity).Accommodation;
            ((AccommodationReservation)accommodationReservation).StartDate = ((AccommodationReservation)entity).StartDate;
            ((AccommodationReservation)accommodationReservation).EndDate = ((AccommodationReservation)entity).EndDate;
            ((AccommodationReservation)accommodationReservation).NumberOfGuests = ((AccommodationReservation)entity).NumberOfGuests;



        }
    }
}
