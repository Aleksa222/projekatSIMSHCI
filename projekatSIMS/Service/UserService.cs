﻿using projekatSIMS.Model;
using projekatSIMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Service
{
    public class UserService
    {
        //Implementira osnovne metode i moze imati posebne metode u zavisnosti od funkcionalnosti
        public void Add(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork(); //Instanciramo UnitOfWork jer cemo preko njega pristupiti repositorijumu
            unitOfWork.Users.Add(user);//Pozovemo odgovarajuci repo i odogvarajucu funkciju
            unitOfWork.Save(); //Sacuvas podatke
        }

        public void Edit(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.Edit(user);
            unitOfWork.Save();
        }

        public void Remove(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.Remove(user);
            unitOfWork.Save();
        }

        public Entity Get(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.Get(id);
        }

        public IEnumerable<Entity> GetAll()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetAll();
        }

        public int GenerateId()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GenerateId();
        }

        public IEnumerable<Entity> Search(string term = "")
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.Search(term);
        }

        public void CheckCredentials(string email, string pass)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            foreach (User user in unitOfWork.Users.GetAll())
            {
                if (user.Email.Equals(email) && user.Password.Equals(pass))
                {
                    SetLoginUser(user);
                }
            }
        }

       

        public User GetLoginUser()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetLoginUser();
        }

        public void SetLoginUser(User user)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.Users.SetLoginUser(user);
        }

        public string GetLoginUserType()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetLoginUserType();
        }

        public int GetOwnerReviewCount()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetOwnerReviewCount();
        }

        public double GetOwnerAverageRating()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            return unitOfWork.Users.GetOwnerAverageRating();
        }

        // Metoda za ažuriranje broja ocena i prosečne ocene vlasnika
        public void UpdateOwnerRating(int ownerId, double newRating)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);

            if (owner == null)
            {
                throw new ArgumentException("Vlasnik sa datim ID-jem nije pronađen.");
            }


            double currentRating = owner.AverageRating;
            int reviewCount = owner.ReviewCount;

            double newAverageRating = ((currentRating * reviewCount) + newRating) / (reviewCount + 1);
            owner.AverageRating = newAverageRating;
            owner.ReviewCount = reviewCount + 1;

            // Save changes to the repository
            unitOfWork.Users.Edit(owner);
            SetSuperOwner(ownerId);
            unitOfWork.Save();
        }

        public bool IsSuperOwner(int ownerId)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);
            return unitOfWork.Users.GetOwnerReviewCount() >= 50 && unitOfWork.Users.GetOwnerAverageRating() > 9.5;
        }

        public void SetSuperOwner(int ownerId)
        {
            UnitOfWork unitOfWork= new UnitOfWork();
            User owner = (User)unitOfWork.Users.Get(ownerId);

            if (owner != null && IsSuperOwner(ownerId))
            {
                owner.SuperStatus = true;
            }

            owner.SuperStatus = false;

        }


    }


    }

