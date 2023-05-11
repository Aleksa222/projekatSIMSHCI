using System;
using System.Collections.Generic;
using projekatSIMS.UI.Dialogs.ViewModel.OwnerHomeModel;
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
using projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel;

namespace projekatSIMS.UI.Dialogs.View.OwnerView
{
    /// <summary>
    /// Interaction logic for OwnerAccommodationPage.xaml
    /// </summary>
    public partial class OwnerAccommodationPage : Page
    {
        public OwnerAccommodationPage()
        {
            InitializeComponent();
            DataContext = new OwnerAccommodationsViewModel();
        }

      
    }
}
