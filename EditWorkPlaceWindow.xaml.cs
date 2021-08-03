using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ITMO.ADO.NET.Control.SOUT
{
    /// <summary>
    /// Interaction logic for EditWorkPlaceWindow.xaml
    /// </summary>
    public partial class EditWorkPlaceWindow : Window
    {
        public EditWorkPlaceWindow(MyTag tag)
        {
            InitializeComponent();
            EditName.DataContext = GetFromDB.GetWorkplace(tag);
            EditWorkPlaceGrid.DataContext = GetFromDB.GetEmployee(tag);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, (Button)sender);
            Workplaces Edit1 = (Workplaces)EditName.DataContext;
            Employees EmpEdit1 = (Employees)EditWorkPlaceGrid.DataContext;
            using (SOUTEntities context = new SOUTEntities())
            {
                Workplaces Edit2 = context.Workplaces
                    .Where(x => x.WorkplaceID == Edit1.WorkplaceID)
                    .Single();
                Edit2.Name = Edit1.Name;

                Employees EmpEdit2 = context.Employees
                    .Where(x => x.WorkplaceID == Edit1.WorkplaceID)
                    .Single();
                EmpEdit2.FIO = EmpEdit1.FIO;
                EmpEdit2.SNILS = EmpEdit1.SNILS;

                context.SaveChanges();
            }

            this.DialogResult = true;

        }
    }
}
