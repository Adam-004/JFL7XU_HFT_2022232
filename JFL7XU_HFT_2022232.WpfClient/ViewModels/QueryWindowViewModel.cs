﻿using CommunityToolkit.Mvvm.ComponentModel;
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
        /*
                .Add("List all ships that are built after given year", () => Functions.ExpHandle(() => Functions.ListShips_WhichBuiltAfter()))
                .Add("List all hangars that has more than 'X' ships", () => Functions.ExpHandle(() => Functions.ListHangars_WithShipsMoreThan()))
                .Add("List all hangars that has less than 'X' ships", () => Functions.ExpHandle(() => Functions.ListHangars_WithShipsLessThan()))
                .Add("List all owners who are older than", () => Functions.ExpHandle(() => Functions.ListOwners_OlderThan()))
                .Add("List all owners who are younger than", () => Functions.ExpHandle(() => Functions.ListOwners_YoungerThan()))
                .Add("List all owners who are younger than 'X' and has more ships than", () => Functions.ExpHandle(() => Functions.ListOwners_YoungerAndHasMoreShipsThan()))
                .Add("List report", () => Functions.ListStatistics()) 
        */
        private RestService rest;
        private ObservableCollection<object> _QueryDisplay;
        public ObservableCollection<object> QueryDisplay
        {
            get { return _QueryDisplay; }
            set => SetProperty(ref _QueryDisplay, value);
        }

        private object selectedItem;
        public object SelectedItem
        {
            get { return selectedItem; }
            set => SetProperty(ref selectedItem, value);
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
        }

        [RelayCommand]
        public void Report()
        {
            QueryDisplay = new ObservableCollection<object>();
            List<OwnershipStatistics> report = rest.Get<OwnershipStatistics>("NonCrud/ListStatistics");
            ObservableCollection<object> tmp = new(); 
            foreach (var item in report)
            {
                tmp.Add(item.Owner);
            }
            QueryDisplay = tmp;
            var b =QueryDisplay;
            ;
        }
        private int GetID(object o)
        {
            if (o is Starship) { return (o as Starship).ID; }
            else if (o is Owner) { return (o as Owner).ID; }
            else if (o is Hangar) { return (o as Hangar).Id; }
            else {  return -1; }
        }
        public void SelectByID(TextBox InputID)
        {
            if (InputID.Text != "")
            {
                int id = int.Parse(InputID.Text);
                var queued = QueryDisplay.Where(t => GetID(t).Equals(id)).FirstOrDefault();
                if (queued != null) SelectedItem = queued; else MessageBox.Show("No record on this ID!");
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