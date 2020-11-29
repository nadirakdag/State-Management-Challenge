using API.Models.ResponseModel;
using AutoMapper;
using Domain.Entities;

namespace API.Models
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Flow, FlowResponseModel>();
            CreateMap<State, StateResponseModel>()
                .ForMember(dest => dest.FlowTitle, opt => opt.MapFrom(src => src.Flow.Title))
                .ForMember(dest => dest.NextStateTitle, opt => opt.MapFrom(src => src.NextState.Title))
                .ForMember(dest => dest.PrevStateTitle, opt => opt.MapFrom(src => src.PrevState.Title));

            CreateMap<StateTask, TaskResponseModel>()
                .ForMember(dest => dest.FlowTitle, opt => opt.MapFrom(src => src.Flow.Title))
                .ForMember(dest => dest.StateTitle, opt => opt.MapFrom(src => src.State.Title));
        }
    }
}