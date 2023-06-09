using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristTourRequestModel : ViewModelBase
    {
        private RelayCommand submitCommand;
        private RelayCommand openStartDatePicker;
        private RelayCommand openEndDatePicker;

        private string state;
        private string city;
        private string description;
        private string language;
        private int guestNumber;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime defaultStartDate = DateTime.Today;

        private TourRequestService tourRequestService;
        private UserService userService;

        private bool isStartDatePickerOpened;
        private bool isEndDatePickerOpened;

        public TouristTourRequestModel() {
            SetService();
        }

        public void SetService()
        {
            tourRequestService = new TourRequestService();
            userService = new UserService();
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristTourRequestView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }

        public void SubmitCommandExecute()
        {
            if (City == null || State == null || Language == null)
            {
                MessageBox.Show("Please fill all the necessary fields!", " ", MessageBoxButton.OK);
                return;
            }
            if (EndDate < StartDate)
            {
                MessageBox.Show("Please enter a valid date range!", " ", MessageBoxButton.OK);
                return;
            }
            if(GuestNumber <= 0)
            {
                MessageBox.Show("Please enter a valid guest number!", " ", MessageBoxButton.OK);
                return;
            }
            CreateRequest();
        }

        public void OpenStartDatePickerExecute()
        {
            IsStartDatePickerOpened = true;
        }

        public void OpenEndDatePickerExecute()
        {
            IsEndDatePickerOpened = true;
        }
        public void CreateRequest()
        {
            Location location = new Location();
            location.City = City;
            location.Country = State;
            TourRequest tourRequest = new TourRequest();
            tourRequest.Id = tourRequestService.GenerateId();
            tourRequest.GuestId = userService.GetLoginUser().Id;
            tourRequest.Location = location;
            tourRequest.Description = Description;
            tourRequest.Language = Language;
            tourRequest.StartDate = StartDate;
            tourRequest.EndDate = EndDate;
            tourRequest.GuestNumber = GuestNumber;
            tourRequest.Status = TourRequestStatus.PENDING;
            tourRequest.IsPartOfComplexTour = false;
            tourRequestService.Add(tourRequest);
            MessageBox.Show("Your request has been noted!", "Thanks", MessageBoxButton.OK);
        }


        public string State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged(nameof(State));
            }
        }

        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Language
        {
            get { return language;}
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
        public int GuestNumber
        {
            get { return guestNumber; }
            set { 
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public RelayCommand SubmitCommand
        {
            get
            {
                return submitCommand ?? (submitCommand = new RelayCommand(param => SubmitCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand OpenStartDatePickerCommand
        {
            get
            {
                return openStartDatePicker ?? (openStartDatePicker = new RelayCommand(param =>  OpenStartDatePickerExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand OpenEndDatePickerCommand
        {
            get
            {
                return openEndDatePicker ?? (openEndDatePicker = new RelayCommand(param => OpenEndDatePickerExecute(), param => CanThisCommandExecute()));
            }
        }

        public DateTime DefaultStartDate
        {
            get { return defaultStartDate; }
            set
            {
                defaultStartDate = value;
                OnPropertyChanged(nameof(DefaultStartDate));
            }
        }
        public bool IsStartDatePickerOpened
        {
            get { return isStartDatePickerOpened; }
            set
            {
                isStartDatePickerOpened = value;
                OnPropertyChanged(nameof(IsStartDatePickerOpened));
            }
        }
        public bool IsEndDatePickerOpened
        {
            get { return isEndDatePickerOpened; }
            set
            {
                isEndDatePickerOpened = value;
                OnPropertyChanged(nameof(IsEndDatePickerOpened));
            }
        }


    }
}
