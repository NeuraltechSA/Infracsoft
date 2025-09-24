using Importacion.Domain.Tests.Presunciones.Mothers;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.CheckSourceDigimax;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using NSubstitute;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacion.Application.Tests.Presunciones.Digimax.UseCases.CheckSourceDigimax
{
    public sealed class TestCheckSourceDigimaxUseCase
    {
        private CheckSourceDigimaxUseCase _useCase;
        private IPresuncionFileSource _presuncionFileSource;
        private IEventBus _eventBus;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _presuncionFileSource = Substitute.For<IPresuncionFileSource>();
            _eventBus = Substitute.For<IEventBus>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _useCase = new CheckSourceDigimaxUseCase(
                _presuncionFileSource,
                _eventBus,
                _unitOfWork
            );
        }

        [Test]
        public async Task CheckSourceDigimax_WithThreeFiles_PublishesEvents()
        {
            //Arrange
            var path1 = PresuncionDigimaxSourcePathMother.Create();
            var path2 = PresuncionDigimaxSourcePathMother.Create();
            var path3 = PresuncionDigimaxSourcePathMother.Create();
            _presuncionFileSource.GetAllFilePathsRecursive().Returns([path1, path2, path3]);

            //Act
            await _useCase.Execute();

            //Assert
            Received.InOrder(async () =>
            {
                await _eventBus.Received(1).Publish(new PresuncionDigimaxUploadedEvent(path1));
                await _eventBus.Received(1).Publish(new PresuncionDigimaxUploadedEvent(path2));
                await _eventBus.Received(1).Publish(new PresuncionDigimaxUploadedEvent(path3));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }

        [Test]
        public async Task CheckSourceDigimax_WithNoFiles_DoesNotPublishEvents()
        {
            //Arrange
            _presuncionFileSource.GetAllFilePathsRecursive().Returns(new List<string>());

            //Act
            await _useCase.Execute();

            //Assert
            await _eventBus.DidNotReceive().Publish(Arg.Any<PresuncionDigimaxUploadedEvent>());
        }

        [Test]
        public async Task CheckSourceDigimax_WithFileWithInvalidFormat_DoesNotPublishEvents()
        {
            //Arrange
            var invalidPath = PresuncionDigimaxSourcePathMother.CreateInvalidFormat();
            _presuncionFileSource.GetAllFilePathsRecursive().Returns([invalidPath]);

            //Act
            await _useCase.Execute();

            //Assert
            await _eventBus.DidNotReceive().Publish(Arg.Any<PresuncionDigimaxUploadedEvent>());
        }

    }
}
