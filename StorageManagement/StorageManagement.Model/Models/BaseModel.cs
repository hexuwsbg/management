using StorageManagement.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Models
{
    public class BaseModel : IFillModel, ITableConfig
    {
        public BaseModel()
        {

        }

        public string TableName {
            get
            {
                return this.GetType().Name;
            }
        }

        public void FillModel(DataRow dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    PropertyInfo propertyInfo = this.GetType().GetProperty(dr.Table.Columns[i].ColumnName);

                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        propertyInfo.SetValue(this, dr[i], null);
                }
            }
        }
    }
}
