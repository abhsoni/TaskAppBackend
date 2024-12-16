using AutoMapper;
using TaskAppDAL.Models;
using TaskAppDTO;

namespace TaskAppBL.Mappers
{
    public class BLMapper: Profile
    {
        public BLMapper() 
        {
            CreateMap<TaskItem,TaskItemDTO>().ReverseMap();
            CreateMap<UserItem,UserItemDTO>().ReverseMap();
        }
    }
}
