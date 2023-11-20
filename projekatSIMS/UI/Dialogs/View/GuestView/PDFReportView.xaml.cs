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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View.GuestView
{
    /// <summary>
    /// Interaction logic for PDFReportView.xaml
    /// </summary>
    public partial class PDFReportView : UserControl
    {
        private AccommodationService accommodationService = new AccommodationService();
        private AccommodationReservationService accommodationReservationService = new AccommodationReservationService();

        public PDFReportView(AccommodationReservation reservation)
        {
            InitializeComponent();

            // Popunite TextBox polja sa vrednostima iz rezervacije
            AccommodationNameTextBox.Text = reservation.AccommodationName;
            StartDateTextBox.Text = reservation.StartDate.ToString("dd.MM.yyyy");
            EndDateTextBox.Text = reservation.EndDate.ToString("dd.MM.yyyy");
            GuestCountTextBox.Text = reservation.GuestCount.ToString();
        }
    }

}
