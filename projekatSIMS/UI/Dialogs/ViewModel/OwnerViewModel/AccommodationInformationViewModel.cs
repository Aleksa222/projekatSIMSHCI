using System;
using System.Collections.Generic;
using projekatSIMS.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatSIMS.CompositeComon;
using System.Runtime.Remoting.Contexts;
using projekatSIMS.Service;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Data;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    public class AccommodationInformationViewModel : ViewModelBase
    {
        public class AvailableRange
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
        public RelayCommand showDatesCommand { get; set; }


        private DateTime selectedStartDate;
        private DateTime selectedEndDate;
        private int duration;
        private string description;
        public ObservableCollection<AvailableRange> availableRangeList { get; set; }



        private ObservableCollection<Accommodation> accommodations;
        private ObservableCollection<Renovation> renovations;
        private ObservableCollection<Renovation> filterFinishedrenovations;
        private ObservableCollection<Renovation> filterNotStartedrenovations;
        private Accommodation selectedAccommodation;




        private readonly AccommodationService accommodationService;
        private readonly AccommodationReservationService accommodationReservationService;
        private readonly RenovationService renovationService;

        UserService userService;
        User owner;



        public AccommodationInformationViewModel() : base()
        {

            accommodations = new ObservableCollection<Accommodation>();
            accommodationService = new AccommodationService();
            accommodationReservationService = new AccommodationReservationService();
            renovationService = new RenovationService();

            LabelVisibility = Visibility.Collapsed;
            ListBoxVisibility = Visibility.Collapsed;
            TextBoxVisibility = Visibility.Collapsed;
            ButtonVisibility = Visibility.Collapsed;


            userService = new UserService();
            owner = userService.GetLoginUser();
            renovations = renovationService.GetRenovationsByOwner(owner.Id);
            LoadAccommodations();
            ShowAllRenovations();
           



            availableRangeList = new ObservableCollection<AvailableRange>();





        }

        private RelayCommand showRenovationsWithIsDoneTrueCommand { get; set; }
        private RelayCommand showRenovationsWithIsDoneFalseCommand { get; set; }
        private RelayCommand saveRenovation { get; set; }
        private RelayCommand cancelRenovation { get; set; }

        public RelayCommand ShowRenovationsIsDoneTrueCommand
        {
            get
            {
                if (showRenovationsWithIsDoneTrueCommand == null)
                {
                    showRenovationsWithIsDoneTrueCommand = new RelayCommand(ShowRenovationsWithIsDoneTrueExecute);
                }
                return showRenovationsWithIsDoneTrueCommand;
            }
        }

        public RelayCommand SaveRenovationCommand
        {
            get
            {
                if (saveRenovation == null)
                {
                    saveRenovation = new RelayCommand(SaveRenovationExecute);
                }
                return saveRenovation;
            }
        }

        public RelayCommand CancelRenovationCommand
        {
            get
            {
                if (cancelRenovation == null)
                {
                    cancelRenovation = new RelayCommand(CancelRenovationExecute);
                }
                return cancelRenovation;
            }
        }

        public RelayCommand SaveRenovationWithCommand
        {
            get
            {
                return saveRenovation ?? (saveRenovation = new RelayCommand(SaveRenovationExecute));
            }
        }

        public RelayCommand CancelRenovationWithCommand
        {
            get
            {
                return cancelRenovation ?? (cancelRenovation = new RelayCommand(CancelRenovationExecute));
            }
        }

        public void CancelRenovationExecute(object parameter)
        {
            Renovation selectedRenovation = parameter as Renovation;
            Renovation ren =(Renovation)renovationService.Get(selectedRenovation.Id);
            ren.IsCanceled = true;
            renovationService.Edit(ren);



        }


        public void SaveRenovationExecute(object parameter)
        {
            if (description == null)
            {
                ErrorMessage = "Please enter a description!";
            }
            if (selectedAvailableRange == null)
            {
                ErrorMessage = "Selecet date range!";
            }

            Accommodation accommodation = accommodationService.GetAccommodationByName(selectedAccommodation.Name);
            DateTime startDate = selectedAvailableRange.StartDate;
            DateTime endDate = selectedAvailableRange.EndDate;
            string desc = description;

           

            Renovation newRenovation = new Renovation();
            newRenovation.Description = desc;
            newRenovation.StartDate = startDate;
            newRenovation.EndDate = endDate;
            newRenovation.AccommodationName = accommodation.Name;
            newRenovation.IsDone = false;
            accommodation.RecentlyRenovated = true;
            accommodationService.Edit(accommodation);
            
            renovationService.Add(newRenovation);
            HideElements();
            ResetValuesExecute();
           



        }


        public RelayCommand ShowRenovationsIsDoneFalseCommand
        {
            get
            {
                if (showRenovationsWithIsDoneFalseCommand == null)
                {
                    showRenovationsWithIsDoneFalseCommand = new RelayCommand(ShowRenovationsWithIsDoneFalseExecute);
                }
                return showRenovationsWithIsDoneFalseCommand;
            }
        }
        public RelayCommand ShowRenovationsWithIsDoneTrueCommand
        {
            get
            {
                return showRenovationsWithIsDoneTrueCommand ?? (showRenovationsWithIsDoneTrueCommand = new RelayCommand(ShowRenovationsWithIsDoneTrueExecute));
            }
        }

        public RelayCommand ShowRenovationsWithIsDoneFalseCommand
        {
            get
            {
                return showRenovationsWithIsDoneFalseCommand ?? (showRenovationsWithIsDoneFalseCommand = new RelayCommand(ShowRenovationsWithIsDoneFalseExecute));
            }
        }


        public void ShowRenovationsWithIsDoneTrueExecute(object parameter)
        {
            AllRenovations = new CompositeCollection();
            AllRenovations.Add(new CollectionContainer { Collection = renovations.Where(r => r.IsDone == true) });

        }
        private CompositeCollection allRenovations;
        public CompositeCollection AllRenovations
        {
            get { return allRenovations; }
            set
            {
                allRenovations = value;
                OnPropertyChanged(nameof(AllRenovations));
            }
        }
        public void ShowRenovationsWithIsDoneFalseExecute(object parameter)
        {
            AllRenovations = new CompositeCollection();
            AllRenovations.Add(new CollectionContainer { Collection = renovations.Where(r => r.IsDone == false) });
        }

        public void ResetValuesExecute()
        {
            SelectedStartDate = DefaultStartDate;
            SelectedEndDate = DefaultEndDate;
            Duration = DefaultDuration;
        }

        private DateTime defaultStartDate = DateTime.Today;
        public DateTime DefaultStartDate
        {
            get { return defaultStartDate; }
            set
            {
                defaultStartDate = value;
                OnPropertyChanged(nameof(DefaultStartDate));
            }
        }

        private DateTime defaultEndDate = DateTime.Today;
        public DateTime DefaultEndDate
        {
            get { return defaultEndDate; }
            set
            {
                defaultEndDate = value;
                OnPropertyChanged(nameof(DefaultEndDate));
            }
        }

        private int defaultDuration = 0;
        public int DefaultDuration
        {
            get { return defaultDuration; }
            set
            {
                defaultDuration = value;
                OnPropertyChanged(nameof(DefaultDuration));
            }
        }












        private void ShowAvailableDates(object obj)
        {
            if (selectedAccommodation == null)
            {
                MessageBox.Show("Choose accommodation!");
            }
            if (selectedStartDate == null || selectedStartDate < DateTime.Today || selectedStartDate > selectedEndDate || selectedEndDate == null || selectedEndDate < DateTime.Today)
            {
                MessageBox.Show("Please choose valid dates!");
            }

            if (duration == 0)
            {
                MessageBox.Show("Choose  duration!");
            }

            Accommodation accommodation = accommodationService.GetAccommodationByName(selectedAccommodation.Name);
            DateTime startDate = selectedStartDate;
            DateTime endDate = selectedEndDate;
            ObservableCollection<DateTime> availableDates = new ObservableCollection<DateTime>();
            ObservableCollection<AvailableRange> availableRangeList = new ObservableCollection<AvailableRange>();
            int dur = duration;

           
            

                ObservableCollection<AccommodationReservation> reservations = new ObservableCollection<AccommodationReservation>();
            reservations = accommodationReservationService.GetReservationsByAccommodationAndDateRange(startDate, endDate, accommodation.Name);
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                availableDates.Add(date);
            }

            int maxIndex = availableDates.Count - dur; // Maksimalni indeks za koji možemo formirati validan opseg datuma


            for (int i = 0; i <= maxIndex; i++)
            {
                bool isRangeAvailable = true;
                DateTime startDate1 = availableDates[i];
                DateTime endDate1 = startDate1.AddDays(duration - 1);

               
                    for (int j = i; j <= i + duration - 1; j++)
                    {
                        if (reservations.Any(r => availableDates[j] >= r.StartDate && availableDates[j] <= r.EndDate))
                        {
                            isRangeAvailable = false;
                            break;
                        }
                    }

                    if (isRangeAvailable)
                    {
                        AvailableRange range = new AvailableRange();
                        range.StartDate = startDate1;
                        range.EndDate = endDate1;
                        availableRangeList.Add(range);


                    
                }
            }

                AvailableRangeList = availableRangeList;
                ShowElements();
            

            
        
    


}

        
        public ObservableCollection<AvailableRange> AvailableRangeList
        {
            get { return availableRangeList; }
            set
            {
                availableRangeList = value;
                OnPropertyChanged(nameof(AvailableRangeList));
            }
        }

        public RelayCommand showAvailableDatesCommand { get; private set; }

        public ObservableCollection<Accommodation> Accommodations
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
                OnPropertyChanged(nameof(Accommodations)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public RelayCommand ShowAvailableDatesCommand
        {
            get
            {
                return showAvailableDatesCommand ?? (showAvailableDatesCommand = new RelayCommand(ShowAvailableDates));
            }
        }

        public RelayCommand ShowAvailableDatesWithCommand
        {
            get
            {
                if (showAvailableDatesCommand == null)
                {
                    showAvailableDatesCommand = new RelayCommand(ShowAvailableDates);
                }
                return showAvailableDatesCommand;
            }
        }

        private AvailableRange selectedAvailableRange;
        public AvailableRange SelectedAvailableRange
        {
            get { return selectedAvailableRange; }
            set
            {
                selectedAvailableRange = value;
                OnPropertyChanged(nameof(SelectedAvailableRange));
            }
        }
 



        public DateTime SelectedStartDate
        {
            get { return selectedStartDate; }
            set
            {
                selectedStartDate = value;
                OnPropertyChanged(nameof(SelectedStartDate)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public Accommodation SelectedAccommodation
        {
            get { return selectedAccommodation; }
            set
            {
                selectedAccommodation = value;
                OnPropertyChanged(nameof(SelectedAccommodation)); // podiže obaveštenje o promeni vrednosti

            }
        }

    
        public DateTime SelectedEndDate
        {
            get { return selectedEndDate; }
            set
            {
                selectedEndDate = value;
                OnPropertyChanged(nameof(SelectedEndDate)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public ObservableCollection<Renovation> Renovations
        {
            get { return renovations; }
            set
            {
                renovations = value;
                OnPropertyChanged(nameof(Renovations)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public ObservableCollection<Renovation> FilterFinishedRenovations
        {
            get { return filterFinishedrenovations; }
            set
            {
                filterFinishedrenovations = value;
                OnPropertyChanged(nameof(FilterFinishedRenovations)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public ObservableCollection<Renovation> FilterNotStartedRenovations
        {
            get { return filterNotStartedrenovations; }
            set
            {
                filterNotStartedrenovations = value;
                OnPropertyChanged(nameof(FilterNotStartedRenovations)); // podiže obaveštenje o promeni vrednosti

            }
        }

        public void ShowAllRenovations()
        {
            AllRenovations = new CompositeCollection();
            AllRenovations.Add(new CollectionContainer() { Collection = renovations });
        }



        private void LoadAccommodations()
        {
            // Učitaj podatke iz baze podataka i dodaj ih u listu smeštaja
            foreach (var accommodation in accommodationService.GetAccommodationsByOwnerId(owner.Id))
            {
                accommodations.Add(accommodation);
            }


        }

        private Visibility labelVisibility;
        public Visibility LabelVisibility
        {
            get { return labelVisibility; }
            set
            {
                labelVisibility = value;
                OnPropertyChanged(nameof(LabelVisibility));
            }
        }

        private Visibility listBoxVisibility;
        public Visibility ListBoxVisibility
        {
            get { return listBoxVisibility; }
            set
            {
                listBoxVisibility = value;
                OnPropertyChanged(nameof(ListBoxVisibility));
            }
        }

        private Visibility textBoxVisibility;
        public Visibility TextBoxVisibility
        {
            get { return textBoxVisibility; }
            set
            {
                textBoxVisibility = value;
                OnPropertyChanged(nameof(TextBoxVisibility));
            }
        }

        private Visibility buttonVisibility;
        public Visibility ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        public ICommand ShowElementsCommand { get; private set; }

       

        private void ShowElements()
        {
            // Promenite vidljivost elemenata na "Visible"
            LabelVisibility = Visibility.Visible;
            ListBoxVisibility = Visibility.Visible;
            TextBoxVisibility = Visibility.Visible;
            ButtonVisibility = Visibility.Visible;
        }

        private void HideElements()
        {
            // Promenite vidljivost elemenata na "Hidden"
            LabelVisibility = Visibility.Hidden;
            ListBoxVisibility = Visibility.Hidden;
            TextBoxVisibility = Visibility.Hidden;
            ButtonVisibility = Visibility.Hidden;
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }


        /*
         private void SomeMethod(object parameter)
{
    DateTime selectedDate = SelectedDate;
    // Koristite izabrani datum kako vam je potrebno
}*/
    }
}
