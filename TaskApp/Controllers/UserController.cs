using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskAppBL;
using TaskAppDAL.Models;
using TaskAppDTO;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UsersLogic _logic;
        private readonly IMapper _mapper;
        public UserController(UsersLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var userList = _logic.GetAllUsers();
            return Ok(new { users = userList });
        }
        [HttpPost]
        public IActionResult CreateUser(UserItem userData)
        {
            var userDto = _mapper.Map<UserItemDTO>(userData);
            try
            {
                var user = _logic.AddUser(userDto);
                if (user == null)
                {
                    return BadRequest();
                }
                return Ok(new { success = true, message = "User added successfully.", userResult = user });
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        [HttpPut]
        public IActionResult UpdateUser(UserItem userData) 
        {
            var userDto = _mapper.Map<UserItemDTO>(userData);
            var updatedUser=_logic.UpdateUser(userDto);
            if (updatedUser == null)
            {
                return BadRequest();
            }
            return Ok(new { success = true, message = "User updated successfully.", updateUserResult = updatedUser });

        }
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId) 
        {
            int deletedUserFlag = _logic.DeleteUser(userId);
            if (deletedUserFlag == 0) 
            {
                return BadRequest();
            }
            return Ok(new { success = true, message = "User deleted successfully."});
        }
        //private readonly DatabaseConnection _dbConnection;
        //public UserController(IConfiguration configuration)
        //{
        //    string connectionString = configuration.GetConnectionString("TaskDbConnection");
        //    _dbConnection = new DatabaseConnection(connectionString);
        //}
        //[HttpGet]
        //public IActionResult GetUsers()
        //{
        //    string query = "SELECT * FROM Users";
        //    var usersList = new List<UserItem>();
        //    _dbConnection.Read(query, reader =>
        //    {
        //        while (reader.Read()) { 
        //            usersList.Add(new UserItem{
        //                UserId = Convert.ToInt32(reader["UserId"]),
        //                Name = reader["Name"].ToString(),
        //                Email = reader["Email"].ToString(),
        //            });
        //        }
        //    });
        //    return Ok(new { users = usersList }); 
        //}
        //[HttpPost]
        //public IActionResult CreateUser(UserItem user) {
        //    string query = "INSERT INTO Users (Name,Email) VALUES (@Name,@Email)";
        //    SqlParameter[] parameters =
        //    {
        //        new("@Name",user.Name),
        //        new("@Email",user.Email)
        //    };
        //    int rowInserted = _dbConnection.ExecuteNonQuery(query,parameters);
        //    if (rowInserted > 0)
        //    {
        //        return Ok(new {success=true,message="User added successfully."});
        //    }
        //    else
        //    {
        //        return BadRequest(new { success = false, message = "Failed to add user." });
        //    }
        //}
        //[HttpPut]
        //public IActionResult UpdateUser(UserItem user) {
        //    string query = "UPDATE Users SET Name=@Name, Email=@Email WHERE UserId=@UserId";
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@UserId",user.UserId),
        //        new SqlParameter("@Name",user.Name),
        //        new SqlParameter("@Email",user.Email)
        //    };
        //    int rowUpdated = _dbConnection.Update(query, parameters);
        //    if (rowUpdated > 0) {
        //        return Ok(new { success = true, message = "User updated Successfully!" });
        //    }
        //    else
        //    {
        //        return BadRequest(new { success = false, message = "Failed to update user." });
        //    }
        //}
        //[HttpDelete("{userId}")]
        //public IActionResult DeleteUser(int userId)
        //{
        //    string query = "DELETE FROM Users WHERE UserId=@userId";
        //    SqlParameter[] parameters =
        //    {
        //        new SqlParameter("@UserId",userId)
        //    };
        //    int rowDeleted = _dbConnection.Delete(query, parameters);
        //    if (rowDeleted > 0) {
        //        return Ok(new { success = true, message = "User deleted successfully!" });
        //    }
        //    else
        //    {
        //        return BadRequest(new { success = false, message = "Failed to delete user!" });
        //    }
        //}
    }
}
