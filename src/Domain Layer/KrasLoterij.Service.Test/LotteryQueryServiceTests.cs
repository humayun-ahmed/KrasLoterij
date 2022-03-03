using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Repository.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NederlandseLoterij.KrasLoterij.Repository.Entity;

namespace NederlandseLoterij.KrasLoterij.Service.Test
{
    [TestClass]
    public class LotteryQueryServiceTests
    {
        [TestMethod]
        public async Task Scratch_Fail()
        {
         
            var mockRepository = new Mock<IReadOnlyRepository>();
            mockRepository.Setup(s => s.AnyAsync<Lottery>(a => a.UserId == It.IsAny<Guid>())).ReturnsAsync(() => false);

            var mockMapper = new Mock<IMapper>();

            var service = new LotteryQueryService(mockRepository.Object, mockMapper.Object);

            var isScratched = await service.IsScratchedByUserAsync(Guid.NewGuid());

            mockRepository.Verify(m => m.AnyAsync(It.IsAny<Expression<Func<Lottery, bool>>>()), Times.Once());
        }
    }
}