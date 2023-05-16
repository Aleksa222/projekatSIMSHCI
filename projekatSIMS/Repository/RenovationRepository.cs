using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class RenovationRepository : Repository<Renovation>
    {

        public override void Edit(Entity entity)
        {
            Entity renovation = base.Get(entity.Id);

            ((Renovation)renovation).AccommodationName = ((Renovation)entity).AccommodationName;
            ((Renovation)renovation).StartDate = ((Renovation)entity).StartDate;
            ((Renovation)renovation).EndDate = ((Renovation)entity).EndDate;
            ((Renovation)renovation).Description = ((Renovation)entity).Description;
            ((Renovation)renovation).Description = ((Renovation)entity).Description;
            ((Renovation)renovation).IsDone = ((Renovation)entity).IsDone;
            ((Renovation)renovation).IsCanceled = ((Renovation)entity).IsCanceled;


        }

        public override IEnumerable<Entity> Search(string term = "")
        {
            return base.Search(term);
        }
    }
}
