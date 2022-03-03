using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Repository.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NederlandseLoterij.KrasLoterij.Repository.Entity;
using NederlandseLoterij.KrasLoterij.Service.Contracts;
using NederlandseLoterij.KrasLoterij.Service.Contracts.DTO;

namespace NederlandseLoterij.KrasLoterij.Service.Test
{
    [TestClass]
    public class LotteryQueryServiceTests
    {
        [TestMethod]
        public async Task Scratch_Fail()
        {
            ScratchCommand command = new ScratchCommand();
            var mockRepository = new Mock<IReadOnlyRepository>();

            var mockMapper = new Mock<IMapper>();

            var service = new LotteryQueryService(mockRepository.Object, mockMapper.Object);
            
            await service.GetAllLotteriesAsync();
        }
    }
}