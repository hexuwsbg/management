using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Interfaces
{
    public interface IFillModel
    {
        void FillModel(DataRow row);
    }
}
