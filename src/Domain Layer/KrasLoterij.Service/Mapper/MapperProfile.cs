using AutoMapper;
using NederlandseLoterij.KrasLoterij.Repository.Entity;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service.Mapper
{
    internal class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Lottery, LotteryDTO>();
        }
    }
}
