using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.Importacion.Domain.Presunciones.Services;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.SharedKernel.Domain.Contracts;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Importacion.Application.Tests.Presunciones.Digimax.UseCases.StoreDigimaxImages
{
    public sealed class TestStoreDigimaxImagesUseCase
    {
        private StoreDigimaxImagesUseCase _useCase;
        private PresuncionDigimaxImagenStore _presuncionDigimaxImagenStore;
        private IEventBus _eventBus;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _presuncionDigimaxImagenStore = Substitute.For<PresuncionDigimaxImagenStore>(
                Substitute.For<IPresuncionTempStore>(),
                Substitute.For<IGuidGenerator>(),
                Substitute.For<ImagenStore>()
            );
            _eventBus = Substitute.For<IEventBus>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            
            _useCase = new StoreDigimaxImagesUseCase(
                _presuncionDigimaxImagenStore,
                _eventBus,
                _unitOfWork
            );
        }

        [Test]
        public async Task StoreImages_WithValidImages_PublishesSuccessEvent()
        {
            //Arrange
            var tempFilePath = "0123DIGIMAX.zip";
            var tempBasePath = "0123DIGIMAX";
            var originalSourcePath = "source/0123DIGIMAX.zip";
            var presuncionId = "presuncion-123";

            _presuncionDigimaxImagenStore.StoreImages(tempBasePath, string.Empty)
                .Returns(presuncionId);

            //Act
            await _useCase.Execute(tempFilePath, tempBasePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _presuncionDigimaxImagenStore.Received(1).StoreImages(tempBasePath, string.Empty);
                await _eventBus.Received(1).Publish(new PresuncionDigimaxImagesStoredEvent(presuncionId, originalSourcePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }

        [Test]
        public async Task StoreImages_WhenStoreImagesFails_PublishesFailureEvent()
        {
            //Arrange
            var tempFilePath = "0123DIGIMAX.zip";
            var tempBasePath = "0123DIGIMAX";
            var originalSourcePath = "source/0123DIGIMAX.zip";

            _presuncionDigimaxImagenStore.StoreImages(tempBasePath, string.Empty)
                .Throws(new InvalidOperationException("Invalid image count"));

            //Act
            await _useCase.Execute(tempFilePath, tempBasePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _presuncionDigimaxImagenStore.Received(1).StoreImages(tempBasePath, string.Empty);
                await _eventBus.Received(1).Publish(new DigimaxImagesStorageFailedEvent(tempBasePath, tempFilePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }

        [Test]
        public async Task StoreImages_WhenTempStoreFails_PublishesFailureEvent()
        {
            //Arrange
            var tempFilePath = "0123DIGIMAX.zip";
            var tempBasePath = "0123DIGIMAX";
            var originalSourcePath = "source/0123DIGIMAX.zip";

            _presuncionDigimaxImagenStore.StoreImages(tempBasePath, string.Empty)
                .Throws(new FileNotFoundException("Temp files not found"));

            //Act
            await _useCase.Execute(tempFilePath, tempBasePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _presuncionDigimaxImagenStore.Received(1).StoreImages(tempBasePath, string.Empty);
                await _eventBus.Received(1).Publish(new DigimaxImagesStorageFailedEvent(tempBasePath, tempFilePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }

        [Test]
        public async Task StoreImages_WhenImagenStoreFails_PublishesFailureEvent()
        {
            //Arrange
            var tempFilePath = "0123DIGIMAX.zip";
            var tempBasePath = "0123DIGIMAX";
            var originalSourcePath = "source/0123DIGIMAX.zip";

            _presuncionDigimaxImagenStore.StoreImages(tempBasePath, string.Empty)
                .Throws(new IOException("Storage failed"));

            //Act
            await _useCase.Execute(tempFilePath, tempBasePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _presuncionDigimaxImagenStore.Received(1).StoreImages(tempBasePath, string.Empty);
                await _eventBus.Received(1).Publish(new DigimaxImagesStorageFailedEvent(tempBasePath, tempFilePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }
    }
}
