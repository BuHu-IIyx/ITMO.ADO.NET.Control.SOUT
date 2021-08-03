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
    /// Interaction logic for EditOrganization.xaml
    /// </summary>
    public partial class EditOrganization : Window
    {
        public EditOrganization(MyTag tag)
        {           
            InitializeComponent();

            EditOrganizationGrid.DataContext = GetFromDB.GetOrganization(tag);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FocusManager.SetFocusedElement(this, (Button)sender);
            Organization EditOrg1 = (Organization)EditOrganizationGrid.DataContext;
            using (SOUTEntities context = new SOUTEntities())
            {
                Organization EditOrg2 = context.Organization
                    .Where(x => x.OrganizationID == EditOrg1.OrganizationID)
                    .Single<Organization>();
                EditOrg2.Name = EditOrg1.Name;
                EditOrg2.Adress = EditOrg1.Adress;
                EditOrg2.INN = EditOrg1.INN;
                EditOrg2.OGRN = EditOrg1.OGRN;
                context.SaveChanges();
            }                

            this.DialogResult = true;

        }

    }
}
