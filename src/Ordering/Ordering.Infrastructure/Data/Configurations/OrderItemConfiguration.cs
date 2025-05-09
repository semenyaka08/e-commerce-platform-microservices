﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(z=>z.Id);
        
        builder.Property(oi => oi.Id).HasConversion(
            orderItemId => orderItemId.Value,
            dbId => OrderItemId.Of(dbId));
        
        builder.Property(oi => oi.OrderId).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));
        
        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Price).IsRequired();
    }
}