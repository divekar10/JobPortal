using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Database
{
    public static class SQLHelper
    {
        public static DataSet CGetData<T>(string sql, List<SqlParameter> sqlParameters = null) where T : new()
        {
            var tcs = new TaskCompletionSource<List<T>>();
            DataSet ds = new DataSet();
            using (var con = new SqlConnection("Data Source=DESKTOP-M6QJC04;Initial Catalog=JobPortal;Integrated Security=True"))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    if (sqlParameters != null)
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    con.Close();

                    return ds;
                    //string JSONString = string.Empty;
                    //JSONString = JsonConvert.SerializeObject(ds.Tables[0]);
                    //IEnumerable<T> list = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(JSONString));
                    //return list;
                }
            }
        }
    }
}
