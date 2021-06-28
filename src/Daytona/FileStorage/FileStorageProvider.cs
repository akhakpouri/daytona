using System;
using System.IO;
using System.Threading.Tasks;

namespace Daytona.FileStorage
{
    public class FileStorageProvider : IFileStorageProvider
    {
        private const string basePath = "./upload";
        public async Task<Guid> Save(Stream stream)
        {
            var id = Guid.NewGuid();
            Directory.CreateDirectory(basePath);

            var path = Path.Combine(basePath, id.ToString());
            using (var fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }

            return id;
        }

        public Task<Stream> Read(Guid fileId)
        {
            var path = Path.Combine(basePath, fileId.ToString());
            var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Task.FromResult((Stream)stream);
        }

        public Task<bool> IsExisting(Guid fileId) =>
            Task.FromResult(File.Exists(Path.Combine(basePath, fileId.ToString())));

        public Task Delete(Guid fileId)
        {
            File.Delete(Path.Combine(basePath, fileId.ToString()));
            return Task.CompletedTask;
        }
    }
}
