using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    internal class AccommodationRepository : Repository<Accommodation>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodation = base.Get(entity.Id);

            ((Accommodation)accommodation).Id = ((Accommodation)entity).Id;
            ((Accommodation)accommodation).Name = ((Accommodation)entity).Name;
            ((Accommodation)accommodation).Location = ((Accommodation)entity).Location;
            ((Accommodation)accommodation).Type = ((Accommodation)entity).Type;
            ((Accommodation)accommodation).GuestLimit = ((Accommodation)entity).GuestLimit;
            ((Accommodation)accommodation).MinimalStay = ((Accommodation)entity).MinimalStay;
            ((Accommodation)accommodation).CancelationLimit = ((Accommodation)entity).CancelationLimit;

        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();
            foreach (Entity it in SIMSContext.Instance.Accommodations)
            {
                if (((Accommodation)it).Name.Contains(term))
                {
                    result.Add(it);
                }
            }

            return result;
        }
<<<<<<< Updated upstream
        public Accommodation GetAccommodationByType(AccommodationType type)
=======

        public List<Accommodation> GetAccommodationByType()
>>>>>>> Stashed changes
        {
            List<Accommodation> retList = new List<Accommodation>();
            foreach (Accommodation it in SIMSContext.Instance.Accommodations)
            {
                if (it.Type == AccommodationType.APARTMENT)
                {
                    retList.Add(it);
                }
            }
            return retList;
        }

    
    }
}
