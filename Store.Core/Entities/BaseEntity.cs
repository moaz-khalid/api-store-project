using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
