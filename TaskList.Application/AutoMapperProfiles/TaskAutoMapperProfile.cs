using AutoMapper;
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
        CreateMap<TaskCreateUpdateCommand, TaskModel>();
        CreateMap<TaskSearchRequest, TaskSearchFilter>();
        CreateMap<TaskCreateUpdateRequest, TaskCreateUpdateCommand>();
        CreateMap<TaskResolveRequest, TaskResolveCommand>();
    }
}