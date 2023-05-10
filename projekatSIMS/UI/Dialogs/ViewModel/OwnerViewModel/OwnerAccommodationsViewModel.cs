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

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    internal class OwnerAccommodationsViewModel : ViewModelBase
    {
        AccommodationService accommodationService;
        UserService userService;
        private ObservableCollection<Accommodation> accommodations;
        private ObservableCollection<AccommodationDto> ownerAccommodationsDto;

        User owner;

        public OwnerAccommodationsViewModel() : base()
        {
            accommodations = new ObservableCollection<Accommodation>();
            accommodationService = new AccommodationService();
            userService = new UserService();

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

    }
}
