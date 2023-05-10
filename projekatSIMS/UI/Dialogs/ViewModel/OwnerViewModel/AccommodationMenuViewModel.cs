using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.OwnerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    internal class AccommodationMenuViewModel : ViewModelBase
    {
        private RelayCommand goToAccommodationInfoPageCommand;
        private RelayCommand goToAccommodationStatisticsPageCommand;
        private RelayCommand goToAccommodationReservationsPageCommand;
        private RelayCommand goToAccommodationRenovationsPageCommand;
        private RelayCommand goToAccommodationRatingsPageCommand;
        

        public AccommodationMenuViewModel()
        {

        }

        public RelayCommand GoToAccommodationInfoCommand
        {
            get
            {
                if (goToAccommodationInfoPageCommand == null)
                {
                    goToAccommodationInfoPageCommand = new RelayCommand(GoToAccommodationInfoPage);
                }
                return goToAccommodationInfoPageCommand;
            }
        }

        public RelayCommand GoToAccommodationStatisticsCommand
        {
            get
            {
                if (goToAccommodationStatisticsPageCommand == null)
                {
                    goToAccommodationStatisticsPageCommand = new RelayCommand(GoToAccommodationStatisticsPage);
                }
                return goToAccommodationStatisticsPageCommand;
            }
        }

      
        public RelayCommand GoToAccommodationReservationsCommand
        {
            get
            {
                if (goToAccommodationReservationsPageCommand == null)
                {
                    goToAccommodationReservationsPageCommand = new RelayCommand(GoToAccommodationReservationsPage);
                }
                return goToAccommodationReservationsPageCommand;
            }
        }

        public RelayCommand GoToAccommodationRatingsCommand
        {
            get
            {
                if (goToAccommodationRatingsPageCommand == null)
                {
                    goToAccommodationRatingsPageCommand = new RelayCommand(GoToAccommodationRatingsPage);
                }
                return goToAccommodationRatingsPageCommand;
            }
        }

        public RelayCommand GoToAccommodationRenovationsCommand
        {
            get
            {
                if (goToAccommodationRenovationsPageCommand == null)
                {
                    goToAccommodationRenovationsPageCommand = new RelayCommand(GoToAccommodationRenovationsPage);
                }
                return goToAccommodationRenovationsPageCommand;
            }
        }


        private void GoToAccommodationInfoPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/OwnerHomeView.xaml", UriKind.Relative));
        }

        private void GoToAccommodationStatisticsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/OwnerView/OwnerUserAccountView.xaml", UriKind.Relative));
        }

        private void GoToAccommodationRenovationsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("/UI/Dialogs/View/OwnerView/OwnerAccommodationPage.xaml", UriKind.Relative));
        }

        private void GoToAccommodationReservationsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("/UI/Dialogs/View/OwnerView/OwnerAccommodationPage.xaml", UriKind.Relative));
        }

        private void GoToAccommodationRatingsPage(object parameter)
        {
            OwnerMainWindow.navigationService.Navigate(
                new Uri("/UI/Dialogs/View/OwnerView/OwnerAccommodationPage.xaml", UriKind.Relative));
        }


        public RelayCommand GoToAccommodationInfoPageCommand
        {
            get
            {
                return goToAccommodationInfoPageCommand ?? (goToAccommodationInfoPageCommand = new RelayCommand(GoToAccommodationInfoPage));
            }
        }

        public RelayCommand GoToAccommodationStatisticsPageCommand
        {
            get
            {
                return goToAccommodationStatisticsPageCommand ?? (goToAccommodationStatisticsPageCommand = new RelayCommand(GoToAccommodationStatisticsPage));
            }
        }


        public RelayCommand GoToAccommodationRatingsPageCommand
        {
            get
            {
                return goToAccommodationRatingsPageCommand ?? (goToAccommodationRatingsPageCommand = new RelayCommand(GoToAccommodationRatingsPage));
            }
        }

        public RelayCommand GoToAccommodationReservationsPageCommand
        {
            get
            {
                return goToAccommodationReservationsPageCommand ?? (goToAccommodationReservationsPageCommand = new RelayCommand(GoToAccommodationReservationsPage));
            }
        }

        public RelayCommand GoToAccommodationRenovationsPageCommand
        {
            get
            {
                return goToAccommodationRenovationsPageCommand ?? (goToAccommodationRenovationsPageCommand = new RelayCommand(GoToAccommodationRenovationsPage));
            }
        }



    }
}


    

