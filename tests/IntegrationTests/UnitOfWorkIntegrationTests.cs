using System;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Task = System.Threading.Tasks.Task;

namespace IntegrationTests
{
    public class UnitOfWorkIntegrationTests
    {
        private StateManagementContext _stateManagementContext;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<StateManagementContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _stateManagementContext = new StateManagementContext(dbOptions);
            var flowRepository = new EfRepository<Flow>(_stateManagementContext);
            var stateRepository = new EfRepository<State>(_stateManagementContext);
            var taskRepository = new EfRepository<StateTask>(_stateManagementContext);
            _unitOfWork = new UnitOfWork(flowRepository, stateRepository, taskRepository, _stateManagementContext);
        }

        [Test]
        public async Task Should_Create_New_Record()
        {
            var flow = await _unitOfWork.FlowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            await _unitOfWork.SaveChangesAsync();
            var record = await _unitOfWork.FlowRepository.Get(flow.Id);

            Assert.AreEqual(flow.Id, record.Id);
            Assert.AreEqual(flow.Title, record.Title);
        }

        [Test]
        public async Task Should_Update_Record()
        {
            var flow = await _unitOfWork.FlowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            await _unitOfWork.SaveChangesAsync();
                
            flow.Title = "Updated Flow";
            _unitOfWork.FlowRepository.Update(flow);
            await _unitOfWork.SaveChangesAsync();
            
            var record = await _unitOfWork.FlowRepository.Get(flow.Id);

            Assert.AreEqual(flow.Id, record.Id);
            Assert.AreEqual(flow.Title, record.Title);
        }

        [Test]
        public async Task Should_Get_By_Id_Record()
        {
            var flow = await _unitOfWork.FlowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            await _unitOfWork.SaveChangesAsync();
                
            var flowResult = await _unitOfWork.FlowRepository.Get(flow.Id);

            Assert.AreEqual(flow.Id, flowResult.Id);
            Assert.AreEqual(flow.Title, flowResult.Title);
        }

        [Test]
        public async Task Should_Delete_By_Id_Record()
        {
            var flow = await _unitOfWork.FlowRepository.Create(new Flow()
            {
                Id = Guid.NewGuid(),
                Title = "Flow"
            });

            await _unitOfWork.SaveChangesAsync();
            
            await _unitOfWork.FlowRepository.Delete(flow.Id);
            await _unitOfWork.SaveChangesAsync();
            
            var flowResult = await _unitOfWork.FlowRepository.Get(flow.Id);

            Assert.IsNull(flowResult);
        }
    }
}