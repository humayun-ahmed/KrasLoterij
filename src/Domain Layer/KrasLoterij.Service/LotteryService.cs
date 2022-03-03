using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Infrastructure.Repository.Contracts;
using NederlandseLoterij.KrasLoterij.Repository.Entity;
using NederlandseLoterij.KrasLoterij.Service.Contracts;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service
{
    class LotteryService : ILotteryService
    {
        private readonly IRepository m_repository;

        public LotteryService(IRepository repository)
        {
            m_repository = repository;
        }
        public async Task<bool> ScratchLotteryAsync(ScratchCommand command)
        {
            var lottery = await m_repository.GetItem<Lottery>(x => x.Id == command.Id);
            if (lottery == null)
            {
                throw new ValidationException("Lottery item not found");
            }

            if (lottery.UserId != null)
            {
                throw new ValidationException("This lottery is already scratched.");
            }

            lottery.UserId = command.UserId;
            await m_repository.SaveChanges();

            return true;
        }
    }
}