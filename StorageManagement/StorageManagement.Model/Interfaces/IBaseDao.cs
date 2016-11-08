using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Interfaces
{
    public interface IBaseDao<T> where T : IFillModel, new()
    {
        List<T> GetByField(string fieldName, object value);
        List<T> GetAll();
        List<T> GetByPage(int page, int pageSize);
        void Insert(T model);
        void Update(T model);
        void Update(IDictionary<string, object> valueDict, IDictionary<string, object> whereDict);
    }
}
