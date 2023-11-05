using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ
{
    /// <summary>
    /// Interaction logic for StarshipUpdateWindow.xaml
    /// </summary>
    public partial class StarshipUpdateWindow : Window
    {
        public Starship Updated { get; set; } = new();
        public StarshipUpdateWindow(Starship ship)
        {
            InitializeComponent();
            Updated = ship;
            ShowID.Content = Updated.ID.ToString();
            InputName.Text = Updated.Name;
            InputSize.Text = Updated.Size.ToString();
            InputYOM.Text = Updated.YearOfManu.ToString();
            InputOwnerID.Text = Updated.OwnerID.ToString();
            InputType.Items.Add(ShipType.Fregatte.ToString());
            InputType.Items.Add(ShipType.Transport.ToString());
            InputType.Items.Add(ShipType.Cruiser.ToString());
            InputType.Items.Add(ShipType.Fighter.ToString());
            Enum.TryParse<ShipType>(Updated.Type.ToString(), out var typeTmp);
            InputType.SelectedItem = typeTmp.ToString();

        }
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            bool ParseOkSize = false;
            bool ParseOkYOM = false;
            bool ParseOkOID = false;
            if (InputName.Text != "") Updated.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputSize.Text != "")
            {
                ParseOkSize = int.TryParse(InputSize.Text.ToString(), out int size);
                if (ParseOkSize)
                {
                    Updated.Size = size;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputYOM.Text != "")
            {
                ParseOkYOM = int.TryParse(InputYOM.Text.ToString(), out int yom);
                if (ParseOkYOM)
                {
                    Updated.YearOfManu = yom;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            Enum.TryParse<ShipType>(InputType.Text, out var type);
            Updated.Type = (int)type;
            if (InputOwnerID.Text != "")
            {
                ParseOkOID = int.TryParse(InputOwnerID.Text.ToString(), out int Oid);
                if (ParseOkOID)
                {
                    Updated.OwnerID = Oid;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputName.Text != "" && InputSize.Text != "" && ParseOkSize && ParseOkYOM && ParseOkOID) this.DialogResult = true;
        }
    }
}
