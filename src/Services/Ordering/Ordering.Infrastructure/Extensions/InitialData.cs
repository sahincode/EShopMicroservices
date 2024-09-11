

namespace Ordering.Infrastructure.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers=>
            new List<Customer> { 
                Customer.Create(CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923"),"Sahin","sahin@gmail.com")),
                Customer.Create(CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923"),"Elkin","elkin@gmail.com"))



                };
    }
}
