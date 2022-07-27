using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class CategoryEntity
    {
        public CategoryEntity()
        {
            TopicEntities = new HashSet<TopicEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsLocked { get; set; }
        public int SortOrder { get; set; }
        public string Image { get; set; }
        public string Pinyin { get; set; }
        public long? ParentCategory { get; set; }
        public bool? Enabled { get; set; }

        public virtual ICollection<TopicEntity> TopicEntities { get; set; }
    }
}
