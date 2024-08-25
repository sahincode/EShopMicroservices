using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public class OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value) => Value = value;
        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("OrderItemId can not be null");
            }
            return new OrderItemId(value);
        }
           
    }
}
