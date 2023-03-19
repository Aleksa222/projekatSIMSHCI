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
        public GuestReviewView()
        {
                InitializeComponent();
               
        }

        private void LayGuestReviewButton_Click(object sender, RoutedEventArgs e)
        {
            

            GuestReviewService guestReviewService = new GuestReviewService();
            AccommodationReservationService accommodationReservationService = new AccommodationReservationService();
            DateTime endDate = new DateTime();
            AccommodationReservation ac = new AccommodationReservation();
            ac = (AccommodationReservation)accommodationReservationService.Get(int.Parse(ReservationId.Text));


            endDate = ac.EndDate;
            endDate = endDate.AddDays(5);
            guestReviewService.CheckDate(endDate);
            
                GuestReview guestReview = new GuestReview();
                guestReview.accommodationReservation = ac;
                guestReview.Cleanliness = int.Parse(CleanlinessTextBox.Text);
                guestReview.RespectingRules = int.Parse(RespectingRulesTextBox.Text);
                guestReview.Comment = CommentTextBox.Text;

                guestReviewService.Add(guestReview);
             
           



            


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

            AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

            foreach (AccommodationReservation entity in accommodationReservationService.GetAll())
            {
                ReservationsListBox.Items.Add(entity.Id + " " + entity.Accommodation.Id + " " +  entity.StartDate + " " + entity.EndDate + " " + entity.NumberOfGuests);
            }
        }
    }
}
