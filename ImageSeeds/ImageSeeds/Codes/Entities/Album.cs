using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageSeeds.Codes.Entities
{ 
        public class Album 
        {
            public Guid Id { get; set; }
            public string FileName { get; set; }
            public string ImagePath { get; set; }
            public string ThumbnailPath { get; set; }
            public IList<Image> Images { get; set; }
            public IList<Tag> Tags { get; set; }

            public Album()
            {
                Images = new List<Image>();
                Tags = new List<Tag>();
            }
        }
   
}