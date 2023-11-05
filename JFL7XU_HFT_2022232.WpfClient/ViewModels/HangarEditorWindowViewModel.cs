using CommunityToolkit.Mvvm.Input;
using JFL7XU_HFT_2022232.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using JFL7XU_HFT_2022232.WpfClient.Services.HangarServ;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;

namespace JFL7XU_HFT_2022232.WpfClient.ViewModels
{
    partial class HangarEditorWindowViewModel : ObservableRecipient
    {
        private Hangar selectedHangar;
        private int? selectedID;
        private string selectedName = " ";
        private string selectedLocation;
        private int? selectedOwnerID;
        private HangarServiceInterface HangarServ;
        public Hangar SelectedHangar
        {
            get => selectedHangar;
            set
            {
                var tmp = value;
                SetProperty(ref selectedHangar, value);
                if (tmp != null)
                {
                    SelectedID = tmp.Id;
                    SelectedName = tmp.Name;
                    SelectedLocation = tmp.Location;
                }
                else
                {
                    SelectedID = null;
                    SelectedName = " ";
                    SelectedLocation = null;
                }
                EditCommand.NotifyCanExecuteChanged();
                DeleteCommand.NotifyCanExecuteChanged();
            }
        }
        public int? SelectedID
        {
            get => selectedID;
            set => SetProperty(ref selectedID, value);
        }
        public string SelectedName
        {
            get => selectedName;
            set => SetProperty(ref selectedName, value);
        }
        public string SelectedLocation
        {
            get => selectedLocation;
            set => SetProperty(ref selectedLocation, value);
        }
        public int? SelectedOwnerID
        {
            get => selectedOwnerID;
            set => SetProperty(ref selectedOwnerID, value);
        }
        public RestCollection<Hangar> Hangars { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public HangarEditorWindowViewModel(HangarServiceInterface service)
        {
            if (!IsInDesignMode)
            {
                Hangars = new RestCollection<Hangar>("http://localhost:40567/", "Hangar");
            }
            HangarServ = service;
        }
        private bool IsSelectedHangarNotNull() {if (SelectedHangar == null) { return false; }return true;}
        [RelayCommand]
        public void Create()
        {
            HangarServ.Create();
            var hangar = HangarServ.Hangar;
            Hangars.Add(hangar);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedHangarNotNull))]
        public void Edit()
        {
            HangarServ.Update(SelectedHangar);
            var hangar = HangarServ.Hangar;
            Hangars.Update(hangar);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedHangarNotNull))]
        public void Delete()
        {
            Hangars.Delete((int)selectedID);
            SelectedHangar = null;
        }
        [RelayCommand]
        public void SelectByID(TextBox InputID)
        {
            if (InputID.Text != "")
            {
                int id = int.Parse(InputID.Text);
                var queued = Hangars.Where(t => t.Id.Equals(id)).FirstOrDefault();
                if (queued != null) SelectedHangar = queued; else MessageBox.Show("No record on this ID!");
            }
            InputID.Text = null;
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
