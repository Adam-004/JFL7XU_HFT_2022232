using CommunityToolkit.Mvvm.Input;
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
        [RelayCommand]
        public void EditOwners(Window window)
        {
            var OwnerWindow = new OwnerEditorWindow();
            OwnerWindow.Show();
            window.Close();
        }
        [RelayCommand]
        public void Terminate(Window window) { window.Close(); }
        public MainWindowViewModel() { }
    }
}
