using System;
using System.Collections.Generic;
using System.Text;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreSearchRequestDto : AbpSearchRequestDto
    {
        public Guid? Classification_Id { get; set; }

        public Guid? CurrentUser_Id { get; set; }

        public string ContextType { get; set; }

        public int MIMETypeId { get; set; }
    }
}
