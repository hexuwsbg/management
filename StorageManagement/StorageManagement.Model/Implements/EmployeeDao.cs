using StorageManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Implements
{
    public class EmployeeDao : BaseDao<Employee>
    {
        protected override string DefaultTableName
        {
            get
            {
                return TableNameConsts.EMPLOYEE_TABLE_NAME;
            }
        }

        protected override string PKField
        {
            get
            {
                return TableNameConsts.EMPLOYEE_TABLE_PK_FIELD;
            }
        }
    }
}
