using Microsoft.Win32;
using projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel;
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

namespace projekatSIMS.UI.Dialogs.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourGuideTourRequestsPageView.xaml
    /// </summary>
    public partial class TourGuideTourRequestsPageView : Page
    {
        public TourGuideTourRequestsPageView()
        {
            InitializeComponent();
            DataContext = new TourGuideTourRequestsPageModel();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;
            if(openDialog.ShowDialog()==true)
            {
                //imagePicture.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }
    }
}
