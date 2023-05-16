using projekatSIMS.CompositeComon;

using System;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatSIMS.Model.ModelDto;
using System.Windows.Input;
using System.ComponentModel;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    public class OwnerAccommodationsViewModel : ViewModelBase
    {
        private ICommand _showGridCommand;

        AccommodationService accommodationService;
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private ObservableCollection<string> accommodationNames;
        private AccommodationDto selectedAccommodation;
        private bool _isGridVisible;


        UserService userService;
        private ObservableCollection<Accommodation> accommodations;
        private ObservableCollection<AccommodationDto> ownerAccommodationsDto;
        private ObservableCollection<OwnerRatingDto> ownerRatings;
        private ObservableCollection<AccommodationOwnerRating> ratings;
       




        User owner;

        public OwnerAccommodationsViewModel() : base()
        {
           

            accommodations = new ObservableCollection<Accommodation>();
            accommodationService = new AccommodationService();
            userService = new UserService();
            AccommodationNames = new ObservableCollection<string>(accommodations.Select(a => a.Name));


            owner = userService.GetLoginUser();
            accommodations = accommodationService.GetAccommodationsByOwnerId(owner.Id);
            ownerAccommodationsDto = new ObservableCollection<AccommodationDto>();
            foreach (var accommodation in accommodations)
            {
                // pronalaženje gosta na osnovu ID-a

                // kreiranje novog DTO objekta i dodavanje u listu
                ownerAccommodationsDto.Add(new AccommodationDto
                {
                    Name = accommodation.Name,
                    LocationCityName = accommodation.Location.City,
                    LocationCountryName = accommodation.Location.Country,
                    GuestLimit = accommodation.GuestLimit,
                    CancellationDays = accommodation.CancellationDays,
                    ImageUrl = accommodation.ImageUrls[0]

                });

                ratings = new ObservableCollection<AccommodationOwnerRating>();
                accommodationOwnerRatingService = new AccommodationOwnerRatingService();



                ratings = accommodationOwnerRatingService.GetOwnerRatingsById(owner.Id);
                ownerRatings = new ObservableCollection<OwnerRatingDto>();
                foreach (var rating in ratings)
                {
                    // pronalaženje gosta na osnovu ID-a
                    User guest = (User)userService.Get(rating.GuestId);
                    // kreiranje novog DTO objekta i dodavanje u listu
                    ownerRatings.Add(new OwnerRatingDto
                    {
                        GuestName = guest.FirstName + " " + guest.LastName,
                        Cleanliness = rating.Cleanliness,
                        GuestImageUrl = guest.ImageUrl,
                        Comment = "Comment : " + rating.Comment,
                        OwnerPoliteness = rating.OwnerPoliteness


                    });
                }

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

        public ObservableCollection<Accommodation> Accommodations
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
                OnPropertyChanged(nameof(accommodations));
            }
        }

        public ObservableCollection<AccommodationDto> OwnerAccommodationsDto
        {
            get { return ownerAccommodationsDto; }
            set
            {
                ownerAccommodationsDto = value;
                OnPropertyChanged(nameof(ownerAccommodationsDto));
            }
        }

       
        public ObservableCollection<string> AccommodationNames
        {
            get { return accommodationNames; }
            set
            {
                accommodationNames = value;
                OnPropertyChanged(nameof(AccommodationNames));
            }
        }

        public AccommodationDto SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }
        public bool IsGridVisible
        {
            get { return _isGridVisible; }
            set
            {
                if (_isGridVisible != value)
                {
                    _isGridVisible = value;
                    OnPropertyChanged(nameof(IsGridVisible));
                }
            }
        }

        public void ShowGrid()
        {
            IsGridVisible = true;
        }
       

    }
}
