﻿using FastFood.Domain.Commons;

namespace FastFood.Domain.Entities.Attachments
{
    public class Attachment : Auditable
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}
