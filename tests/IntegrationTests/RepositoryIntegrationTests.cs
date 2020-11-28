using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces.Data;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace IntegrationTests
{
    public class RepositoryIntegrationTests
    {
        private StateManagementContext _stateManagementContext;
        private IRepository<Flow> _flowRepository;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<StateManagementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _stateManagementContext = new StateManagementContext(dbOptions);
            _flowRepository = new EfRepository<Flow>(_stateManagementContext);
        }

        [Test]
        public async Task Should_Create_New_Record()
        {
            var flow = await _flowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            var list = await _flowRepository.Get();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(flow.Id, list.First().Id);
            Assert.AreEqual(flow.Title, list.First().Title);
        }

        [Test]
        public async Task Should_Update_Record()
        {
            var flow = await _flowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            flow.Title = "Updated Flow";
            await _flowRepository.Update(flow);

            var list = await _flowRepository.Get();

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(flow.Id, list.First().Id);
            Assert.AreEqual(flow.Title, list.First().Title);
        }

        [Test]
        public async Task Should_Get_By_Id_Record()
        {
            var flow = await _flowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            var flowResult = await _flowRepository.Get(flow.Id);

            Assert.AreEqual(flow.Id, flowResult.Id);
            Assert.AreEqual(flow.Title, flowResult.Title);
        }
        
        [Test]
        public async Task Should_Delete_By_Id_Record()
        {
            var flow = await _flowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            await _flowRepository.Delete(flow.Id);
            
            var flowResult = await _flowRepository.Get(flow.Id);

            Assert.IsNull(flowResult);
        }
    }
}