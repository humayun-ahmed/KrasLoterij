using System;
using System.Linq.Expressions;
using Moq;
using System.Threading.Tasks;
using Infrastructure.Repository.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NederlandseLoterij.KrasLoterij.Repository.Entity;

namespace NederlandseLoterij.KrasLoterij.Repository.Test
{
    [TestClass]
    public class LotteryRepositoryTest
    {
        private Mock<IReadOnlyRepository> m_repositoryMock;

        [TestInitialize]
        public void TestInitialize()
        {
            m_repositoryMock = new Mock<IReadOnlyRepository>();
        }

        [TestMethod]
        public async Task Lottery_AnyAsync()
        {
            // Arrange
            m_repositoryMock.Setup(s => s.AnyAsync<Lottery>(a => a.UserId == Guid.NewGuid())).ReturnsAsync(() => true);

            // Act
            var isAny = await m_repositoryMock.Object.AnyAsync<Lottery>(a => a.UserId == Guid.NewGuid());

            // Assert
            m_repositoryMock.Verify(m => m.AnyAsync(It.IsAny<Expression<Func<Lottery, bool>>>()), Times.Once());
        }
    }
}