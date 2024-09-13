namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer> {
                Customer.Create(CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923")),"Sahin","sahin@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6ab18923")),"Elkin","elkin@gmail.com")



                };
        public static IEnumerable<Product> Products =>
           new List<Product> {
                Product.Create(ProductId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923")),"Iphone",1000),
                Product.Create(ProductId.Of(new Guid("d43ed3e4-b474-4753-80ac-a2164a7f767c")),"Samsung",500),
                Product.Create(ProductId.Of(new Guid("be1bece4-5708-4daf-92e5-5f9fabf51a4a")),"Samsung",500),
                Product.Create(ProductId.Of(new Guid("7ea9b3fb-1f01-48b4-9e97-8346fe96caea")),"Samsung",500)




               };
        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
                var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

                var payment1 = Payment.Of("mehmet", "5555555555554444", "12/28", "355", 1);
                var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", 2);

                var order1 = Order.Create(
                                OrderId.Of(Guid.NewGuid()),
                                CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923")),
                                OrderName.Of("ORD_1"),
                                shippingAddress: address1,
                                billingAddress: address1,
                                payment1);
                order1.Add(ProductId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6cc18923")), 2, 1000);
                order1.Add(ProductId.Of(new Guid("d43ed3e4-b474-4753-80ac-a2164a7f767c")), 1, 500);

                var order2 = Order.Create(
                                OrderId.Of(Guid.NewGuid()),
                                CustomerId.Of(new Guid("f14d320f-8f44-4b3a-aa9f-8c0c6ab18923")),
                                OrderName.Of("ORD_2"),
                                shippingAddress: address2,
                                billingAddress: address2,
                                payment2);
                order2.Add(ProductId.Of(new Guid("be1bece4-5708-4daf-92e5-5f9fabf51a4a")), 1, 500);
                order2.Add(ProductId.Of(new Guid("7ea9b3fb-1f01-48b4-9e97-8346fe96caea")), 2, 500);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
