using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace JFL7XU_HFT_2022232.WpfClient.Services.HangarServ
{
    /// <summary>
    /// Interaction logic for HangarCreateWindow.xaml
    /// </summary>
    public partial class HangarCreateWindow : Window
    {
        public Hangar hangar { get; set; } = new();
        public HangarCreateWindow()
        {
            InitializeComponent();
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            bool ParseOkID = false;
            bool ParseOkOID = false;
            if (InputID.Text != "")
            {
                ParseOkID = int.TryParse(InputID.Text.ToString(), out int id);
                if (ParseOkID)
                {
                    hangar.Id = id;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputName.Text != "") hangar.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputLocation.Text != "") hangar.Location = InputLocation.Text.ToString(); else MessageBox.Show("Location was null!");
            if (InputOwnerID.Text != "")
            {
                ParseOkOID = int.TryParse(InputOwnerID.Text.ToString(), out int id);
                if (ParseOkOID)
                {
                    hangar.OwnerID = id;
                }
                else MessageBox.Show("OwnerID must be a number!");
            }
            else MessageBox.Show("OwnerID was null!");
            if (InputID.Text != "" && InputLocation.Text != "" && InputName.Text != "" && InputOwnerID.Text != "" && ParseOkID && ParseOkOID) this.DialogResult = true;
        }
    }
}
