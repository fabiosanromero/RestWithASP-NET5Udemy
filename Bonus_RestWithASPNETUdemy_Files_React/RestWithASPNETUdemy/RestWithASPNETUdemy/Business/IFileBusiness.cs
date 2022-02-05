using Microsoft.AspNetCore.Http;
using RestWithASPNETUdemy.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string filename);
        public Task<FileDetailDTO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailDTO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
