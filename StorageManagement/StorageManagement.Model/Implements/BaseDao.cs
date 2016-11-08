using MySql.Data.MySqlClient;
using StorageManagement.DataAccess.Interfaces;
using StorageManagement.DataAccess.Models;
using StorageManagement.DB.Implements;
using StorageManagement.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagement.DataAccess.Implements
{
    public class BaseDao<T> : IBaseDao<T> where T : IFillModel, ITableConfig, new()
    {
        private IConnectionHelper connectionHelper = new MySqlConnectionHelper();

        public T GetByField(string fieldName, object value)
        {
            var model = new T();
            var cmdText = string.Format("SELECT * FROM {0} WHERE {1}=@{1}", model.TableName, fieldName);
            var parameter = new MySqlParameter(fieldName, value);
            var dr = connectionHelper.ExecuteScalar("", System.Data.CommandType.Text, cmdText, parameter) as DataRow;
            model.FillModel(dr);
            return model;
        }

        public List<T> GetAll()
        {
            var list = new List<T>();
            var instance = new T();
            var cmdText = string.Format("SELECT * FROM {0}", instance.TableName);
            var ds = connectionHelper.GetDataSet("Database='test1';Data Source='localhost';User Id='root';Password='!QAZ2wsx';Charset=utf8", CommandType.Text, cmdText);
            if(ds.Tables != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                foreach(DataRow row in table.Rows)
                {
                    var model = new T();
                    model.FillModel(row);
                    list.Add(model);
                }
            }
            return list;
        }
    }
}
