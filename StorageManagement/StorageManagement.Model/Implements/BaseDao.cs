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
using System.Reflection;

namespace StorageManagement.DataAccess.Implements
{
    public class BaseDao<T> : IBaseDao<T> where T : IFillModel, new()
    {
        private IConnectionHelper connectionHelper = new MySqlConnectionHelper();

        protected virtual string DefaultTableName { get; }
        protected virtual string PKField { get; }

        protected string DefaultConnectionString
        {
            get
            {
                return "Database='test1';Data Source='localhost';User Id='root';Password='!QAZ2wsx';Charset=utf8";
            }
        }

        public List<T> GetByField(string fieldName, object value)
        {
            var list = new List<T>();
            var cmdText = string.Format("SELECT * FROM {0} WHERE {1}=@{1}", DefaultTableName, fieldName);
            var parameter = new MySqlParameter(fieldName, value);
            var ds = connectionHelper.GetDataSet(DefaultConnectionString, System.Data.CommandType.Text, cmdText, parameter);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    var model = new T();
                    model.FillModel(row);
                    list.Add(model);
                }
            }
            return list;
        }

        public List<T> GetAll()
        {
            var list = new List<T>();
            var cmdText = string.Format("SELECT * FROM {0}", DefaultTableName);
            var ds = connectionHelper.GetDataSet(DefaultConnectionString, CommandType.Text, cmdText);
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

        public List<T> GetByPage(int page, int pageSize)
        {
            var list = new List<T>();
            if (page < 1)
                page = 1;
            var cmdText = string.Format("SELECT * FROM {0} LIMIT {1},{2}", DefaultTableName, (page - 1) * pageSize, pageSize);
            var ds = connectionHelper.GetDataSet(DefaultConnectionString, CommandType.Text, cmdText);
            if (ds.Tables != null && ds.Tables.Count > 0)
            {
                var table = ds.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    var model = new T();
                    model.FillModel(row);
                    list.Add(model);
                }
            }
            return list;
        }

        public void Insert(T model)
        {
            if (model == null)
                return;
            var sbValues = new StringBuilder();
            var sbFields = new StringBuilder();
            var paramters = new List<MySqlParameter>();
            foreach(var propertyInfo in model.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public))
            {
                var value = propertyInfo.GetValue(model);
                paramters.Add(new MySqlParameter(propertyInfo.Name, value));
                sbValues.AppendFormat("@{0},", propertyInfo.Name);
                sbFields.AppendFormat("{0},", propertyInfo.Name);
            }

            sbValues.Remove(sbValues.Length - 1, 1);
            sbFields.Remove(sbFields.Length - 1, 1);
            var cmdText = string.Format("INSERT INTO {0}({1}) VALUES({2})",
                DefaultTableName, sbFields.ToString(), sbValues.ToString());
            connectionHelper.ExecuteNonQuery(DefaultConnectionString, CommandType.Text, cmdText, paramters.ToArray());
        }

        public void Update(T model)
        {
            if (model == null)
                return;
            var valueDict = new Dictionary<string, object>();
            var whereDict = new Dictionary<string, object>();
            foreach (var propertyInfo in model.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public))
            {
                if (string.Compare(propertyInfo.Name, PKField, true) == 0)
                {
                    var value = propertyInfo.GetValue(model);
                    if (value == null)
                        return;
                    whereDict.Add(PKField, value);
                }
                else
                {
                    valueDict.Add(propertyInfo.Name, propertyInfo.GetValue(model));
                }
            }
            Update(valueDict, whereDict);
        }

        public void Update(IDictionary<string, object> valueDict, IDictionary<string, object> whereDict)
        {
            if (valueDict == null)
                return;
            var sbValues = new StringBuilder();
            var sbWhere = new StringBuilder();

            var paramters = new List<MySqlParameter>();
            foreach (var kv in valueDict)
            {
                paramters.Add(new MySqlParameter(kv.Key, kv.Value));
                sbValues.AppendFormat("{0}=@{0},", kv.Key);
            }
            if(whereDict != null)
            {
                foreach(var kv in whereDict)
                {
                    var paramName = string.Format("@where{0}", kv.Key);
                    paramters.Add(new MySqlParameter(paramName, kv.Value));
                    sbWhere.AppendFormat("AND {0} = {1}", kv.Key, paramName);
                }
            }

            sbValues.Remove(sbValues.Length - 1, 1);
            var cmdText = string.Format("UPDATE {0} SET {1} WHERE 1 = 1 {2}",
                DefaultTableName, sbValues.ToString(), sbWhere.ToString());
            connectionHelper.ExecuteNonQuery(DefaultConnectionString, CommandType.Text, cmdText, paramters.ToArray());
        }
    }
}
