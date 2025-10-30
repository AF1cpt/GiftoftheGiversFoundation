using System;

namespace GiftGivers.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; } // Made nullable
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}