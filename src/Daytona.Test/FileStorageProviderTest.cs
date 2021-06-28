using Daytona.FileStorage;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Daytona.Test
{
    public class FileStorageProviderTest
    {
        private readonly IFileStorageProvider _fileStorageProvider;

        public FileStorageProviderTest()
        {
            _fileStorageProvider = new FileStorageProvider();
        }

        [Fact]
        public async Task File_Storage_Test()
        {
            var enc = Encoding.ASCII.GetBytes("Hello World");
            var stream = new MemoryStream(enc);
            var guid = await _fileStorageProvider.Save(stream);
            Assert.True(guid != Guid.Empty);
            Assert.NotNull(await _fileStorageProvider.Read(guid));
            Assert.True(await _fileStorageProvider.IsExisting(guid));
        }
    }
}
