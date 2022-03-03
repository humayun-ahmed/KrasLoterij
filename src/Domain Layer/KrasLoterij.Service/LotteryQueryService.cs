using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using NederlandseLoterij.KrasLoterij.Repository.Entity;
using NederlandseLoterij.KrasLoterij.Service.Contracts;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service
{
    class LotteryQueryService : ILotteryQueryService
    {
        private readonly IReadOnlyRepository m_readOnlyRepository;
        private readonly IMapper m_mapper;

        public LotteryQueryService(IReadOnlyRepository readOnlyRepository, IMapper mapper)
        {
            m_readOnlyRepository = readOnlyRepository;
            m_mapper = mapper;
        }

        public async Task<List<LotteryDTO>> GetAllLotteriesAsync()
        {
            var lotteries =m_readOnlyRepository.GetItems<Lottery>();

            return await lotteries.Select(x => m_mapper.Map<LotteryDTO>(x)).ToListAsync();
        }

        public async Task<bool> IsScratchedByUserAsync(Guid userId)
        {
            return await m_readOnlyRepository.AnyAsync<Lottery>(l => l.UserId == userId);
        }
    }
}