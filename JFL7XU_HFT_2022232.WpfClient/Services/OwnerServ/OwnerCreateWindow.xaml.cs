﻿using JFL7XU_HFT_2022232.Models;
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
            if (InputID.Text != "") owner.ID = int.Parse(InputID.Text.ToString()); else MessageBox.Show("ID was null!");
            if (InputName.Text != "") owner.Name = InputName.Text.ToString(); else MessageBox.Show("Name was null!");
            if (InputAge.Text != "") owner.Age = int.Parse(InputAge.Text.ToString()); else MessageBox.Show("Age was null!");
            if (InputID.Text != "" && InputName.Text != "" && InputAge.Text != "") this.DialogResult = true;
        }
    }
}
