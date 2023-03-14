using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    internal class AccommodationService
    {
        public void Add(Accommodation room)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Add(room);
            unitOfWork.Save();
        }

        public void Edit(Accommodation room)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Edit(room);
            unitOfWork.Save();
        }
        public void Remove(Accommodation room)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Accommodations.Remove(room);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.GetAll();
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.GenerateId();
        }

      

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Accommodations.Search(term);
        }

        public Accommodation GetAccommodationByName(string name)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            Accommodation result = null;
            foreach (Accommodation it in unitOfWork.Accommodations.GetAll())
            {
                if (it.Name == name)
                {
                    result = it;
                }
            }
            return result;
        }
    }
}
