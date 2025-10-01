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
            var tempCompressedFilePath = Path.GetFileName(sourceCompressedFilePath);
            var tempBasePath = Path.GetFileNameWithoutExtension(sourceCompressedFilePath);

            var tempImage1Path = Path.Join(tempBasePath, "1.BMP");
            var tempImage2Path = Path.Join(tempBasePath, "2.BMP");
            var image1Id = Guid.NewGuid();
            var image2Id = Guid.NewGuid();
            var stream1 = Substitute.For<Stream>();
            var stream2 = Substitute.For<Stream>();
         
            _presuncionTempFileStore.GetFilePathsFromFolder(tempBasePath).Returns([tempImage1Path, tempImage2Path]);
            _presuncionTempFileStore.DownloadFile(tempImage1Path).Returns(stream1);
            _presuncionTempFileStore.DownloadFile(tempImage2Path).Returns(stream2);

            _guidGenerator.GenerateGuid().Returns(image1Id, image2Id);


            //Act
            await _useCase.Execute(tempCompressedFilePath, tempBasePath, sourceCompressedFilePath);


            //Assert
            Received.InOrder(async () =>
            {
                await _imagenStorageService.Received(1).Upload(image1Id.ToString(), Path.GetFileName(tempImage1Path), stream1);
                await _imagenStorageService.Received(1).Upload(image2Id.ToString(), Path.GetFileName(tempImage2Path), stream2);
                await _eventBus.Received(1).Publish(Arg.Is<DigimaxImagesStoredEvent>(x =>
                    x.CompressedFileSourcePath == sourceCompressedFilePath &&
                    x.CompressedFileTempPath == tempCompressedFilePath &&
                    x.TempBasePath == tempBasePath &&
                    x.ImagenesIds.SequenceEqual(new List<string>() { image1Id.ToString(), image2Id.ToString() })
                ));
                await _unitOfWork.SaveChangesAsync();
            });
            await _eventBus.DidNotReceive().Publish(Arg.Any<DigimaxImagesStorageFailedEvent>());

        }
    }
}
