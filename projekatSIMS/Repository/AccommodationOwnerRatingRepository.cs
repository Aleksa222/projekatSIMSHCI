using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class AccommodationOwnerRatingRepository : Repository<AccommodationOwnerRating>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodationOwnerRating = base.Get(entity.Id);

            ((AccommodationOwnerRating)accommodationOwnerRating).Id = ((AccommodationOwnerRating)entity).Id;
            ((AccommodationOwnerRating)accommodationOwnerRating).AccommodationName = ((AccommodationOwnerRating)entity).AccommodationName;
            ((AccommodationOwnerRating)accommodationOwnerRating).GuestId = ((AccommodationOwnerRating)entity).GuestId;
            ((AccommodationOwnerRating)accommodationOwnerRating).Cleanliness = ((AccommodationOwnerRating)entity).Cleanliness;
            ((AccommodationOwnerRating)accommodationOwnerRating).OwnerPoliteness = ((AccommodationOwnerRating)entity).OwnerPoliteness;
            ((AccommodationOwnerRating)accommodationOwnerRating).Comment = ((AccommodationOwnerRating)entity).Comment;
            ((AccommodationOwnerRating)accommodationOwnerRating).ImageUrl = ((AccommodationOwnerRating)entity).ImageUrl;
        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            return base.Search(term);
        }

      
    }
}
