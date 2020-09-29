using System;
using System.Collections.Generic;
using System.Text;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Core.Models
{
    public class Attachment : BaseModelGuid
    {

        public string FileType { get; set; }

        public string Extension { get; set; }
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileId { get; set; }

    }
}
