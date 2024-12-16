using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TaskAppDAL.Models;
using TaskAppDTO;

namespace TaskAppDAL
{
    public class UserOperations
    {
        private readonly TaskAppDatabaseConnection _connection;
        public UserOperations(IOptions<ConnectionStrings> options)
        {
            string connectionString = options.Value.TaskDbConnection;
            _connection = new TaskAppDatabaseConnection(connectionString);
        }
        public List<UserItem> GetAllUsers() 
        {
            string query = "SELECT * FROM Users";
            var usersList = new List<UserItem>();
            _connection.Read(query, reader =>
            {
                while (reader.Read())
                {
                    usersList.Add(new UserItem
                    {
                        UserId = Convert.ToInt32(reader["UserId"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                    });
                }
            });
            return usersList;
        }
        public UserItem? AddUser(UserItem userData)
        {
            string checkUserExistsQuery = "SELECT COUNT(*) FROM Users WHERE Email=@Email";
            SqlParameter[] emailParameter = {new("@Email",userData.Email)};
            int existingUserCount = _connection.ExecuteScalar<int>(checkUserExistsQuery, emailParameter);
            if (existingUserCount > 0) {
                throw new CustomException(message: string.Format("User with Email Id: {0} already exists", userData.Email), statusCode:409);
            }
            else
            {
                string query = "INSERT INTO Users (Name,Email) VALUES (@Name,@Email)";
                SqlParameter[] parameters =
                {
                    new("@Name",userData.Name),
                    new("@Email",userData.Email)
                };
                int rowInserted = _connection.ExecuteNonQuery(query, parameters);
                if (rowInserted == -1)
                {
                    throw new CustomException(message:"Failed to create the user.", statusCode:500);
                }
                var createdUser = new UserItem();
                string readQuery = "SELECT TOP 1 * FROM Users ORDER BY UserId DESC";
                _connection.Read(readQuery, reader =>
                {
                    while (reader.Read())
                    {
                        createdUser = new UserItem
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                });
                return createdUser;
            }   
        }
        public UserItem? UpdateUser(UserItem user)
        {
            string query = "UPDATE Users SET Name=@Name, Email=@Email WHERE UserId=@UserId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId",user.UserId),
                new SqlParameter("@Name",user.Name),
                new SqlParameter("@Email",user.Email)
            };
            int rowUpdated = _connection.Update(query, parameters);
            if (rowUpdated > 0)
            {
                string readQuery = "SELECT * FROM Users WHERE UserId="+user.UserId;
                var updatedUser=new UserItem();
                _connection.Read(readQuery, reader =>
                {
                    while (reader.Read())
                    {
                        updatedUser = new UserItem
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                });
                return updatedUser;
            }
            else
            {
                return null;
            }
        }
        public int DeleteUser(int userId)
        {
            string query = "DELETE FROM Users WHERE UserId=@userId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UserId",userId)
            };
            int rowDeleted = _connection.Delete(query, parameters);
            if (rowDeleted > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
