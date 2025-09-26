using Importacion.Domain.Tests.Presunciones.Mothers;
using Infracsoft.Importacion.Application.Presunciones.Digimax.UseCases.DecompressDigimaxTempFile;
using Infracsoft.Importacion.Domain.Presunciones.Contracts;
using Infracsoft.Importacion.Domain.Presunciones.Events;
using Infracsoft.Importacion.Domain.Presunciones.Events.Failure;
using Infracsoft.SharedKernel.Domain.Contracts;
using Infracsoft.SharedKernel.Domain.Utilities;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SharedKernel.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Importacion.Application.Tests.Presunciones.Digimax.UseCases.DecompressDigimaxFile
{
    public sealed class TestDecompressDigimaxFileUseCase
    {
        private DecompressDigimaxFileUseCase _useCase;
        private IDecompressor _decompressor;
        private IPresuncionTempFileStore _tempStore;
        private IEventBus _eventBus;
        private IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _decompressor = Substitute.For<IDecompressor>();
            _tempStore = Substitute.For<IPresuncionTempFileStore>();
            _eventBus = Substitute.For<IEventBus>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _configuration = Substitute.For<IConfiguration>();
            
            _useCase = new DecompressDigimaxFileUseCase(
                _decompressor,
                _tempStore,
                _eventBus,
                _unitOfWork,
                _configuration
            );
        }

        private async IAsyncEnumerable<CompressedEntry> GetAsyncEntries(List<CompressedEntry> files)
        {
            foreach(var file in files)
            {
                yield return await Task.FromResult(file);
            }
        }

        [Test]
        public async Task Decompress_WithValidFiles_StoresTempFileAndPublishesSuccessEvent()
        {
            //Arrange
            var originalSourcePath = PresuncionDigimaxSourcePathMother.Create();
            var tempFilePath = Path.GetFileName(originalSourcePath);
            var expectedTempBasePath = Path.GetFileNameWithoutExtension(originalSourcePath);
            var password = PresuncionDigimaxCompressedFilePasswordMother.Create();
            var compressedEntries = new List<CompressedEntry>
            {
                new("1.BMP", Substitute.For<Stream>()),
                new("2.BMP", Substitute.For<Stream>()),
            };
            var compressedFile = Substitute.For<Stream>();

            _configuration["Digimax:FilePassword"].Returns(password);
            _tempStore
                .DownloadFile(tempFilePath)
                .Returns(compressedFile);
            _decompressor
                .Decompress(compressedFile, Path.GetFileName(tempFilePath), password)
                .Returns(GetAsyncEntries(compressedEntries));

            //Act
            await _useCase.Execute(tempFilePath, originalSourcePath);


            //Assert
            Received.InOrder(async () =>
            {
                foreach (var entry in compressedEntries)
                {
                    await _tempStore.Received(1).Store(Path.Combine(expectedTempBasePath, entry.Path), entry.Content);
                }
                await _eventBus.Received(1).Publish(new DecompressedDigimaxFileEvent(tempFilePath, expectedTempBasePath, originalSourcePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
        }

        [Test]
        public async Task Decompress_WhenDownloadFails_PublishesFailureEvent()
        {
            //Arrange
            var originalSourcePath = PresuncionDigimaxSourcePathMother.Create();
            var tempFilePath = Path.GetFileName(originalSourcePath);
            _tempStore.DownloadFile(tempFilePath).Throws(new FileNotFoundException("File not found"));

            //Act
            await _useCase.Execute(tempFilePath, originalSourcePath);

            //Assert
            Received.InOrder(async () => {
                await _tempStore.Received(1).DownloadFile(tempFilePath);
                await _eventBus.Received(1).Publish(new DigimaxDecompressionFailedEvent(tempFilePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
            await _tempStore.DidNotReceive().Store(Arg.Any<string>(), Arg.Any<Stream>());
        }

        [Test]
        public async Task Decompress_WhenDecompressionFails_PublishesFailureEvent()
        {
            //Arrange
            var originalSourcePath = PresuncionDigimaxSourcePathMother.Create();
            var tempFilePath = Path.GetFileName(originalSourcePath);
            var password = PresuncionDigimaxCompressedFilePasswordMother.Create();
            var compressedFile = Substitute.For<Stream>();

            _configuration["Digimax:FilePassword"].Returns(password);
            _tempStore.DownloadFile(tempFilePath).Returns(compressedFile);
            _decompressor.Decompress(compressedFile, Path.GetFileName(tempFilePath), password)
                .Throws(new InvalidDataException("Corrupted file"));

            //Act
            await _useCase.Execute(tempFilePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _tempStore.Received(1).DownloadFile(tempFilePath);
                await _eventBus.Received(1).Publish(new DigimaxDecompressionFailedEvent(tempFilePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
            await _tempStore.DidNotReceive().Store(Arg.Any<string>(), Arg.Any<Stream>());
        }

        [Test]
        public async Task Decompress_WithEmptyCompressedFile_PublishesSuccessEvent()
        {
            //Arrange
            var originalSourcePath = PresuncionDigimaxSourcePathMother.Create();
            var tempFilePath = Path.GetFileName(originalSourcePath);
            var expectedTempBasePath = Path.GetFileNameWithoutExtension(originalSourcePath);
            var password = PresuncionDigimaxCompressedFilePasswordMother.Create();
            var compressedFile = Substitute.For<Stream>();

            _configuration["Digimax:FilePassword"].Returns(password);
            _tempStore.DownloadFile(tempFilePath).Returns(compressedFile);
            _decompressor.Decompress(compressedFile, Path.GetFileName(tempFilePath), password)
                .Returns(GetAsyncEntries(new List<CompressedEntry>()));

            //Act
            await _useCase.Execute(tempFilePath, originalSourcePath);

            //Assert
            Received.InOrder(async () =>
            {
                await _tempStore.Received(1).DownloadFile(tempFilePath);
                await _eventBus.Received(1).Publish(new DecompressedDigimaxFileEvent(tempFilePath, expectedTempBasePath, originalSourcePath));
                await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
            });
            await _tempStore.DidNotReceive().Store(Arg.Any<string>(), Arg.Any<Stream>());
        }
    }
}
