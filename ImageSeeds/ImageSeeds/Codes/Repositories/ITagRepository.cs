using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.Repositories
{
    public interface ITagRepository
    {
        IQueryable<Tag> GetTags();
        Tag GetTag(Guid id);
        Tag GetTag(string name);
        Tag GetTag(string name, bool create);
        bool SaveTag(Tag original);
        IList<Tag> CreateOrReturnTags(string[] name);
        bool TagExists(string name);
    }
}