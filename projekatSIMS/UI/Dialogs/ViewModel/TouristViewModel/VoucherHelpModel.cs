using projekatSIMS.CompositeComon;
using projekatSIMS.UI.Dialogs.View.TouristView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class VoucherHelpModel : ViewModelBase
    {
        private RelayCommand gotItCommand;

        public VoucherHelpModel()
        {

        }
        private void GotItCommandExecute()
        {
            TouristMainWindow.navigationService.Navigate(
                new Uri("UI/Dialogs/View/TouristView/TouristVouchersView.xaml", UriKind.Relative));
        }
        private bool CanThisCommandExecute()
        {
            string currentUri = TouristMainWindow.navigationService?.CurrentSource?.ToString();
            return currentUri?.EndsWith("VoucherHelp.xaml", StringComparison.OrdinalIgnoreCase) == true;
        }
        public RelayCommand GotItCommand
        {
            get
            {
                if (gotItCommand == null)
                {
                    gotItCommand = new RelayCommand(param => GotItCommandExecute(), param => CanThisCommandExecute());
                }

                return gotItCommand;
            }
        }
    }
}
