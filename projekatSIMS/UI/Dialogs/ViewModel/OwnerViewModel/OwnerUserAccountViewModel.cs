using LiveCharts;
using LiveCharts.Wpf;
using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Model.ModelDto;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    public class OwnerUserAccountViewModel : ViewModelBase
    {
        private AccommodationOwnerRatingService accommodationOwnerRatingService;
        private UserService userService;
        private ObservableCollection<AccommodationOwnerRating>ratings;
        private List<int> _dataPoints;
        public SeriesCollection ChartSeries { get; set; }

        



        private ObservableCollection<OwnerRatingDto> ownerRatings;
        public SeriesCollection DataSeries { get; set; }
        public List<string> Labels { get; set; }


        private User owner;

        public OwnerUserAccountViewModel() : base()
        {
            ratings = new ObservableCollection<AccommodationOwnerRating>();
            accommodationOwnerRatingService = new AccommodationOwnerRatingService();
            userService = new UserService();


            owner = userService.GetLoginUser();
            ratings = accommodationOwnerRatingService.GetOwnerRatingsById(owner.Id);
            _dataPoints = accommodationOwnerRatingService.GetOwnerRatings(ratings);
            ownerRatings = new ObservableCollection<OwnerRatingDto>();
            foreach (var rating in ratings)
            {
                // pronalaženje gosta na osnovu ID-a
                User guest = (User)userService.Get(rating.GuestId);
                // kreiranje novog DTO objekta i dodavanje u listu
                ownerRatings.Add(new OwnerRatingDto
                {
                    GuestName = guest.FirstName + " " + guest.LastName,
                    GuestImageUrl = guest.ImageUrl,
                    Comment = "Comment : " + rating.Comment,
                    OwnerPoliteness = rating.OwnerPoliteness


                }); ;

                var series = new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<int>(DataPoints),
                    PointGeometry = null,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent
                };

                ChartSeries = new SeriesCollection { series };
            

        }

    }
        public ObservableCollection<AccommodationOwnerRating> Ratings
        {
            get { return ratings; }
            set
            {
                ratings = value;
                OnPropertyChanged(nameof(ratings));
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


        public User Owner
        {
            get { return owner; }
            set
            {
                owner = value;
                OnPropertyChanged(nameof(owner));
                OnPropertyChanged(nameof(owner.FirstName));
                OnPropertyChanged(nameof(owner.LastName));
            }
        }

        public List<int> DataPoints
        {
            get { return _dataPoints; }
            set
            {
                _dataPoints = value;
                OnPropertyChanged(nameof(DataPoints));
            }
        }
    }
}
