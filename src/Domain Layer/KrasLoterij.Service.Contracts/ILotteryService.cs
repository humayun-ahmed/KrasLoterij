

using System.Threading.Tasks;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service.Contracts
{
    public interface ILotteryService
    {
        Task<bool> ScratchLotteryAsync(ScratchCommand command);
    }
}