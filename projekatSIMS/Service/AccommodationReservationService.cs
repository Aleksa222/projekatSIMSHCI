using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class AccommodationReservationService
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


        public void CreateAccommodationReservation(AccommodationReservation accommodationReservation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            if (unitOfWork.AccommodationReservations.GetAccommodationReservationById(accommodationReservation.Id) != null)
            {
                throw new Exception("ID already exist!");
            }
            unitOfWork.AccommodationReservations.Add(accommodationReservation);
            unitOfWork.Save();
        }

     

        public ObservableCollection<AccommodationReservation> GetOverlappingReservations(string accommodationName,DateTime from, DateTime to)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ObservableCollection<AccommodationReservation> retList = new ObservableCollection<AccommodationReservation>();
            foreach (AccommodationReservation r in unitOfWork.AccommodationReservations.GetAll())
                {
                    if (r.AccommodationName == accommodationName &&
                    r.StartDate < from &&
                    r.EndDate > to)
                    {
                       retList.Add(r);
                    }
                }
            return retList;
        }

        public ObservableCollection<AccommodationReservation> GetReservationsByAccommodationAndDateRange(DateTime startDate, DateTime endDate, string accommodationName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ObservableCollection<AccommodationReservation> retList = new ObservableCollection<AccommodationReservation>();
            foreach (AccommodationReservation r in unitOfWork.AccommodationReservations.GetAll())
            {
                if (r.AccommodationName == accommodationName && r.StartDate >= startDate && r.EndDate <= endDate)
                {
                    retList.Add(r);
                }


            }

            return retList;
        }

        public ObservableCollection<AccommodationReservation> GetReservationsByAccommodation( string accommodationName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ObservableCollection<AccommodationReservation> retList = new ObservableCollection<AccommodationReservation>();
            foreach (AccommodationReservation r in unitOfWork.AccommodationReservations.GetAll())
            {
                if (r.AccommodationName == accommodationName )
                {
                    retList.Add(r);
                }


            }

            return retList;
        }

        public ObservableCollection<AccommodationReservation> GetReservationsByOwner(ObservableCollection<Accommodation> accommodations)
        {
            ObservableCollection<AccommodationReservation> reservations = new ObservableCollection<AccommodationReservation>();

            foreach (Accommodation accommodation in accommodations)
            {
                // Simuliramo dobijanje rezervacija iz baze podataka ili nekog izvora podataka
                ObservableCollection<AccommodationReservation> reservationsForAccommodation = GetReservationsByAccommodation(accommodation.Name);

                // Dodajemo rezervacije u listu ukoliko su vezane za smeštaj vlasnika
                foreach (AccommodationReservation reservation in reservationsForAccommodation)
                {
                    if (reservation.AccommodationName == accommodation.Name)
                    {
                        reservations.Add(reservation);
                    }
                }
            }

            return reservations;
        }



    }
}
