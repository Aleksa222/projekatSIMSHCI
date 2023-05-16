using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Model.ModelDto;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    internal class AccommodationRatingsViewModel : ViewModelBase
    {
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private UserService userService;
        private ObservableCollection<AccommodationOwnerRating> ratings;
       

        private ObservableCollection<OwnerRatingDto> ownerRatings;

        private User owner;

        public AccommodationRatingsViewModel() : base()
        {
           
            accommodationOwnerRatingService = new AccommodationOwnerRatingService();
            userService = new UserService();
            owner = userService.GetLoginUser();
            ratings = accommodationOwnerRatingService.GetOwnerRatingsById(owner.Id);

            ownerRatings = new ObservableCollection<OwnerRatingDto>();
            foreach (var rating in ratings)
            {
                // pronalaženje gosta na osnovu ID-a
                User guest = (User)userService.Get(rating.GuestId);
                // kreiranje novog DTO objekta i dodavanje u listu
                ownerRatings.Add(new OwnerRatingDto
                {
                    GuestName = guest.FirstName,
                    GuestSurname = guest.LastName,
                    AccommodationName = rating.AccommodationName,
                    Comment = rating.Comment,
                    OwnerPoliteness = rating.OwnerPoliteness

                });
            }

        }

        public ObservableCollection<AccommodationOwnerRating> Ratings
        {
            get { return ratings; }
            set
            {
                ratings = value;
                OnPropertyChanged(nameof(ratings));
            }
        }

        public ObservableCollection<OwnerRatingDto> OwnerRatings
        {
            get { return ownerRatings; }
            set
            {
                ownerRatings = value;
                OnPropertyChanged(nameof(ownerRatings));
            }
        }




    }
}
