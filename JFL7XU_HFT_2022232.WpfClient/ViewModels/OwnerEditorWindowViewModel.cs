using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JFL7XU_HFT_2022232.Models;
using JFL7XU_HFT_2022232.WpfClient.Services.HangarServ;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;
using JFL7XU_HFT_2022232.WpfClient.Services.OwnerServ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JFL7XU_HFT_2022232.WpfClient.ViewModels
{
    partial class OwnerEditorWindowViewModel : ObservableRecipient
    {
        private Owner selectedOwner;
        private int? selectedID;
        private string selectedName = " ";
        private int? selectedAge;
        private OwnerServiceInterface OwnerServ;
        public Owner SelectedOwner
        {
            get => selectedOwner;
            set
            {
                var tmp = value;
                SetProperty(ref selectedOwner, value);
                if (tmp != null)
                {
                    SelectedID = tmp.ID;
                    SelectedName = tmp.Name;
                    SelectedAge = tmp.Age;
                }
                else
                {
                    SelectedID = null;
                    SelectedName = " ";
                    SelectedAge = null;
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
        public int? SelectedAge
        {
            get => selectedAge;
            set => SetProperty(ref selectedAge, value);
        }
        public RestCollection<Owner> Owners { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public OwnerEditorWindowViewModel(OwnerServiceInterface service)
        {
            if (!IsInDesignMode)
            {
                Owners = new RestCollection<Owner>("http://localhost:40567/", "Owner", "hub");
            }
            OwnerServ = service;
        }
        private bool IsSelectedOwnerNotNull()
        {
            if (SelectedOwner == null) { return false; }
            return true;
        }
        [RelayCommand]
        public void Create()
        {
            OwnerServ.Create();
            var owner = OwnerServ.Owner;
            Owners.Add(owner);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedOwnerNotNull))]
        public void Edit()
        {
            OwnerServ.Update(SelectedOwner);
            var owner = OwnerServ.Owner;
            Owners.Update(owner);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedOwnerNotNull))]
        public void Delete()
        {
            Owners.Delete((int)selectedID);
            SelectedOwner = null;
        }
        [RelayCommand]
        public void SelectByID(TextBox InputID)
        {
            if (InputID.Text != "")
            {
                int id = int.Parse(InputID.Text);
                var queued = Owners.Where(t => t.ID.Equals(id)).FirstOrDefault();
                if (queued != null) SelectedOwner = queued; else MessageBox.Show("No record on this ID!");
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
