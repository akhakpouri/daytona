using System;
using System.IO;
using System.Threading.Tasks;

namespace Daytona.FileStorage
{
    public interface IFileStorageProvider
    {
        Task<Guid> Save(Stream fileStream);
        Task<Stream> Read(Guid fileId);
        Task Delete(Guid fileId);
        Task<bool> IsExisting(Guid fileId);
    }
}
