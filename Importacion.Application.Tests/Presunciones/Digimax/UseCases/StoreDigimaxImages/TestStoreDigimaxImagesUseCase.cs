using Importacion.Domain.Tests.Presunciones.Mothers;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.StoreDigimaxImages;
using Infracsoft.Importacion.Domain.Imagenes.Contracts;
using Infracsoft.Importacion.Domain.Imagenes.Services;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.SharedKernel.Domain.Contracts;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SharedKernel.Domain.Contracts;

namespace Importacion.Application.Tests.Presunciones.Digimax.UseCases.StoreDigimaxImages
{
    public sealed class TestStoreDigimaxImagesUseCase
    {
        private StoreDigimaxImagesUseCase _useCase;
        private IImagenStorageService _imagenStorageService;
        private IPresuncionTempFileStore _presuncionTempFileStore;
        private IGuidGenerator _guidGenerator;
        private IEventBus _eventBus;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _guidGenerator = Substitute.For<IGuidGenerator>();
            _imagenStorageService = Substitute.For<IImagenStorageService>();
            _presuncionTempFileStore = Substitute.For<IPresuncionTempFileStore>();
            _eventBus = Substitute.For<IEventBus>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            
            _useCase = new StoreDigimaxImagesUseCase(
                _imagenStorageService,
                _presuncionTempFileStore,
                _guidGenerator,
                _eventBus,
                _unitOfWork
            );
        }

        [Test]
        public async Task StoreImages_WithValidImages_UploadsImagesAndPublishesEvents()
        {
            //Arrange
            var sourceCompressedFilePath = PresuncionDigimaxSourcePathMother.Create();
            var tempBasePath = Path.GetFileNameWithoutExtension(sourceCompressedFilePath);
            var expectedFilename = Path.GetFileName(sourceCompressedFilePath);
            var tempFilePaths = new[] { "A.png" };
            var imageId = new Guid("d68f563d-7b5d-4296-b08d-091cd49f823f");

            _presuncionTempFileStore.GetFilePathsFromFolder(tempBasePath).Returns(tempFilePaths);
            _guidGenerator.GenerateGuid().Returns(imageId);

            //Act


            //Assert
            foreach (var filePath in tempFilePaths) { 
                var stream = await _presuncionTempFileStore.Received(1).DownloadFile(filePath);
                await _imagenStorageService.Received(1).Upload(imageId.ToString(), expectedFilename, stream);
            }

        }
        /*
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
        }*/
    }
}
