﻿using System;

namespace Sdl.Community.GroupShareKit.Models.Response
{
    public class LanguageFiles
    {
        public Guid FileUniqueId { get; set; }
        public string FileName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int PercentComplete { get; set; }
        public string LanguageCode { get; set; }
    }
}
