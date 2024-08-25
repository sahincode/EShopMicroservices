using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public class OrderName
    {
        private const int DefalutLength = 5;
        public string Value { get; }
        private OrderName(string value) => Value = value;
        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            ArgumentOutOfRangeException.ThrowIfEqual(value.Length, DefalutLength);
            return new OrderName(value);
        }


    }
}
