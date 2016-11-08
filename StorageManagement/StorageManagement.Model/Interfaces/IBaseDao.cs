using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Interfaces
{
    public interface IBaseDao<T> where T : IFillModel, ITableConfig, new()
    {
        T GetByField(string fieldName, object value);
        List<T> GetAll();
    }
}
