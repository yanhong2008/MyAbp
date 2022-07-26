﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Zhaoxi.Forum.Domain
{
    public class BaseEntity:Entity<long>
    {
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
