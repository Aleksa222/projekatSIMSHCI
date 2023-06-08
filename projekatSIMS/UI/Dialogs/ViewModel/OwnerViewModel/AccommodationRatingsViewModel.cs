using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Model.ModelDto;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    internal class AccommodationRatingsViewModel : ViewModelBase
    {
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private UserService userService;
        private ObservableCollection<AccommodationOwnerRating> ratings;
       

        private ObservableCollection<OwnerRatingDto> ownerRatings;

        private User owner;

        public SeriesCollection LevelData { get; set; }
        public List<string> LevelLabels { get; set; }
        public List<double> XAxisLabels { get; set; }
        public SeriesCollection ChartSeries { get; set; }
        public AccommodationRatingsViewModel() : base()
        {

            accommodationOwnerRatingService = new AccommodationOwnerRatingService();
            userService = new UserService();
            owner = userService.GetLoginUser();
            ratings = accommodationOwnerRatingService.GetOwnerRatingsById(owner.Id);

            ownerRatings = new ObservableCollection<OwnerRatingDto>();
            foreach (var rating in ratings)
            {
                // pronalaženje gosta na osnovu ID-a
                User guest = (User)userService.Get(rating.GuestId);
                // kreiranje novog DTO objekta i dodavanje u listu
                ownerRatings.Add(new OwnerRatingDto
                {
                    GuestName = guest.FirstName,
                    GuestSurname = guest.LastName,
                    AccommodationName = rating.AccommodationName,
                    Comment = rating.Comment,
                    OwnerPoliteness = rating.OwnerPoliteness

                });
            }
            LevelData = new SeriesCollection();
            LevelLabels = new List<string> { };
            XAxisLabels = new List<double> { 5, 10, 15, 20 };


            var values = new ObservableCollection<ObservableValue>
        {
            new ObservableValue(5),
            new ObservableValue(8),
            new ObservableValue(1),
            new ObservableValue(11),
            new ObservableValue(2)
        };

            string hexColor = "#d2b48c"; // Beige - heksadecimalni zapis boje

            Color color = (Color)ColorConverter.ConvertFromString(hexColor);
            SolidColorBrush brush = new SolidColorBrush(color);

            LevelData.Add(new RowSeries
            {
                Title = "Level",
                Values = new ChartValues<ObservableValue>(values),
                Fill = brush,


            });

            ChartSeries = new SeriesCollection();

            // Unos podataka za 2019. godinu
            ChartSeries.Add(new LineSeries
            {
                Title = "2019",
                Values = new ChartValues<double> { 4.5, 3.8, 4.2, 4.6, 4.9 } // Unesite svoje ocene za 2019. godinu ovde
            });

            // Unos podataka za 2020. godinu
            ChartSeries.Add(new LineSeries
            {
                Title = "2020",
                Values = new ChartValues<double> { 4.7, 4.4, 4.8, 4.6, 4.5 } // Unesite svoje ocene za 2020. godinu ovde
            });

            // Unos podataka za 2021. godinu
            ChartSeries.Add(new LineSeries
            {
                Title = "2021",
                Values = new ChartValues<double> { 4.2, 4.5, 4.3, 4.7, 4.6 } // Unesite svoje ocene za 2021. godinu ovde
            });

            // Unos podataka za 2022. godinu
            ChartSeries.Add(new LineSeries
            {
                Title = "2022",
                Values = new ChartValues<double> { 4.8, 4.6, 4.9, 4.7, 4.8 } // Unesite svoje ocene za 2022. godinu ovde
            });

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
        public ObservableCollection<OwnerRatingDto> OwnerRatings
        {
            get { return ownerRatings; }
            set
            {
                ownerRatings = value;
                OnPropertyChanged(nameof(ownerRatings));
            }
        }



    }
}
