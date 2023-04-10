using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class AccommodationOwnerRatingService
    {
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

        /* public IEnumerable<Entity> GetAllOwnerRatings()
         {
             UnitOfWork unitOfWork = new UnitOfWork();
             List<Entity> accommodations = unitOfWork.Accommodations.GetAccommodationsByOwner(unitOfWork.Users.GetLoginUser().Id);

             foreach (Accommodation accommodation in accommodations)
             {
                AccommodationReservation reservation = unitOfWork.AccommodationReservations.GetAccommodationReservationByAccommodation(accommodation);
             }

             foreach (AccommodationReservation accommodation in accommodations)
             {
                 unitOfWork.AccommodationReservations.GetAccommodationReservationByAccommodation(accommodation);
             }

             return unitOfWork.AccommodationOwnerRatings.GetAll();
         }*/

        public int GetRatingOwnerId(AccommodationOwnerRating request)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            AccommodationReservation reservation = (AccommodationReservation)unitOfWork.AccommodationReservations.Get(request.ReservationId);
            Accommodation accommodation = (Accommodation)unitOfWork.Accommodations.GetAccommodationByName(reservation.AccommodationName);
            int ownerId = accommodation.OwnerId;
            return ownerId;
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationOwnerRatings.GenerateId();
        }
    }
}