using ToDo.Application.DTOs.TaskItems;
using ToDo.Domain.Entities;
using AutoMapper;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Domain.Models;
using ToDo.Domain.DTOs;

namespace ToDo.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskItemViewModel>()
            .ForMember(dest => dest.TaskListTitle, opt => opt.MapFrom(src => src.TaskList.Name));


        CreateMap<TaskItemSearchModel, TaskItemSearchDto>();
        CreateMap<TaskItemViewDto, TaskItemViewModel>();

        CreateMap<TaskList, TaskListViewModel>().ReverseMap();
        CreateMap<CreateTaskListDto, TaskList>();
        CreateMap<EditTaskListDto, TaskList>();

        CreateMap<TaskListSearchModel, TaskListSearchDto>();
        CreateMap<TaskListViewDto, TaskListViewModel>();
    }
}
