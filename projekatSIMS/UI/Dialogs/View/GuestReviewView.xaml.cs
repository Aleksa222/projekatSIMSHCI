using projekatSIMS.Model;
using projekatSIMS.Repository;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for GuestReviewView.xaml
    /// </summary>
    public partial class GuestReviewView : Window
    {
        AccommodationReservationService accommodationReservationService = new AccommodationReservationService();
        GuestReviewService guestReviewService = new GuestReviewService();
        public GuestReviewView()
        {
                InitializeComponent();
               
        }

        private void LayGuestReviewButton_Click(object sender, RoutedEventArgs e)
        {

            AccommodationReservation ac = (AccommodationReservation)accommodationReservationService.Get(int.Parse(ReservationId.Text));

            GuestReview guestReview = new GuestReview();
            guestReview.accommodationReservation = ac;
            //if(int.Parse(CleanlinessTextBox.Text) < 1 || int.Parse(CleanlinessTextBox.Text))
            guestReview.Cleanliness = int.Parse(CleanlinessTextBox.Text);
            guestReview.RespectingRules = int.Parse(RespectingRulesTextBox.Text);
            guestReview.Comment = CommentTextBox.Text;

            guestReviewService.PlaceGuestReview(guestReview);
            
     
            
        }

      

        private void CleanlinessTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RespectingRulesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CommentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ReservationId_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ReservationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

        private void ReservationsDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            ReservationsListBox.Items.Clear();

            foreach (AccommodationReservation entity in accommodationReservationService.GetAll())
            {
                ReservationsListBox.Items.Add(entity.Id + " " + entity.Accommodation.Id + " " +  entity.StartDate + " " + entity.EndDate + " " + entity.NumberOfGuests);
            }
        }
    }
}
