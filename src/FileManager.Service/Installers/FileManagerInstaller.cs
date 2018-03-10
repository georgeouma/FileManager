using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Service.Installers
{
    public class FileManagerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<FileManager>()
                .ImplementedBy<PaymentFileManager>()
                .DependsOn(Dependency.OnValue("container", GetContainer(ConfigurationManager.AppSettings["blobStoreConnection"])))
                .LifestyleSingleton());
        }

        CloudBlobContainer GetContainer(string connectionKey)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionKey);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("paymentfiles");
            container.CreateIfNotExists();
            return container;
        }
    }
}
