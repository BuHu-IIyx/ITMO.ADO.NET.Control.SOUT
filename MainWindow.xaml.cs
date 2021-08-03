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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITMO.ADO.NET.Control.SOUT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OrganizationTreeView_Expanded(object sender, RoutedEventArgs e)
        {
            using(SOUTEntities context = new SOUTEntities())
            {
                TreeViewItem item = (TreeViewItem)e.OriginalSource;
                MyTag Tag = (MyTag)item.Tag;
                item.Items.Clear();
                var DepartmentList =
                    from x in context.Departments
                    where Tag.OrgID == x.OrganizationID & Tag.PodrID == x.ParrentDepartmentID
                    select new { Name = x.Name, PodrID = x.DepartmentID };
                if (DepartmentList.Count() != 0)
                {
                    foreach (var Dept in DepartmentList)
                    {
                        TreeViewItem newItem = new TreeViewItem();
                        newItem.Tag = new MyTag(Tag.OrgID, Dept.PodrID);
                        newItem.Header = Dept.Name.ToString();
                        newItem.Items.Add("*");
                        item.Items.Add(newItem);
                    }
                }
                else
                {
                    var WorkPlacesList =
                        from x in context.Workplaces
                        where Tag.PodrID == x.DepartmentID
                        select new { Name = x.Name, WorkplaceID = x.WorkplaceID };
                    foreach (var WorkPlace in WorkPlacesList)
                    {
                        TreeViewItem newItem = new TreeViewItem();
                        newItem.Tag = new MyTag(0, 0, WorkPlace.WorkplaceID);
                        newItem.Header = WorkPlace.Name;
                        item.Items.Add(newItem);
                    }
                }
            }            
        }

        private void OrganizationTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            using (SOUTEntities context = new SOUTEntities())
            {
                var OrganizationList =
                from x in context.Organization
                select new { Name = x.Name, OrganizationID = x.OrganizationID };
                if (OrganizationList.Count() != 0)
                {
                    foreach (var Org in OrganizationList)
                    {
                        TreeViewItem item = new TreeViewItem();
                        item.Tag = new MyTag(Org.OrganizationID);
                        item.Header = Org.Name.ToString();
                        item.Items.Add("*");
                        OrganizationTreeView.Items.Add(item);
                    }
                }
            }                
        }

        private void OrganizationTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem selectedItem = (TreeViewItem)OrganizationTreeView.SelectedItem;
            if (selectedItem != null)
            {
                MyTag Tag = (MyTag)selectedItem.Tag;
                switch (Tag.PodrID)
                {                    
                    case 0:
                        EditWorkPlaceWindow editWorkPlaceWindow = new EditWorkPlaceWindow(Tag);
                        if (editWorkPlaceWindow.ShowDialog() == true)
                        {
                            MessageBox.Show("Данные сохранены");
                        }
                        break;
                    case null:
                        EditOrganization editOrganization = new EditOrganization(Tag);
                        if (editOrganization.ShowDialog() == true)
                        {                            
                            MessageBox.Show("Данные сохранены");
                        }                        
                        break;
                    default:
                        EditDepartmentWindow departmentWindow = new EditDepartmentWindow(Tag);
                        if (departmentWindow.ShowDialog() == true)
                        {
                            MessageBox.Show("Данные сохранены");
                        }
                        break;
                }

            }
        }
    }
}