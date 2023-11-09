using JFL7XU_HFT_2022232.Models;
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

namespace JFL7XU_HFT_2022232.WpfClient.Services.HangarServ
{
    /// <summary>
    /// Interaction logic for HangarUpdateWindow.xaml
    /// </summary>
    public partial class HangarUpdateWindow : Window
    {
        public Hangar Updated { get; set; }
        public HangarUpdateWindow(Hangar hangar)
        {
            InitializeComponent();
            Updated = hangar;
            this.Title = $"Updating '{hangar.Name}'";
            ShowID.Content = Updated.ID.ToString();
            InputName.Text = Updated.Name.ToString();
            InputLocation.Text = Updated.Location.ToString();
            InputOwnerID.Text = Updated.OwnerID.ToString();
        }
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            bool ParseOkOID = false;
            if (InputName.Text != "") Updated.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputLocation.Text != "") Updated.Location = InputLocation.Text.ToString(); else MessageBox.Show("Location was null!");
            if (InputOwnerID.Text != "")
            {
                ParseOkOID = int.TryParse(InputOwnerID.Text.ToString(), out int id);
                if (ParseOkOID)
                {
                    Updated.OwnerID = id;
                }
                else MessageBox.Show("OwnerID must be a number!");
            }
            else MessageBox.Show("OwnerID was null!");
            if (InputLocation.Text != "" && InputName.Text != "" && InputOwnerID.Text != "" && ParseOkOID) this.DialogResult = true;
        }
    }
}
