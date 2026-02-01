using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo_BLL.Attachment.Interface
{
    public interface IAttachment 
    {
        public string? Updload(IFormFile file ,string FolderName);
        public bool Delete(string filePath);
    }
}
