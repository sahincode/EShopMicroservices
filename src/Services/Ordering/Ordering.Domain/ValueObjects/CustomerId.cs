﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; set; }
        private CustomerId(Guid value) => Value = value;
    public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("Customer  cannot empty.");
         
            }
            return new CustomerId(value);
        }
    }
    
}
