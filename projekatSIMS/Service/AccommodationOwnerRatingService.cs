using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class AccommodationOwnerRatingService
    {
        private readonly IRepository<AccommodationOwnerRating> accommodationOwnerRatingRepository;

        public AccommodationOwnerRatingService(IRepository<AccommodationOwnerRating> _accommodationOwnerRatingRepository)
        {
            accommodationOwnerRatingRepository = _accommodationOwnerRatingRepository;
        }

        public AccommodationOwnerRatingService()
        {
        }

        public void Add(AccommodationOwnerRating accommodationOwnerRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationOwnerRatings.Add(accommodationOwnerRating);
            unitOfWork.Save();
        }

        public void Edit(AccommodationOwnerRating accommodationOwnerRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationOwnerRatings.Edit(accommodationOwnerRating);
            unitOfWork.Save();
        }

        public void Remove(AccommodationOwnerRating accommodationOwnerRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationOwnerRatings.Remove(accommodationOwnerRating);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationOwnerRatings.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationOwnerRatings.GetAll();
        }


        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationOwnerRatings.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationOwnerRatings.GenerateId();
        }

        public int GetRatingOwnerId(string accommodationName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Accommodation accommodation = unitOfWork.Accommodations.GetAccommodationByName(accommodationName);
            int ownerId = accommodation.OwnerId;
            return ownerId;
        }

        public ObservableCollection<AccommodationOwnerRating> GetOwnerRatingsByAccommodation(string accommodationName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Entity> ratings = new List<Entity>();
            ratings = (List<Entity>)unitOfWork.AccommodationOwnerRatings.GetAll();
            ObservableCollection<AccommodationOwnerRating> retList = new ObservableCollection<AccommodationOwnerRating>();            
            foreach (AccommodationOwnerRating it in ratings)
            {
                if (it.AccommodationName == accommodationName)
                {
                    retList.Add(it);
                }
            }
            return retList;
        }

        public List<int> GetOwnerRatings(ObservableCollection<AccommodationOwnerRating> ratings)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<int> ownerRatings = new List<int>();
            
            foreach(AccommodationOwnerRating it in ratings)
            {
                ownerRatings.Add(it.OwnerPoliteness);
            }
            return ownerRatings;
        }

        public ObservableCollection<AccommodationOwnerRating> GetOwnerRatingsById(int ownerId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ObservableCollection<AccommodationOwnerRating> ownerRatings = new ObservableCollection<AccommodationOwnerRating>();

            List<Accommodation> accommodations = unitOfWork.Accommodations.GetAccommodationsByOwner(ownerId);

            foreach (Accommodation accommodation in accommodations)
            {
                ObservableCollection<AccommodationOwnerRating> ratings = GetOwnerRatingsByAccommodation(accommodation.Name);

                if (ratings != null)
                {
                    foreach (var rating in ratings)
                    {
                        ownerRatings.Add(rating);
                    }
                }
            }

            return ownerRatings;
        }
    }
   }
