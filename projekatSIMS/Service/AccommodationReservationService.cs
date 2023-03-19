using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    class AccommodationReservationService
    {
        public void Add(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Add(accommodationReservation);
            unitOfWork.Save();
        }

        public void Edit(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Edit(accommodationReservation);
            unitOfWork.Save();
        }

        public void Remove(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.AccommodationReservations.Remove(accommodationReservation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.AccommodationReservations.GenerateId();
        }

        public List<Entity> GetAvailableAccommodations(DateTime start, DateTime end)
        {

            UnitOfWork unitOfWork = new UnitOfWork();
            List<Entity> allAccommodations = new List<Entity>(unitOfWork.Accommodations.GetAll());
            var reservedAccommodations = this.GetAll();
            List<Entity> availableAccommodations = new List<Entity>(allAccommodations);
            foreach (Accommodation r in allAccommodations)
            {
                foreach (AccommodationReservation reservation in reservedAccommodations)
                {
                    if ((((start <= reservation.StartDate) && (end > reservation.StartDate)) || ((start < reservation.EndDate) && (end >= reservation.EndDate))) && (reservation.Accommodation == r))
                    {
                        availableAccommodations.Remove(r);
                    }
                }
            }
            return availableAccommodations;
        }
    }
}
