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

namespace JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ
{
    /// <summary>
    /// Interaction logic for StarshipCreateWindow.xaml
    /// </summary>
    public partial class StarshipCreateWindow : Window
    {
        public Starship starship { get; set; } = new();
        public StarshipCreateWindow()
        {
            InitializeComponent();
            InputType.Items.Add(ShipType.Fregatte);
            InputType.Items.Add(ShipType.Transport);
            InputType.Items.Add(ShipType.Cruiser);
            InputType.Items.Add(ShipType.Fighter);
            InputType.SelectedItem = ShipType.Transport;
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            bool ParseOkID = false;
            bool ParseOkSize = false;
            bool ParseOkYOM = false;
            bool ParseOkOID = false;
            if (InputID.Text != "")
            {
                ParseOkID = int.TryParse(InputID.Text.ToString(), out int id);
                if (ParseOkID)
                {
                    starship.ID = id;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputName.Text != "") starship.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputSize.Text != "")
            {
                ParseOkSize = int.TryParse(InputSize.Text.ToString(), out int size);
                if (ParseOkSize)
                {
                    starship.Size = size;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputYOM.Text != "")
            {
                ParseOkYOM = int.TryParse(InputYOM.Text.ToString(), out int yom);
                if (ParseOkYOM)
                {
                    starship.YearOfManu = yom;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            Enum.TryParse<ShipType>(InputType.Text, out var type);
            starship.Type = (int)type;
            if (InputOwnerID.Text != "")
            {
                ParseOkOID = int.TryParse(InputOwnerID.Text.ToString(), out int Oid);
                if (ParseOkOID)
                {
                    starship.OwnerID = Oid;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputID.Text != "" && InputName.Text != "" && InputSize.Text != "" && ParseOkID && ParseOkSize && ParseOkYOM && ParseOkOID) this.DialogResult = true;
        }
    }
}
