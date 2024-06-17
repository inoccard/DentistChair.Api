using AutoMapper;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;

namespace Sds.DentistChair.Api.AutoMapper;

public class ChairMapper : Profile
{
    public ChairMapper()
    {
        CreateMap<ChairDto, ChairDto>();
    }
}
