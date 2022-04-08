using  System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace zyberfox.challenge.api.Model
{
    public class UserUpdate
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateofBirth { get; set; }
        public int Age { get; set; }


        public static bool SqlUpdateUser(UserUpdate users)
        {
            try
            {
                string sql = $" Update Users Set Firstname=@Firstname,Lastname=@Lastname,Email=@Email,dateofbirth=@DateofBirth "
                    + " Where Id=@Id";

                using (var sqlcm = new SqlCommand(sql))
                {
                    var results = SqlCon.SqlOrmexecute(sql,users);
                    return true;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static UserUpdate GetUser(string user)
        {
            try
            {
                var sqlcmd = new SqlCommand();
                string sql = $"Select [Id],[firstname],[lastname],[email],[dateofbirth],(SELECT FLOOR((CAST (GetDate() AS INTEGER) - CAST(dateofbirth AS INTEGER)) / 365.25)) AS Age From Users where id='" + user + "'";
                var results = SqlCon.SqlGetData<UserUpdate>(sql);

              

                return results;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static List<UserUpdate> GetUsers()
        {
            try
            {
                var sqlcmd = new SqlCommand();
                string sql = $"Select [Id],[firstname],[lastname],[email],[dateofbirth],(SELECT FLOOR((CAST (GetDate() AS INTEGER) - CAST(dateofbirth AS INTEGER)) / 365.25)) AS Age From Users ";
                var results = SqlCon.SqlGetDataList<UserUpdate>(sql);

                return results;

            }
            catch (Exception e)
            {
                  return null;
            }

        }


        public static bool DeleteUser(string user)
        {
            try
            {
                var sqlcmd = new SqlCommand();
                string sql = $"Delete From Users where id='" + user + "'";
                var results = SqlCon.SqlGetData<UserUpdate>(sql);

                return true;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }
            return age;
        }


    }
}
