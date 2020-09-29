using System;
using System.Collections.Generic;
using System.Text;

namespace SuhailApps.Core.ViewModels
{
    public class AttachmentViewModel
    {
        public Guid? AttachmentId { get; set; }
        public byte[] FileContent { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }
    }
}
