using System.Collections.Generic;
using System.Threading.Tasks;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service.Contracts
{
    public interface ILotteryQueryService
    {
        Task<List<LotteryDTO>> GetAllLotteriesAsync();
    }
}