using Infracsoft.SharedKernel.Domain.Contracts;
using Infracsoft.SharedKernel.Domain.Utilities;
using ICSharpCode.SharpZipLib.Zip;

namespace SharedKernel.Infraestructure.Services;

public sealed class ZipDecompressor : IDecompressor
{
    public async IAsyncEnumerable<CompressedEntry> Decompress(Stream compressedFile, string filename, string? password = null)
    {
        var zipFile = new ZipFile(compressedFile);
        if(password != null) zipFile.Password = password;

        foreach(ZipEntry entry in zipFile)
        {
            if (!entry.IsFile) continue;
            var entryStream = zipFile.GetInputStream(entry);
            var memoryStream = new MemoryStream();
            await entryStream.CopyToAsync(memoryStream);

            yield return new CompressedEntry(
                entry.Name,
                memoryStream
            );
        }

        /*
        using var zipInputStream = password == null
            ? new ZipInputStream(compressedFile)
            : new ZipInputStream(compressedFile) { Password = password };

        ZipEntry? entry;
        while ((entry = zipInputStream.GetNextEntry()) != null)
        {
            if (!entry.IsFile)
                continue;

            
            var entryStream = new MemoryStream();
            await zipInputStream.CopyToAsync(entryStream);
            entryStream.Position = 0;
            //entryStream.Seek(0, SeekOrigin.Begin);

            yield return new CompressedEntry(
                entry.Name,
                entryStream
            );
        }*/
    }
}