using projekatSIMS.Model;
using projekatSIMS.Repository;
using projekatSIMS.Service;
using projekatSIMS.UI.Dialogs.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

namespace projekatSIMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*private AccommodationService accommodationService;*/
        public MainWindow()
        {
            InitializeComponent();
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                comboBox.Items.Add(entity.Type);
            }

            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                for (int y = 0; y < comboBox.Items.Count; y++)
                {
                    if (y != i && comboBox.Items[i].ToString() == comboBox.Items[y].ToString())
                    {
                        comboBox.Items.RemoveAt(i);
                        break;
                    }
                }
            }

        }
<<<<<<< Updated upstream
=======

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MyListBox.Items.Clear();

            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                Debug.WriteLine(entity.Id + " " + entity.Name);
            }
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.GuestLimit + " " + entity.MinimalStay + " " + entity.CancelationLimit);
            }
        }

        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MyListBox.Items.Clear();
            string selectedType = comboBox.SelectedItem.ToString();
            AccommodationService accommodationService = new AccommodationService();
            foreach (Accommodation entity in accommodationService.GetAll())
            {
                if(entity.Type.ToString() == selectedType)
                {
                    MyListBox.Items.Add(entity.Id + " " + entity.Name + " " + entity.Location.City + " " + entity.Location.Country + " " + entity.GuestLimit + " " + entity.MinimalStay + " " + entity.CancelationLimit);
                }

            }

        }
>>>>>>> Stashed changes
    }
}



