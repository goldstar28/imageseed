using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageSeeds.Codes.Entities
{
    public class Tag : IComparable<Tag>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Tag)) return false;

            return Id.Equals(((Tag)obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public int ItemCount { get; set; }

        public int CompareTo(Tag other)
        {
            if (Equals(other)) return 0;
            if (Name.CompareTo(other.Name) > 0) return 1;
            return -1;
        }
    }
}
