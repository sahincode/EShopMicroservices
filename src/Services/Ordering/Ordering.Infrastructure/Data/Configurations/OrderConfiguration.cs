using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));
            builder.HasOne<Customer>().WithMany().HasForeignKey(o=>o.CustomerId).IsRequired();
            builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId);
            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value).HasColumnName(nameof(Order.OrderName)).
                    HasMaxLength(100).
                    IsRequired();
                });
            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(n => n.FirstName).
                    HasMaxLength(50).
                    IsRequired();
                    addressBuilder.Property(n => n.LastName).
                   HasMaxLength(50).
                   IsRequired();
                    addressBuilder.Property(n => n.EmailAddress).
                    HasMaxLength(180).
                    IsRequired(); addressBuilder.Property(n => n.Country).
                    HasMaxLength(50);
                    addressBuilder.Property(n => n.State).
                    HasMaxLength(50);
                    addressBuilder.Property(n => n.ZipCode).
                   HasMaxLength(5).
                   IsRequired();
                });
            builder.ComplexProperty(
              o => o.BillingAddress, addressBuilder =>
              {
                  addressBuilder.Property(n => n.FirstName).
                  HasMaxLength(50).
                  IsRequired();
                  addressBuilder.Property(n => n.LastName).
                 HasMaxLength(50).
                 IsRequired();
                  addressBuilder.Property(n => n.EmailAddress).
                  HasMaxLength(180).
                  IsRequired(); addressBuilder.Property(n => n.Country).
                  HasMaxLength(50);
                  addressBuilder.Property(n => n.State).
                  HasMaxLength(50);
                  addressBuilder.Property(n => n.ZipCode).
                 HasMaxLength(5).
                 IsRequired();
              });
            builder.ComplexProperty(
              o => o.Payment, paymentBuilder =>
              {
                  paymentBuilder.Property(n => n.CardName)
                  .HasMaxLength(50);
                 
                  paymentBuilder.Property(n => n.CardNumber).HasMaxLength(24).
                 IsRequired();
                  paymentBuilder.Property(n => n.Expiration).
                  HasMaxLength(10);
             
                  paymentBuilder.Property(n => n.CVV).
                  HasMaxLength(3);
                  paymentBuilder.Property(p => p.PaymentMethod);
                  
              });
            builder.Property(o=>o.Status).HasDefaultValue(OrderStatus.Draft).HasConversion(
                s=>s.ToString(),dbStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus));
            builder.Property(o => o.TotalPrice);



        }
    }
}
