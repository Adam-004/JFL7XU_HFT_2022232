using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JFL7XU_HFT_2022232.WpfClient.ViewModels
{
    partial class QueryWindowViewModel : ObservableRecipient
    {
        public class QueryDisplayItem
        {
            public QueryDisplayItem(string display)
            {
                Display = display;
            }
            public string Display { get; set; }
        }
        private RestService rest;
        public string InputA { get; set; }
        public string InputB { get; set; }
        public string InputC { get; set; }
        public string InputD { get; set; }
        public string InputE { get; set; }
        public string InputF { get; set; }
        public string InputG { get; set; }
        private string _QueryStatus;
        public string QueryStatus
        {
            get { return _QueryStatus; }
            set => SetProperty(ref _QueryStatus, value);
        }
        private ObservableCollection<QueryDisplayItem> _QueryDisplay;
        public ObservableCollection<QueryDisplayItem> QueryDisplay
        {
            get { return _QueryDisplay; }
            set => SetProperty(ref _QueryDisplay, value);
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public QueryWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                rest = new("http://localhost:40567/");
            }
            QueryStatus = "No Query running";
        }
        [RelayCommand]
        public void ListShips()//|A|
        {
            if (InputA != null && InputA != "")
            {
                QueryStatus = "Query in progress...";
                QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                bool isOk = int.TryParse(InputA, out var year);
                if (isOk) 
                {
                    List<Starship> ships = rest.Get<Starship>("NonCrud/ListShips_WhichBuiltAfter/" + year);
                    foreach (var ship in ships)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(ship.ToString()));
                    }
                    QueryStatus = "Query finished.";
                }
                else { MessageBox.Show("|A| parameter must be a number!"); QueryStatus = "Query failed!"; }
            }
            else { MessageBox.Show("|A| parameter must NOT be a empty!"); }
            
        }
        [RelayCommand]
        public void ListHangarsMoreThan()//|B|
        {
            if (InputB != null && InputB != "")
            {
                bool isOk = int.TryParse(InputB, out var quantity);
                if (isOk)
                {
                    QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                    List<Hangar> ships = rest.Get<Hangar>("NonCrud/ListHangars_WithShipsMoreThan/" + quantity);
                    foreach (var ship in ships)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(ship.ToString()));
                    }
                }
                else { MessageBox.Show("|B| parameter must be a number!"); }
            }
            else { MessageBox.Show("|B| parameter must NOT be a empty!"); }
        }
        [RelayCommand]
        public void ListHangarsLessThan()//|C|
        {
            if (InputC != null && InputC != "")
            {
                QueryStatus = "Query in progress...";
                QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                bool isOk = int.TryParse(InputC, out var quantity);
                if (isOk)
                {
                    List<Hangar> ships = rest.Get<Hangar>("NonCrud/ListHangars_WithShipsLessThan/" + quantity);
                    foreach (var ship in ships)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(ship.ToString()));
                    }
                    QueryStatus = "Query finished.";
                }
                else { MessageBox.Show("|C| parameter must be a number!"); QueryStatus = "Query failed!"; }
            }
            else { MessageBox.Show("|C| parameter must NOT be a empty!"); }
        }
        [RelayCommand]
        public void ListOlderOwners()//|D|
        {
            if (InputD != null && InputD != "")
            {
                QueryStatus = "Query in progress...";
                QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                bool isOk = int.TryParse(InputD, out var age);
                if (isOk)
                {
                    List<Owner> owners = rest.Get<Owner>("NonCrud/ListOwners_OlderThan/" + age);
                    foreach (var owner in owners)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(owner.ToString()));
                    }
                    QueryStatus = "Query finished.";
                }
                else { MessageBox.Show("|D| parameter must be a number!"); QueryStatus = "Query failed!"; }
            }
            else { MessageBox.Show("|D| parameter must NOT be a empty!"); }
        }
        [RelayCommand]
        public void ListYoungerOwners()//|E|
        {
            if (InputE != null && InputE != "")
            {
                QueryStatus = "Query in progress...";
                QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                bool isOk = int.TryParse(InputE, out var age);
                if (isOk)
                {
                    List<Owner> owners = rest.Get<Owner>("NonCrud/ListOwners_YoungerThan/" + age);
                    foreach (var owner in owners)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(owner.ToString()));
                    }
                    QueryStatus = "Query finished.";
                }
                else { MessageBox.Show("|E| parameter must be a number!"); QueryStatus = "Query failed!"; }
            }
            else { MessageBox.Show("|E| parameter must NOT be a empty!"); }
        }
        [RelayCommand]
        public void ListComplexOwners() //|F|G|
        {
            if (InputF != null && InputG != null && InputF != "" && InputG != "")
            {
                QueryStatus = "Query in progress...";
                QueryDisplay = new ObservableCollection<QueryDisplayItem>();
                bool isFOk = int.TryParse(InputF, out var age);
                bool isGOk = int.TryParse(InputG, out var quantity);
                if (isFOk && isGOk)
                {
                    List<Owner> owners = rest.Get<Owner>($"NonCrud/ListOwners_YoungerAndHasMoreShipsThan/{age}/{quantity}");
                    foreach (var owner in owners)
                    {
                        QueryDisplay.Add(new QueryDisplayItem(owner.ToString()));
                    }
                    QueryStatus = "Query finished.";
                }
                else { MessageBox.Show("Both |F| and |G| parameter must be a number!"); QueryStatus = "Query failed!"; }
            }
            else
            {
                MessageBox.Show("|F| and |G| parameters must NOT be empty!");
            }
        }
        [RelayCommand]
        public void Report()
        {
            QueryDisplay = new ObservableCollection<QueryDisplayItem>();
            List<OwnershipStatistics> report = rest.Get<OwnershipStatistics>("NonCrud/ListStatistics");

            foreach (var item in report)
            {
                QueryDisplay.Add(new QueryDisplayItem(item.ToString()));
            }
            var tmp = QueryDisplay;
        }
        [RelayCommand]
        public void Return(Window window)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            window.Close();
        }
        [RelayCommand]
        public void Terminate(Window window) { window.Close(); }
    }
}
