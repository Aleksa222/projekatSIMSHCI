using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    internal class GuestReviewService
    {
        public void Add(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork(); 
            unitOfWork.GuestReviews.Add(guestReview);
            unitOfWork.Save();
        }

        public void Edit(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.GuestReviews.Edit(guestReview);
            unitOfWork.Save();
        }

        public void Remove(GuestReview guestReview)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.GuestReviews.Remove(guestReview);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.GetAll();
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.GuestReviews.GenerateId();
        }

        public bool CheckDate( DateTime endTimeofRegistration)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            DateTime currentDate = DateTime.Now;

            if (endTimeofRegistration < currentDate)
            {
                throw new Exception("Vaš rok za ocenjivanje gosta je istekao.");
               
            }

            return true;
        }

        

        public bool GuestReviewExists(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GuestReview guestReview = new GuestReview();

            if(unitOfWork.GuestReviews.GetGuestReviewByAccommodation(id) != null)
            {
                throw new Exception("Recenzija za datog gosta vec postoji.");
            }

            return true;
        }

     
    }
}
