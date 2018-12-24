using ListViewAsGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ListViewAsGrid.Data.ListViewAsGrid
{
    public static class MockData
    {
         public static ObservableCollection<Employee> Employees = new ObservableCollection<Employee>()
        {
            new Models.Employee{
                Name = "Ali",ID = 1 
            },
            new Models.Employee{
                Name = "Salah",ID = 2 
            },
            new Models.Employee{
                Name = "Hassan",ID = 3 
            },
             new Models.Employee{
                Name = "Youssef",ID = 4 
            },
             new Models.Employee{
                Name = "Mostafa",ID = 5
            },

            new Models.Employee{
                Name = "Mohamed",ID = 6
            },
            new Models.Employee{
                Name = "Haroun",ID = 7
            },
            new Models.Employee{
                Name = "Houssem",ID = 8
            },
             new Models.Employee{
                Name = "Moez",ID = 9
            },
             new Models.Employee{
                Name = "Amir",ID = 10
            }
        };
    }
}
