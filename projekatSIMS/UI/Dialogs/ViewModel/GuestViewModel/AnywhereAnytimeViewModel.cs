using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.GuestView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace projekatSIMS.UI.Dialogs.ViewModel.GuestViewModel
{
    public class AnywhereAnytimeViewModel : ViewModelBase
    {
        public ICommand SearchCommand { get; set; }
        private ObservableCollection<Accommodation> accommodationItems = new ObservableCollection<Accommodation>();
        private AccommodationService accommodationService = new AccommodationService();
        private AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

        public AnywhereAnytimeViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(1);
            SearchCommand = new RelayCommand(SearchAccommodations);
            LoadAccommodationItems();
        }

        public ObservableCollection<Accommodation> AccommodationItems
        {
            get { return accommodationItems; }
            set
            {
                accommodationItems = value;
                OnPropertyChanged(nameof(accommodationItems));
            }
        }
        private int guestCount;
        public int GuestCount
        {
            get { return guestCount; }
            set
            {
                guestCount = value;
                OnPropertyChanged(nameof(GuestCount));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
                UpdateAvailableDates();
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
                UpdateAvailableDates();
            }
        }

        private int numberOfDays;
        public int NumberOfDays
        {
            get { return numberOfDays; }
            set
            {
                numberOfDays = value;
                OnPropertyChanged(nameof(NumberOfDays));
                UpdateAvailableDates();
            }
        }

        private bool isDateRangeSelected;
        public bool IsDateRangeSelected
        {
            get { return isDateRangeSelected; }
            set
            {
                isDateRangeSelected = value;
                OnPropertyChanged(nameof(IsDateRangeSelected));
                UpdateAvailableDates();
            }
        }

        private ObservableCollection<AvailableDate> availableDates = new ObservableCollection<AvailableDate>();
        public ObservableCollection<AvailableDate> AvailableDates
        {
            get { return availableDates; }
            set
            {
                availableDates = value;
                OnPropertyChanged(nameof(AvailableDates));
            }
        }

        private AvailableDate selectedDate;
        public AvailableDate SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private Accommodation selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation));
            }
        }

        private void LoadAccommodationItems()
        {
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                AccommodationItems.Add(accommodation);
            }
        }

        private void UpdateAvailableDates()
        {
            AvailableDates.Clear();

            if (IsDateRangeSelected && StartDate != null && EndDate != null && NumberOfDays > 0)
            {
                foreach (var accommodation in AccommodationItems)
                {
                    if (IsAccommodationAvailable(accommodation))
                    {
                        DateTime currentDate = StartDate;

                        while (currentDate <= EndDate.AddDays(-NumberOfDays))
                        {
                            DateTime reservationEndDate = currentDate.AddDays(NumberOfDays);

                            if (IsAccommodationAvailableInDateRange(accommodation, currentDate, reservationEndDate))
                            {
                                AvailableDates.Add(new AvailableDate
                                {
                                    AvailableStartDate = currentDate,
                                    AvailableEndDate = reservationEndDate,
                                    AccommodationName = accommodation.Name
                                });
                            }

                            currentDate = currentDate.AddDays(1);
                        }
                    }
                }
            }
        }

        private bool IsAccommodationAvailable(Accommodation accommodation)
        {
            foreach (AccommodationReservation accommodationReservation in accommodationReservationService.GetAll())
            {
                if (accommodationReservation.AccommodationName == accommodation.Name)
                {
                    if (accommodationReservation.StartDate <= EndDate && accommodationReservation.EndDate >= StartDate)
                    {
                        return false; // Smeštaj je zauzet u nekom od datuma
                    }
                }
            }
            return accommodation.GuestLimit >= GuestCount;
        }
        private bool IsAccommodationAvailableInDateRange(Accommodation accommodation, DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue && endDate == DateTime.MinValue)
            {
                return true; // Smeštaj je uvek dostupan
            }

            foreach (AccommodationReservation accommodationReservation in accommodationReservationService.GetAll())
            {
                if (accommodationReservation.AccommodationName == accommodation.Name)
                {
                    if (accommodationReservation.StartDate <= endDate && accommodationReservation.EndDate >= startDate)
                    {
                        return false; // Smeštaj je zauzet u nekom od datuma
                    }
                }
            }

            return true; // Smeštaj je dostupan za dati opseg datuma
        }

        private string reservationSuccessfulLabel;
        public string ReservationSuccessfulLabel
        {
            get { return reservationSuccessfulLabel; }
            set
            {
                reservationSuccessfulLabel = value;
                OnPropertyChanged(nameof(ReservationSuccessfulLabel));
            }
        }


        private void SearchAccommodations(object parameter)
        {
            if (GuestCount <= 0 || NumberOfDays <= 0)
            {
                ReservationSuccessfulLabel = "You must enter the number of guests and the number of days";
                return;
            }

            AvailableDates.Clear();

            if (StartDate == DateTime.MinValue && EndDate == DateTime.MinValue)
            {
                // Pretražuj smeštaje koji su slobodni bilo kada za zadati broj ljudi i broj dana
                foreach (var accommodation in AccommodationItems)
                {
                    if (accommodation.GuestLimit >= GuestCount)
                    {
                        AvailableDates.Add(new AvailableDate
                        {
                            AvailableStartDate = DateTime.MinValue,
                            AvailableEndDate = DateTime.MinValue,
                            AccommodationName = accommodation.Name
                        });

                    }
                }
            }
            else
            {
                // Pretražuj smeštaje za zadati opseg datuma
                foreach (var accommodation in AccommodationItems)
                {
                    if (accommodation.GuestLimit >= GuestCount)
                    {
                        DateTime currentDate = StartDate;

                        while (currentDate <= EndDate.AddDays(-NumberOfDays))
                        {
                            DateTime reservationEndDate = currentDate.AddDays(NumberOfDays);

                            if (IsAccommodationAvailableInDateRange(accommodation, currentDate, reservationEndDate))
                            {
                                AvailableDates.Add(new AvailableDate
                                {
                                    AvailableStartDate = currentDate,
                                    AvailableEndDate = reservationEndDate,
                                    AccommodationName = accommodation.Name
                                });
                            }

                            currentDate = currentDate.AddDays(1);
                        }

                    }
                }
            }

            AccommodationItems.Clear();

            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                if (accommodation.GuestLimit >= GuestCount)
                {
                    if (IsDateRangeSelected && StartDate != null && EndDate != null && NumberOfDays > 0)
                    {
                        if (IsAccommodationAvailableInDateRange(accommodation, StartDate, EndDate.AddDays(-NumberOfDays)))
                        {
                            AccommodationItems.Add(accommodation);
                        }
                    }
                    else
                    {
                        AccommodationItems.Add(accommodation);
                    }
                }
            }

        }

        public ICommand ShowAnywhereAnytimeHelpCommand { get; set; }
        public ICommand BackCommand { get; set; }
        private UserControl _selectedView;

        public UserControl SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        // BackCommand = new RelayCommand(BackControl);
        // ShowAnywhereAnytimeHelpCommand = new RelayCommand(ShowAnywhereAnytimeHelpControl);

    }

}
