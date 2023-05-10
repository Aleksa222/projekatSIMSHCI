using System;
using projekatSIMS.Service;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projekatSIMS.CompositeComon;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using LiveCharts;
using System.Windows.Markup;
using projekatSIMS.Model;

namespace projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel
{
    public class GuestReviewChartViewModel : ViewModelBase
    {
        private ObservableCollection<int> ownerRatings;
        AccommodationOwnerRatingService accommodationOwnerRatingSevice;
        UserService userService;
        User owner;

        public GuestReviewChartViewModel()
        {
            //data = new ObservableCollection<int>(GetData());
            accommodationOwnerRatingSevice = new AccommodationOwnerRatingService();
            userService = new UserService();
            owner = userService.GetLoginUser();
            ownerRatings = new ObservableCollection<int>(GetData());
        }

        public SeriesCollection ChartSeries
        {
            get
            {
                var series = new SeriesCollection();
                series.Add(new LineSeries
                {
                    Title = "My Data",
                    Values = new ChartValues<int>(ownerRatings)
                });
                return series;
            }
        }

        private List<int> GetData()
        {
            List<int> retVal = new List<int>();
            ObservableCollection<AccommodationOwnerRating> ratings = accommodationOwnerRatingSevice.GetOwnerRatingsById(owner.Id);
            retVal = accommodationOwnerRatingSevice.GetOwnerRatings(ratings);
            return retVal;
        }
    }
}




