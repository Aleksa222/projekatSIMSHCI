using LiveCharts.Wpf;
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
using projekatSIMS.UI.Dialogs.ViewModel.OwnerViewModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekatSIMS.UI.Dialogs.View.OwnerView
{
    /// <summary>
    /// Interaction logic for GuestReviewChart.xaml
    /// </summary>
    public partial class GuestReviewChart : UserControl
    {
        public GuestReviewChart()
        {
            InitializeComponent();
            DataContext = new GuestReviewChartViewModel();
        }
    }
}
