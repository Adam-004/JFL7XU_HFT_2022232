using CommunityToolkit.Mvvm.Input;
using JFL7XU_HFT_2022232.WpfClient.Services.HangarServ;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JFL7XU_HFT_2022232.WpfClient.ViewModels
{
    partial class MainWindowViewModel
    {
        StarshipService shipService = new();
        OwnerService ownerService = new();
        HangarService hangarService = new();
        [RelayCommand]
        public void EditOwners(Window window)
        {
            var OwnerWindow = new OwnerEditorWindow();
            OwnerWindow.DataContext = new OwnerEditorWindowViewModel(ownerService);
            OwnerWindow.Show();
            window.Close();
        }
        [RelayCommand]
        public void EditHangars(Window window)
        {
            var HangarWindow = new HangarEditorWindow();
            HangarWindow.DataContext = new HangarEditorWindowViewModel(hangarService);
            HangarWindow.Show();
            window.Close();
        }
        [RelayCommand]
        public void EditStarships(Window window)
        {
            var ShipWindow = new StarshipEditorWindow();
            ShipWindow.DataContext = new StarshipEditorWindowViewModel(shipService);
            ShipWindow.Show();
            window.Close();
        }
        [RelayCommand]
        public void Query(Window window)
        {
            //var QueryWindow = new QueryWindow();
            //QueryWindow.DataContext = new QueryWindowViewModel(shipService);
            //QueryWindow.Show();
            //window.Close();
        }
        [RelayCommand]
        public void Terminate(Window window) { window.Close(); }
        public MainWindowViewModel() { }
    }
}
