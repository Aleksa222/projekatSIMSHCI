using projekatSIMS.CompositeComon;
using projekatSIMS.Model;
using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.UI.Dialogs.ViewModel.TouristViewModel
{
    internal class TouristPDFModel : ViewModelBase
    {
        private string date;
        private ObservableCollection<Voucher> items = new ObservableCollection<Voucher>();
        private VoucherService voucherService = new VoucherService();

        public TouristPDFModel()
        {
            DateTime currentDate = DateTime.Now;
            date = currentDate.ToString("d/M/yyyy");
            foreach(Voucher voucher in voucherService.GetAll())
            {
                items.Add(voucher);
            }
        }

        public string Date
        {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged(nameof(date));
            }
        }
        public ObservableCollection<Voucher> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
    }
}
