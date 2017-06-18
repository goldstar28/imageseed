using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ImageSeeds.Codes.ViewModels
{
    public class NewImageViewModel
    {
        public Guid BatchId { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }

        [RegularExpression(@"([A-Za-z0-9\-]{3,30},+[,\s]*)*[A-Za-z0-9\-]{3,30}[,\s]*")]
        public string Tags { get; set; }
    }
}