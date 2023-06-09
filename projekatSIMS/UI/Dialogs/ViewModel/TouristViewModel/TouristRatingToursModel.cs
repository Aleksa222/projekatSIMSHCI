using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristRatingToursModel : ViewModelBase
    {
        private RelayCommand backCommand;
        private RelayCommand submitCommand;
        private RelayCommand toursMoveDownCommand;
        private RelayCommand toursMoveUpCommand;
        private RelayCommand uploadCommand;
        private RelayCommand helpCommand;

        private ObservableCollection<Tour> items = new ObservableCollection<Tour>();
        private Tour selectedTour;

        private int touristId;
        private int tourGuideKnowledge;
        private int tourGuideLanguageProficiency;
        private int interestLevel;
        private string comment;
        private string imageUrl;

        private TourService tourService;
        private KeyPointsService keyPointsService;
        private TourReservationService reservationService;
        private UserService userService;
        private TourRatingService ratingService;
        public TouristRatingToursModel()
        {
            SetService(); 
            LoadData();
        }

        #region LOADING IN THE DATA
        //FIRST WE LOAD IN ALL THE TOURS THAT THE GUEST HAS A RESERVATION FOR AND HASN`T YET RATED

        private void LoadData()
        {
            InitialDataGridLoad();
            FilterDataGrid();
        }
        private void InitialDataGridLoad()
        {
            foreach(Tour tour in tourService.GetAll())
            {
                if(reservationService.GetReservationByGuestId(userService.GetLoginUser().Id).Contains(tour.Id) && !ratingService.GetRatedTours(userService.GetLoginUser().Id).Contains(tour.Id))
                {
                    Items.Add(tour);
                }
            }
        }

        //THEN WE FILTER OUT THE ONES THAT ARE NOT YET ENDED
        private void FilterDataGrid()
        {
            foreach(Tour tour in Items.ToList())
            {
                int[] keyPoints = tourService.GetTourKeypoints(tour.Id);
                if(!keyPointsService.CheckIfKeyPointsPassed(keyPoints))
                {
                    Items.Remove(tour);
                }
                
            }
        }
        #endregion

        #region COMMANDS
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("TouristRatingToursView.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }
        private void BackCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristActiveToursView.xaml", UriKind.Relative));
        }
        private void HelpCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/RateHelp.xaml", UriKind.Relative));
        }

        private void SubmitCommandExecute()
        {
            if (!CanSubmitCommandExecute())
            {
                MessageBox.Show("Please fill out all the necessary fields and respect the restrictions.","!!!", MessageBoxButton.OK);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Your rating was registered successfuly.", "Thank you. We appreciate it!", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristHomeView.xaml", UriKind.Relative));
            }
        }

        public void UploadCommandExecute()
        {
            if(selectedTour == null)
            {
                MessageBox.Show("Please select a tour first!", " ", MessageBoxButton.OK);
                return;
            }
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "PNG (.png)|*.png";
            bool? response = fileDialog.ShowDialog();
            if (response == true)
            {
                string filepath = fileDialog.FileName;
                string destinationFolder = @"C:\Users\Aleksa\Desktop\TouristTourImages";
                string destinationPath = System.IO.Path.Combine(destinationFolder, System.IO.Path.GetFileName(filepath));

                try
                {
                    System.IO.File.Copy(filepath, destinationPath, true);
                    MessageBox.Show("Image saved successfully!", "", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving picture: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public bool CanSubmitCommandExecute()
        {
            if (!IsTourguideKnowledgeValid()) return false;
            if(!IsTourguideLanguageProficiencyValid()) return false;
            if(!IsOverallInterestLevelValid()) return false;
            CreateRating();
            return true;
        }

        public bool IsTourguideKnowledgeValid()
        {
            if(TourGuideKnowledge < 0 ||  TourGuideKnowledge > 5 || TourGuideKnowledge == null) return false;
            return true;
        }

        public bool IsTourguideLanguageProficiencyValid()
        {
            if(TourGuideLanguageProficiency < 0 || TourGuideLanguageProficiency > 5 || TourGuideLanguageProficiency == null) return false;
            return true;
        }

        public bool IsOverallInterestLevelValid()
        {
            if (InterestLevel < 0 || InterestLevel > 5 || InterestLevel == null) return false;
            return true;
        }

        private void CreateRating()
        {
            if(selectedTour == null)
            {
                MessageBox.Show("Please select a tour first!", " ", MessageBoxButton.OK);
                return;
            }
            TourRating tourRating = new TourRating(ratingService.GenerateId(), SelectedTour.Id, userService.GetLoginUser().Id, TourGuideKnowledge, TourGuideLanguageProficiency, InterestLevel, Comment);
            ratingService.Add(tourRating);
        }
        private void ToursMoveDownCommandExecute()
        {
            int selectedIndex = Items.IndexOf(SelectedTour);
            if (selectedIndex < Items.Count - 1)
            {
                SelectedTour = Items[selectedIndex + 1];
            }
        }
        private void ToursMoveUpCommandExecute()
        {
            int selectedIndex = Items.IndexOf(SelectedTour);
            if (selectedIndex > 0)
            {
                SelectedTour = Items[selectedIndex - 1];
            }
        }
        public void SetService()
        {
            tourService = new TourService();
            keyPointsService = new KeyPointsService();
            reservationService = new TourReservationService();
            userService = new UserService();
            ratingService = new TourRatingService();
        }

        #endregion

        #region PROPERTIES
        public ObservableCollection<Tour> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        public Tour SelectedTour
        {
            get { return selectedTour; }
            set
            {
                selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }
        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ?? (backCommand = new RelayCommand(param => BackCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand SubmitCommand
        {
            get
            {
                return submitCommand ?? (submitCommand= new RelayCommand(param => SubmitCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand UploadCommand
        {
            get
            {
                return uploadCommand ?? (uploadCommand = new RelayCommand(param =>  UploadCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public int TouristId
        {
            get { return touristId; }
            set
            {
                touristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public int TourGuideKnowledge
        {
            get { return tourGuideKnowledge; }
            set
            {
                tourGuideKnowledge = value;
                OnPropertyChanged(nameof(TourGuideKnowledge));
            }
        }

        public int TourGuideLanguageProficiency
        {
            get { return tourGuideLanguageProficiency; }
            set
            {
                tourGuideLanguageProficiency = value;
                OnPropertyChanged(nameof(TourGuideLanguageProficiency));
            }
        }

        public int InterestLevel
        {
            get { return interestLevel; }
            set
            {
                interestLevel = value;
                OnPropertyChanged(nameof(InterestLevel));
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        public RelayCommand ToursMoveDownCommand
        {
            get
            {
                return toursMoveDownCommand ?? (toursMoveDownCommand = new RelayCommand(param => ToursMoveDownCommandExecute(), param => CanThisCommandExecute()));
            }
        }

        public RelayCommand ToursMoveUpCommand
        {
            get
            {
                return toursMoveUpCommand ?? (toursMoveUpCommand = new RelayCommand(param => ToursMoveUpCommandExecute(), param => CanThisCommandExecute()));
            }
        }
        public RelayCommand HelpCommand
        {
            get
            {
                if (helpCommand == null)
                {
                    helpCommand = new RelayCommand(param => HelpCommandExecute(), param => CanThisCommandExecute());
                }

                return helpCommand;
            }
        }


        #endregion
    }
}
