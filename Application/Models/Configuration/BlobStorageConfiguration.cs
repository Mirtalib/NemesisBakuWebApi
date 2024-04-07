using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Configuration
{
    public class BlobStorageConfiguration
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
