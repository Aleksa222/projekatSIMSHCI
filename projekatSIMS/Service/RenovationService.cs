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
    internal class RenovationService
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

        public IEnumerable<DateTime> GetFreeTerms(string accommodationName, DateTime startDate, DateTime endDate, int renovationDuration)
        {
            List<DateTime> availableDates = new List<DateTime>();

            // Izvučemo sve rezervacije koje se preklapaju sa zadatim opsegom datuma
            var overlappingReservations = reservationService.GetOverlappingReservations(accommodationName, startDate, endDate);

            // Napravimo listu datuma koje pokriva period renoviranja
            List<DateTime> renovationDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                renovationDates.Add(date);
            }
            renovationDates.RemoveAll(date => overlappingReservations.Any(reservation => reservation.StartDate <= date && date <= reservation.EndDate));

            // Prođemo kroz sve uzastopne grupe od 'renovationDuration' dana u periodu između startDate i endDate
            for (DateTime date = startDate; date.AddDays(renovationDuration - 1) <= endDate; date = date.AddDays(1))
            {
                // Proverimo da li postoje slobodni termini unutar ove grupe datuma
                bool isAvailable = true;
                for (int i = 0; i < renovationDuration; i++)
                {
                    if (!renovationDates.Contains(date.AddDays(i)))
                    {
                        isAvailable = false;
                        break;
                    }
                }

                // Ako postoji slobodan termin, dodamo ga u listu slobodnih termina
                if (isAvailable)
                {
                    availableDates.Add(date);
                }
            }

            return availableDates;
        }
    }
}
