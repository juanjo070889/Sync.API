using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sync.API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string DefaultCulture { get; set; }
        public string ShopifyAPIEndpoint { get; set; }
        public string APIkey { get; set; }
        public string APIpass { get; set; }
    }
}
