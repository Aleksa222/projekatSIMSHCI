using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    public class ReservationsViewModel : ViewModelBase
    {
        private RelayCommand goToRescheduleRequestCommand;

        AccommodationReservationService reservationsService;
        UserService userService;
        private ObservableCollection<AccommodationReservation> reservations;
        private ObservableCollection<Accommodation> accommodations;
        AccommodationService accommodationService;
        User owner;



        public ReservationsViewModel() : base()
        {
            reservationsService = new AccommodationReservationService();
            accommodationService = new AccommodationService();
            userService = new UserService();

            owner = userService.GetLoginUser();
            accommodations = accommodationService.GetAccommodationsByOwnerId(owner.Id);

            reservations = reservationsService.GetReservationsByOwner(accommodations);

        }

        public ObservableCollection<AccommodationReservation> Reservations
        {
            get { return reservations; }
            set
            {
                reservations = value;
                OnPropertyChanged(nameof(reservations));
            }
        }

        public RelayCommand GoToRescheduleRequestCommand
        {
            get
            {
                if (goToRescheduleRequestCommand == null)
                {
                    goToRescheduleRequestCommand = new RelayCommand(GoToRescheduleRequestPage);
                }
                return goToRescheduleRequestCommand;
            }
        }

        private void GoToRescheduleRequestPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                  new Uri("/UI/Dialogs/View/OwnerView/RescheduleRequestPage.xaml", UriKind.Relative));
        }

        public RelayCommand GoToRescheduleRequestPageCommand
        {
            get
            {
                return goToRescheduleRequestCommand ?? (goToRescheduleRequestCommand = new RelayCommand(GoToRescheduleRequestPage));
            }
        }

    }
}
