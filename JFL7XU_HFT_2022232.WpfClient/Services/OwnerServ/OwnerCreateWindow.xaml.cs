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

namespace JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ
{
    /// <summary>
    /// Interaction logic for OwnerCreateWindow.xaml
    /// </summary>
    public partial class OwnerCreateWindow : Window
    {
        public Owner owner { get; set; } = new();
        public OwnerCreateWindow()
        {
            InitializeComponent();
        }
        private void CreateClick(object sender, RoutedEventArgs e)
        {
            bool ParseOkID = false;
            bool ParseOkAge = false;
            if (InputID.Text != "")
            {
                ParseOkID = int.TryParse(InputID.Text.ToString(), out int id);
                if (ParseOkID)
                {
                    owner.ID = id;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputName.Text != "") owner.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputAge.Text != "")
            {
                ParseOkAge = int.TryParse(InputAge.Text.ToString(), out int age);
                if (ParseOkAge)
                {
                    owner.Age = age;
                }
                else MessageBox.Show("ID must be a number!");
            }
            else MessageBox.Show("ID was null!");
            if (InputID.Text != "" && InputName.Text != "" && InputAge.Text != "" &&ParseOkID && ParseOkAge) this.DialogResult = true;
        }
    }
}
