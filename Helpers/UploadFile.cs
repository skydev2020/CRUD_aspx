using System;
using System.IO;
using System.Web;
using System.Web.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EF_CRUD.Helpers
{
    public class UploadFile
    {
        //private string imageLocation = WebConfigurationManager.AppSettings["ImageLocation"];
        private string webImageLocation = WebConfigurationManager.AppSettings["WebImageLocation"];
        private string accountname = WebConfigurationManager.AppSettings["AccountName"];
        private string accesskey = WebConfigurationManager.AppSettings["AccessKey"];
        private string storageConnectionString = WebConfigurationManager.AppSettings["StorageConnectionString"];

        public void UploadFileToBlobStorage(HttpPostedFileBase imageFile, string container)
        {
            string webPath = webImageLocation;

            try
            {
                String strorageconn = storageConnectionString;

                CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

                // Create Reference to Azure Blob
                CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

                // Give the new blob a name
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(imageFile.FileName);

                blockBlob.UploadFromStream(imageFile.InputStream);
            }
            catch (Exception ex)
            {

            }
        }
    }
}