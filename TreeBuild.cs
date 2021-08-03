using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMO.ADO.NET.Control.SOUT
{
    public class MyTag
    {
        public int OrgID { get; set; }
        public int? PodrID { get; set; }
        public int? WPlaceID { get; set; }

        public MyTag(int OrgID, int? PodrID)
        {
            this.OrgID = OrgID;
            this.PodrID = PodrID;
            WPlaceID = null;
        }
        public MyTag(int OrgID)
        {
            PodrID = null;
            this.OrgID = OrgID;
            WPlaceID = null;
        }
        public MyTag(int OrgID, int? PodrID, int? WPlaceID)
        {
            this.OrgID = 0;
            this.PodrID = 0;
            this.WPlaceID = WPlaceID;
        }
    }
    public static class GetFromDB
    {
        public static Organization GetOrganization(MyTag Tag)
        {
            SOUTEntities context = new SOUTEntities();
            return context.Organization.Where(x => x.OrganizationID == Tag.OrgID).FirstOrDefault();
        }
        public static Departments GetDepartment(MyTag Tag)
        {
            SOUTEntities context = new SOUTEntities();
            return context.Departments.Where(x => x.OrganizationID == Tag.OrgID
                            & x.DepartmentID == Tag.PodrID).FirstOrDefault();
        }
        public static Workplaces GetWorkplace(MyTag Tag)
        {
            SOUTEntities context = new SOUTEntities();
            return context.Workplaces.Where(x => x.WorkplaceID == Tag.WPlaceID).FirstOrDefault();
        }
        public static Employees GetEmployee(MyTag Tag)
        {
            SOUTEntities context = new SOUTEntities();
            return context.Employees.Where(x => x.WorkplaceID == Tag.WPlaceID).FirstOrDefault();
        }
    }
}
