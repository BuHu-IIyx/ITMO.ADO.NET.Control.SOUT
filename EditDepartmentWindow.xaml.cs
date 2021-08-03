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
    /// Interaction logic for EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(MyTag tag)
        {
            InitializeComponent();
            EditDepartmentGrid.DataContext = GetFromDB.GetDepartment(tag);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, (Button)sender);
            Departments Edit1 = (Departments)EditDepartmentGrid.DataContext;
            using (SOUTEntities context = new SOUTEntities())
            {
                Departments Edit2 = context.Departments
                    .Where(x => x.DepartmentID == Edit1.DepartmentID)
                    .Single<Departments>();
                Edit2.Name = Edit1.Name;               
                context.SaveChanges();
            }

            this.DialogResult = true;
        }
    }
}
