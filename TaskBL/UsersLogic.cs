using AutoMapper;
using TaskAppDAL;
using TaskAppDAL.Models;
using TaskAppDTO;

namespace TaskAppBL
{
    public class UsersLogic
    {
        private UserOperations _operations;
        private readonly IMapper _mapper;
        public UsersLogic(UserOperations operation, IMapper mapper)
        {
            _operations = operation;
            _mapper = mapper;
        }
        public List<UserItemDTO> GetAllUsers() 
        {
            var users = _operations.GetAllUsers();
            var mappedUsers = _mapper.Map<List<UserItemDTO>>(users);
            return mappedUsers;
        }
        public UserItem? AddUser(UserItemDTO user)
        {
            var userItem = _mapper.Map<UserItem>(user);
            return _operations.AddUser(userItem);
        }
        public UserItem? UpdateUser(UserItemDTO user)
        {
            var userItem = _mapper.Map<UserItem>(user);
            return _operations.UpdateUser(userItem);
        }
        public int DeleteUser(int userId)
        {
            return _operations.DeleteUser(userId);
        }
    }
    
}
