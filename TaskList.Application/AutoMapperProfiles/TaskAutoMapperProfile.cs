using AutoMapper;
using TaskList.Dto.Enums;
using TaskList.Dto.Task;
using TaskList.Dto.Task.Commands;
using TaskList.Dto.Task.Queries;
using TaskModel = TaskList.Domain.Models.Task;

namespace TaskList.Application.AutoMapperProfiles;

public class TaskAutoMapperProfile : Profile
{
    public TaskAutoMapperProfile()
    {
        CreateMap<TaskModel, TaskDto>();
        CreateMap<TaskCreateUpdateCommand, TaskModel>()
            .ForMember(dst => dst.State, c => c.MapFrom(src => (int)src.State));
        CreateMap<TaskSearchRequest, TaskSearchFilter>()
            .ForMember(dst => dst.OrderType, c => c.MapFrom(src => (EOrderDirection)src.OrderType));
        CreateMap<TaskCreateUpdateRequest, TaskCreateUpdateCommand>()
            .ForMember(dst => dst.State, c => c.MapFrom(src => (ETaskState)src.State));
        CreateMap<TaskResolveRequest, TaskResolveCommand>();
    }
}