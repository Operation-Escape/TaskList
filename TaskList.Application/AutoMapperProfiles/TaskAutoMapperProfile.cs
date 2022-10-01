using AutoMapper;
using TaskList.Dto.Task;
using TaskModel = TaskList.Domain.Model.Task;

namespace TaskList.Application.AutoMapperProfiles;

public class TaskAutoMapperProfile : Profile
{
    public TaskAutoMapperProfile()
    {
        CreateMap<TaskModel, TaskDto>();
        CreateMap<TaskCreateUpdateCommand, TaskModel>();
    }
}