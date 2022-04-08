using System;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace zyberfox.challenge.api.Model
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Users 
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public  string LastName { get; set; }
        public string Email { get; set; }
        public String DateofBirth { get; set; }

        const int MaxLength = 128;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool AddUser(Users users,string value) {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

            try {

                if (users.FirstName.Length > MaxLength || users.LastName.Length > MaxLength) { 
                    return false;
                }

                if (!new EmailAddressAttribute().IsValid(users.Email)) {
                    return false;
                }

                bool DobVal = DateOfBirthConvert(users.DateofBirth);

                if (DobVal) {

                    if (value == "Add")
                    {
                        users.Id = SetUserId();
                        return SqlAddUser(users);
                    }
                    else
                    {
                        var UpdateObj = new UserUpdate
                        {
                            Id = users.Id,
                            FirstName = users.FirstName,
                            LastName = users.LastName,
                            Email = users.Email,
                            DateofBirth = Convert.ToDateTime(users.DateofBirth),
                        };

                        return UserUpdate.SqlUpdateUser(UpdateObj);
                    }
                }

                return false;
                
            } 
            
            catch (Exception e) {
                return false;            
            }
        
        }


        public static string SetUserId()
        {
        
            int Number = SqlCon.SqlGetData<int>("Select Count(*) from Users") + 1;
            string Id = $"ZF-{Number.ToString()}";

            int CheckId = SqlCon.SqlGetData<int>("Select Count(*) from Users where Id='"+ Id +"' ");

            if (CheckId > 0) {
                Number++;
                Id = $"ZF-{Number.ToString()}";
            }

            return Id;
        }

        public static bool SqlAddUser(Users users)
        {
            try
            {
                string sql =  $" INSERT INTO Users(Id,Firstname,Lastname,Email,Dateofbirth)"
                    + " Values(@Id,@Firstname,@Lastname,@Email,@DateofBirth) ";

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

    
        public static bool DateOfBirthDate(DateTime dtDOB) 
        {

            try {
                int age = GetAge(dtDOB);
                if (age >= 18) { return true; }

                return false;

            } catch (Exception e) {

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

        public static bool DateOfBirthConvert(string
            date) //assumes a valid date string
        {
            DateTime dtDOB = DateTime.Parse(date);
            return DateOfBirthDate(dtDOB);
        }


    }
}
