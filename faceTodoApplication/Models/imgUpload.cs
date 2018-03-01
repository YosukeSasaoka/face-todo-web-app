using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure;
using faceTodoApplication.Models;
using System.Threading;

namespace faceTodoApplication.Models
{
    public class imgUpload
    {
        public string ImgUpload(HttpContext context,string accontName,string key)
        {
            var account = new CloudStorageAccount(
                new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
                    accontName,
                    key),
                true);
            var blobClient = account.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("person");
            string imgPath = "";
            foreach (var file in context.Request.Form.Files)
            {
                var name = System.IO.Path.GetFileName(DateTime.Now.ToString("yyyyMMddHHmmssfff")+file.FileName);
                using (var s = file.OpenReadStream())
                {
                    var blockBlob = blobContainer.GetBlockBlobReference(name);
                        blockBlob.UploadFromStreamAsync(s);
                    Thread.Sleep(3000);
                    imgPath = blockBlob.Uri.ToString();
                }
            }
            return imgPath;                     
        }
    }
}
