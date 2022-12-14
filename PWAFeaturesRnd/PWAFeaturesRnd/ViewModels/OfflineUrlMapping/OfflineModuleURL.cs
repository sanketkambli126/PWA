using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.OfflineUrlMapping
{
    public class Views
    {
        public Views()
        {
            Modules = new List<Modules>();
            Url = new OfflineModuleURL();
        }
        public int ViewId { get; set; }
        public string ViewName { get; set; }
        public OfflineModuleURL Url { get; set; }
        public List<Modules> Modules { get; set; }
    }

    public class Modules
    {
        public Modules()
        {
            OfflineModuleURLs = new List<OfflineModuleURL>();
            Views = new List<Views>();
        }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public List<OfflineModuleURL> OfflineModuleURLs { get; set; }
        public List<Views> Views { get; set; }
    }
    public class OfflineModuleURL
    {
        public string requestDataString { get; set; }
        public string Url { get; set; }
        public string dbStoreName { get; set; }
        public bool toStoreInDb { get; set; }
        public bool toStoreInCache { get; set; }
    }

}
