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

namespace JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ
{
    /// <summary>
    /// Interaction logic for OwnerUpdateWindow.xaml
    /// </summary>
    public partial class OwnerUpdateWindow : Window
    {
        public Owner Updated { get; set; }
        public OwnerUpdateWindow(Owner owner)
        {
            InitializeComponent();
            Updated = owner;
            this.Title = $"Updating '{owner.Name}'";
            var id = Updated.ID.ToString();
            InputID.Text = id;
            InputName.Text = Updated.Name.ToString();
            InputAge.Text = Updated.Age.ToString();
        }
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            if (InputID.Text != "") Updated.ID = int.Parse(InputID.Text.ToString()); else MessageBox.Show("ID was null!");
            if (InputName.Text != "") Updated.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputAge.Text != "") Updated.Age = int.Parse(InputAge.Text.ToString()); else MessageBox.Show("Age was null!");
            if (InputID.Text != "" && InputName.Text != "" && InputAge.Text != "") this.DialogResult = true;
        }
    }
}
