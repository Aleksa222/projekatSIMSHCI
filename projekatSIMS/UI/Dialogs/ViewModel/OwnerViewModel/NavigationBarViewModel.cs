using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    internal class NavigationBarViewModel : ViewModelBase
    {
        private RelayCommand goToHomePageCommand;
        private RelayCommand goToUserAccountPageCommand;
        private RelayCommand goToAccommodationsPageCommand;
        private RelayCommand goToReservationsPageCommand;
        private RelayCommand goToInboxPageCommand;
        private RelayCommand logOutCommand;

        public NavigationBarViewModel()
        {

        }

        public RelayCommand GoToHomeCommand
        {
            get
            {
                if (goToHomePageCommand == null)
                {
                    goToHomePageCommand = new RelayCommand(GoToHomePage);
                }
                return goToHomePageCommand;
            }
        }

        public RelayCommand GoToReservationsCommand
        {
            get
            {
                if (goToReservationsPageCommand == null)
                {
                    goToReservationsPageCommand = new RelayCommand(GoToReservationsPage);
                }
                return goToHomePageCommand;
            }
        }

        public RelayCommand GoToUserAccountCommand
        {
            get
            {
                if (goToUserAccountPageCommand == null)
                {
                    goToUserAccountPageCommand = new RelayCommand(GoToUserAccountPage);
                }
                return goToUserAccountPageCommand;
            }
        }

        public RelayCommand LogOutCommand
        {
            get
            {
                if (logOutCommand == null)
                {
                    logOutCommand = new RelayCommand(LogOutPage);
                }
                return logOutCommand;
            }
        }

        public RelayCommand GoToAccommodationCommand
        {
            get
            {
                if (goToAccommodationsPageCommand == null)
                {
                    goToAccommodationsPageCommand = new RelayCommand(GoToAccommodationPage);
                }
                return goToAccommodationsPageCommand;
            }
        }
        public RelayCommand GoToInboxCommand
        {
            get
            {
                if (goToInboxPageCommand == null)
                {
                    goToInboxPageCommand = new RelayCommand(GoToInboxPage);
                }
                return goToInboxPageCommand;
            }
        }
        private void LogOutPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("/projekatSIMS;component/MainWindow.xaml", UriKind.Relative));
        }



        private void GoToHomePage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/AccommodationRatingsView.xaml", UriKind.Relative));
        }

        private void GoToReservationsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/ReservationsPage.xaml", UriKind.Relative));
        }
        private void GoToInboxPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/AccommodationRenovationView.xaml", UriKind.Relative));
        }

        private void GoToUserAccountPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/OwnerUserAccountView.xaml", UriKind.Relative));
        }

        private void GoToAccommodationPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("/UI/Dialogs/View/OwnerView/OwnerAccommodationPage.xaml", UriKind.Relative));
        }

        public RelayCommand GoToHomePageCommand
        {
            get
            {
                return goToHomePageCommand ?? (goToHomePageCommand = new RelayCommand(GoToHomePage));
            }
        }

        public RelayCommand GoToReservationsPageCommand
        {
            get
            {
                return goToReservationsPageCommand ?? (goToReservationsPageCommand = new RelayCommand(GoToReservationsPage));
            }
        }

        public RelayCommand GoToUserAccountPageCommand
        {
            get
            {
                return goToUserAccountPageCommand ?? (goToUserAccountPageCommand = new RelayCommand(GoToUserAccountPage));
            }
        }

        public RelayCommand GoToInboxPageCommand
        {
            get
            {
                return goToInboxPageCommand ?? (goToInboxPageCommand = new RelayCommand(GoToInboxPage));
            }
        }

        public RelayCommand LogOutPageCommand
        {
            get
            {
                return logOutCommand ?? (logOutCommand = new RelayCommand(LogOutPage));
            }
        }

        public RelayCommand GoToAccommodationPageCommand
        {
            get
            {
                return goToAccommodationsPageCommand ?? (goToAccommodationsPageCommand = new RelayCommand(GoToAccommodationPage));
            }
        }


    }
 }
