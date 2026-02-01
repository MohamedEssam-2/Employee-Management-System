using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Demo_BLL.Attachment.Interface;
using Microsoft.AspNetCore.Http;

namespace Demo_BLL.Attachment.Class
{
    public class AttachmentService : IAttachment
    {
        public List<string> AllowedExtention = [".png", ".jpg", ".jpeg"];
        const int MaxSize = 2_097_152; //2MB


        public string? Updload(IFormFile file, string FolderName)
        {
            
               
                //Check Extension
                //get file extension
                var extension = Path.GetExtension(file.FileName);
                if (!AllowedExtention.Contains(extension)) return null;


                //Check Size
                //if file is larger than max size or if file size is 0
                if (file.Length > MaxSize || file.Length == 0) return null;

                //Get Located Folder Path

                var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Image");


                //var folderPAth = $"{Directory.GetCurrentDirectory()}\\ wwwroot \\Attachments\\ FolderName)";
                //var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Image");

                //Make Attachment Name Unique --GUID
                //to avoid conflict if we have 2 files with same name
                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                //Get File Path
                //final path to store the file
                var filePath = Path.Combine(FolderPath, fileName);

                //Create File Stream To Copy File[Unmanaged]
                //Auto Dispose After Use
                //stream used to read and write bytes to files
                if (!Directory.Exists(FolderPath))
                    Directory.CreateDirectory(FolderPath);

                using FileStream stream = new FileStream(filePath, FileMode.Create);
              
                //Use Stream To Copy File
                //Copy from IFormFile to FileStream
                file.CopyTo(stream);

                //Return FileName To Store In Database
                //we return file name not full path to avoid issues if we change server
                return fileName;
          
        }
        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false;

            File.Delete(filePath);
            return true;
        }
    }
    }

