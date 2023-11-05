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
using Microsoft.Toolkit.Mvvm.ComponentModel;
using JFL7XU_HFT_2022232.WpfClient.Services.StarshipServ;
using JFL7XU_HFT_2022232.WpfClient.Services.Interfaces;

namespace JFL7XU_HFT_2022232.WpfClient.ViewModels
{
    partial class StarshipEditorWindowViewModel : ObservableRecipient
    {
        private Starship selectedStarship;
        private int? selectedID;
        private string selectedName = " ";
        private int? selectedSize;
        private string selectedType;
        private StarshipServiceInterface StarshipServ;
        public Starship SelectedStarship
        {
            get => selectedStarship;
            set
            {
                var tmp = value;
                SetProperty(ref selectedStarship, value);
                if (tmp != null)
                {
                    SelectedID = tmp.ID;
                    SelectedName = tmp.Name;
                    SelectedSize = tmp.Size;
                    Enum.TryParse<ShipType>(tmp.Type.ToString(), out var typeTmp);
                    SelectedType = typeTmp.ToString();
                }
                else
                {
                    SelectedID = null;
                    SelectedName = " ";
                    SelectedSize = null;
                    SelectedType = null;
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
        public int? SelectedSize
        {
            get => selectedSize;
            set => SetProperty(ref selectedSize, value);
        }
        public string SelectedType
        {
            get => selectedType;
            set => SetProperty(ref selectedType, value);
        }
        public RestCollection<Starship> Starships { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public StarshipEditorWindowViewModel(StarshipServiceInterface service)
        {
            if (!IsInDesignMode)
            {
                Starships = new RestCollection<Starship>("http://localhost:40567/", "Starship");
            }
            StarshipServ = service;
        }
        private bool IsSelectedOwnerNotNull()
        {
            if (SelectedStarship == null) { return false; }
            return true;
        }
        [RelayCommand]
        public void Create()
        {
            StarshipServ.Create();
            var ship = StarshipServ.Starship;
            Starships.Add(ship);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedOwnerNotNull))]
        public void Edit()
        {
            StarshipServ.Update(SelectedStarship);
            var ship = StarshipServ.Starship;
            Starships.Update(ship);
        }
        [RelayCommand(CanExecute = nameof(IsSelectedOwnerNotNull))]
        public void Delete()
        {
            Starships.Delete((int)selectedID);
            SelectedStarship = null;
        }
        [RelayCommand]
        public void SelectByID(TextBox InputID)
        {
            if (InputID.Text != "")
            {
                int id = int.Parse(InputID.Text);
                var queued = Starships.Where(t => t.ID.Equals(id)).FirstOrDefault();
                if (queued != null) SelectedStarship = queued; else MessageBox.Show("No record on this ID!");
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
