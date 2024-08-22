using ToDo.Application.DTOs.TaskItems;
using ToDo.Domain.Entities;
using AutoMapper;
using ToDo.Application.DTOs.TaskItem;
using ToDo.Application.DTOs.TaskLists;

namespace ToDo.Application.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskItemDto>()
            .ForMember(dest => dest.TaskListTitle, opt => opt.MapFrom(src => src.TaskList.Name));

        CreateMap<TaskList, TaskListDto>().ReverseMap();
        CreateMap<CreateTaskListDto, TaskList>();
        CreateMap<EditTaskListDto, TaskList>();
    }
}
