using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace zyberfox.challenge.api.Model
{
    public class SqlCon
    {
     
        public static string SqlConnect => "Server=HARPS\\SQLEXPRESS;Database=Zyberfox;Integrated Security=SSPI;";

        public static SqlConnection SqlConnection() => new SqlConnection(SqlConnect);

        public static int SqlExecute(SqlCommand cmd)
        {
            using (var connect = SqlConnection())
            {
                cmd.Connection = connect;
                connect.Open();
                var results = cmd.ExecuteNonQuery();
                return results;
            }
        }

        public static int SqlOrmexecute(string sql, object parameters = null)
        {
            using (var Connect = SqlConnection())
            {
                Connect.Open();
                var data = Connect.Execute(sql, parameters);
                return data;
            }
        }

        public static T SqlGetData<T>(string sql, object parameters = null)
        {
            using (var Connect = SqlConnection())
            {
                Connect.Open();
                var data = Connect.Query<T>(sql, parameters).FirstOrDefault();
                return data;
            }
        }

        public static List<T> SqlGetDataList<T>(string sql, object parameters = null)
        {
            using (var Connect = SqlConnection())
            {
                Connect.Open();
                var data = Connect.Query<T>(sql, parameters).ToList();
                return data;

            }
        }

        public static string SqlScalar(SqlCommand cmd) => SqlScalar<string>(cmd);

        public static T SqlScalar<T>(SqlCommand cmd)
        {
            using (var Connect = SqlConnection())
            {
                cmd.Connection = Connect;
                Connect.Open();
                var data = cmd.ExecuteScalar();

                if (data == null)
                {
                    return default(T);
                }
                return (T)data;

            }
        }
    }
}

