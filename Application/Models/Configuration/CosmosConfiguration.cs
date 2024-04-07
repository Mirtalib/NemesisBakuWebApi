using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Configuration
{
    public class CosmosConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Uri { get; set; }
        public string Key { get; set; }
    }
}
