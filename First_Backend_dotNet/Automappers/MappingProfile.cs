using AutoMapper;
using First_Backend_dotNet.DTOs;
using First_Backend_dotNet.Models;

namespace First_Backend_dotNet.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDto, Beer>(); // para mapear campos con el mismo nombre
            CreateMap<Beer, BeerDto>().ForMember(dto => dto.Id, m => m.MapFrom(b => b.BeerId)); // para mappear campos con nombres diferentes
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
