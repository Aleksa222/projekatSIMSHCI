using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class RenovationService
    {
        private readonly IRepository<Renovation> renovationRepository;
        AccommodationReservationService reservationService = new AccommodationReservationService();

        public RenovationService(IRepository<Renovation> _renovationRepository)
        {
            renovationRepository = _renovationRepository;
        }

        public RenovationService()
        {
        }

        public void Add(Renovation renovation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Renovations.Add(renovation);
            unitOfWork.Save();
        }

        public void Edit(Renovation renovation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Renovations.Edit(renovation);
            unitOfWork.Save();
        }

        public void Remove(Renovation renovation)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Renovations.Remove(renovation);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Renovations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Renovations.GetAll();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Renovations.Search(term);
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Renovations.GenerateId();
        }

        public ObservableCollection<Renovation> GetRenovationsByAccommodation(string accommodationName)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            List<Entity> renovations = new List<Entity>();
            renovations = (List<Entity>)unitOfWork.Renovations.GetAll();
            ObservableCollection<Renovation> retList = new ObservableCollection<Renovation>();

            foreach (Renovation it in renovations)
            {
                if (it.AccommodationName == accommodationName)
                {
                    retList.Add(it);
                }
            }
            return retList;
        }

        public ObservableCollection<Renovation> GetRenovationsByOwner(int ownerId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            ObservableCollection<Renovation> retList = new ObservableCollection<Renovation>();
            List<Accommodation> accommodations = new List<Accommodation>();
            accommodations = unitOfWork.Accommodations.GetAccommodationsByOwner(ownerId);



            foreach (Accommodation accommodation in accommodations)
            {
                ObservableCollection<Renovation> renovations = GetRenovationsByAccommodation(accommodation.Name);

                if (renovations != null)
                {
                    foreach (var renovation in renovations)
                    {
                        retList.Add(renovation);
                    }
                }
            }

            return retList;
        }



        public List<(DateTime, DateTime)> FindAvailableDates(DateTime startDate, DateTime endDate, int duration, ObservableCollection<AccommodationReservation> reservations)
        {
            List<(DateTime, DateTime)> availableDateRanges = new List<(DateTime, DateTime)>();
            List<DateTime> availableDates = new List<DateTime>();

            // Generišite listu svih datuma u opsegu
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                availableDates.Add(date);
            }

            int maxIndex = availableDates.Count - duration; // Maksimalni indeks za koji možemo formirati validan opseg datuma


            for (int i = 0; i <= maxIndex; i++)
            {
                bool isRangeAvailable = true;
                DateTime startDate1 = availableDates[i];
                DateTime endDate1 = startDate.AddDays(duration - 1);

                for (int j = i; j <= i + duration - 1; j++)
                {
                    if (reservations.Any(r => availableDates[j] >= r.StartDate && availableDates[j] <= r.EndDate))
                    {
                        isRangeAvailable = false;
                        break;
                    }
                }

                if (isRangeAvailable)
                {
                    availableDateRanges.Add((startDate1, endDate1));
                }
            }

            return availableDateRanges;
        }
    }
}
