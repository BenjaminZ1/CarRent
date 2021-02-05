using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
