using Microsoft.Win32;
using projekatSIMS.Model;
using projekatSIMS.UI.Dialogs.ViewModel.TourGuideViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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



        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files|*.bmp;*.jpg;*.png";
            openDialog.FilterIndex = 1;

            string destinationFolder = "C:\\Users\\Korisnik\\OneDrive - Univerzitet u Novom Sadu\\Desktop\\simshci\\projekatSIMSHCI\\projekatSIMS\\Resources\\Images\\TouristImages\\";

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    string imagePath = openDialog.FileName;
                    string fileName = System.IO.Path.GetFileName(imagePath);
                    string destinationPath = System.IO.Path.Combine(destinationFolder, fileName);
                    ImageSource = imagePath;
                    // Copy the image file to the destination folder
                    File.Copy(imagePath, destinationPath, true);
                    
                    MessageBox.Show("Image saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save image: {ex.Message}");
                }
            }
        }

    }
}
