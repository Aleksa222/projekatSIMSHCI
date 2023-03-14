using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class UserRepository : Repository<User>
    {
        public IEnumerable<Entity> Search(string term = "") //Posto se search razlikuje svaki entitet mora da implementira search na svoj nacin
        {
            List<Entity> result = new List<Entity>();  //Formiram praznu listu koju cu da popunim i vratim
            foreach (Entity it in SIMSContext.Instance.Users) //Daj mi listu usera
            {
                if (((User)it).FirstName.ToLower().Contains(term.ToLower()) || ((User)it).LastName.ToLower().Contains(term.ToLower())) //Pogledaj da li se poklapaju ime ili prezime sa poslatim stringom
                {
                    result.Add(it); // ako da dodaj u listu jer moze da ih bude vise
                }
            }

            return result; //vrati result
        }
        //override za edit

        public override void Edit(Entity entity) //prosledjujem izmenjen entitet ali mu je id isti
        {
            Entity user = base.Get(entity.Id);

            ((User)user).Id = ((User)entity).Id;            //I sada menjam vrednosti sa tim da se id ne moze promeniti!
            ((User)user).Email = ((User)entity).Email;
            ((User)user).FirstName = ((User)entity).FirstName;
            ((User)user).LastName = ((User)entity).LastName;
            ((User)user).Password = ((User)entity).Password;
        }

        public void SetLoginUser(User user)
        {
            SIMSContext.Instance.LoginUser = user;
        }

        public User GetLoginUser()
        {
            return SIMSContext.Instance.LoginUser;
        }
    }
}
