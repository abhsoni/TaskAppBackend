using AutoMapper;
using TaskApp.ApiModels;
using TaskAppDTO;

namespace TaskApp.Mappers
{
    public class ApiMapper: Profile
    {
        public ApiMapper() {
            CreateMap<ApiTaskItem, TaskItemDTO>().ReverseMap();
            CreateMap<ApiUserItem, UserItemDTO>().ReverseMap();
        }
    }
}
