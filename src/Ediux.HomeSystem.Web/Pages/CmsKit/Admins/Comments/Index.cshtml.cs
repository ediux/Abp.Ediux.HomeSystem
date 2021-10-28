using System;

namespace Ediux.HomeSystem.Web.Pages.CmsKit.Admins.Comments
{
    public class IndexModel : CmsKitAdminPageModel
    {
        public string EntityType { get; set; }

        public string Author { get; set; }

        public DateTime? CreationStartDate { get; set; }
        
        public DateTime? CreationEndDate { get; set; }
    }
}
