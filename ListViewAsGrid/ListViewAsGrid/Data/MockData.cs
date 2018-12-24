using ListViewAsGrid.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ListViewAsGrid.Data.ListViewAsGrid
{
    public static class MockData
    {
         public static ObservableCollection<Categories> Categories = new ObservableCollection<Categories>()
        {
            new Models.Categories{
                Ar = "في كل الاوقات",ID = 1, Dt = "", En = "All Times",  Fr="Tous le Temps",    Tr="" 
            },
            new Models.Categories{
                Ar = "دعاء من القرأن",ID = 2, Dt = "", En = "Quraan ListViewAsGrid",  Fr="Quraan ListViewAsGrid",    Tr="" 
            },
            new Models.Categories{
                Ar = "دعاء المسجد",ID = 3, Dt = "", En = "Mosque",  Fr="Mosque",    Tr="" 
            },
             new Models.Categories{
                Ar = "دعاء الاستسقاء",ID = 4, Dt = "", En = "Ablution",  Fr="Ablution",    Tr="" 
            },
             new Models.Categories{
                Ar = "دعاء الصلاة",ID = 5, Dt = "", En = "Prayer",  Fr="Prière",    Tr="" 
            },

            new Models.Categories{
                Ar = "توجيه",ID = 6, Dt = "", En = "Guidance",  Fr="Orientation",    Tr="" 
            },
            new Models.Categories{
                Ar = "الأطفال",ID = 7, Dt = "", En = "Children",  Fr="Enfants",    Tr="" 
            },
            new Models.Categories{
                Ar = "الأكل",ID = 8, Dt = "", En = "Eating",  Fr="Manger",    Tr="" 
            },
             new Models.Categories{
                Ar = "الصوم",ID = 9, Dt = "", En = "Fasting",  Fr="Le jeûne",    Tr="" 
            },
             new Models.Categories{
                Ar = "يوم الجمعة",ID = 10, Dt = "", En = "friday",  Fr="Vendredi",    Tr="" 
            }
        };
    }
}
