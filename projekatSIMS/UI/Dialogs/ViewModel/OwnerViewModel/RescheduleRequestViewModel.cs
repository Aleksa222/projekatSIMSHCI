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
    public class RescheduleRequestViewModel : ViewModelBase
    {
        private RelayCommand goToReservationsCommand;

        ReservationRescheduleRequestService requestsService;
        AccommodationReservationService reservationsService;
        UserService userService;
        private ObservableCollection<ReservationRescheduleRequest> requests;
        private ObservableCollection<AccommodationReservation> reservations;
        private ObservableCollection<Accommodation> accommodations;
        AccommodationService accommodationService;
        User owner;



        public RescheduleRequestViewModel() : base()
        {
            requestsService  = new ReservationRescheduleRequestService();
            reservationsService = new AccommodationReservationService();
            accommodationService = new AccommodationService();
            userService = new UserService();

            owner = userService.GetLoginUser();
            accommodations = accommodationService.GetAccommodationsByOwnerId(owner.Id);

           
     
            reservations = reservationsService.GetReservationsByOwner(accommodations);

        }
        public RelayCommand GoToReservationsCommand
        {
            get
            {
                if (goToReservationsCommand == null)
                {
                    goToReservationsCommand = new RelayCommand(GoToReservationsPage);
                }
                return goToReservationsCommand;
            }
        }
        private void GoToReservationsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                  new Uri("/UI/Dialogs/View/OwnerView/ReservationsPage.xaml", UriKind.Relative));
        }

        public RelayCommand GoToReservationsPageCommand
        {
            get
            {
                return goToReservationsCommand ?? (goToReservationsCommand = new RelayCommand(GoToReservationsPage));
            }
        }

        public ObservableCollection<ReservationRescheduleRequest> Requests
        {
            get { return requests; }
            set
            {
                requests = value;
                OnPropertyChanged(nameof(requests));
            }
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
    }
}
