using System;
using System.ComponentModel.DataAnnotations;

namespace CarRent.Common.Domain
{
    public class EntityBase
    {
        [Required]
        public Guid Guid { get; }

        protected EntityBase()
        {
            Guid = Guid.NewGuid();
        }
    }
}
