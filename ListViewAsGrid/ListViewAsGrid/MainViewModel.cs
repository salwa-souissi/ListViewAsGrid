using ListViewAsGrid.Data.ListViewAsGrid;
using ListViewAsGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ListViewAsGrid
{
    class MainViewModel
    {
        public ObservableCollection<Employee> EmployeesList { get; set; }
        public Page CurrentPage { get; set; }
        public INavigation _nav { get; set; }
        private Employee _selectedCategorie;

        public Employee SelectedEmployee
        {
            get { return _selectedCategorie; }
            set {
                _selectedCategorie = value; }
        }

        public MainViewModel()
        {
            EmployeesList = MockData.Employees;
        }
    }
}
