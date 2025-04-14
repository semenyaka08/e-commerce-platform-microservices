using BuildingBlocks.RabbitMQ.Events.Models;

namespace BuildingBlocks.RabbitMQ.Events;

public record OrderCreateEvent : IntegrationEvent
{
    public Guid OrderId { get; set; }
    
    public Guid CustomerId { get; set; }

    public string OrderName { get; set; } = string.Empty;
    
    public string FirstNameShippingAddress { get; set; } = default!;
    public string LastNameShippingAddress { get; set; } = default!;
    public string EmailAddressShippingAddress { get; set; } = default!;
    public string AddressLineShippingAddress { get; set; } = default!;
    public string CountryShippingAddress { get; set; } = default!;
    public string StateShippingAddress { get; set; } = default!;
    public string ZipCodeShippingAddress { get; set; } = default!;

    public string FirstNameBillingAddress { get; set; } = default!;
    public string LastNameBillingAddress { get; set; } = default!;
    public string EmailAddressBillingAddress { get; set; } = default!;
    public string AddressLineBillingAddress { get; set; } = default!;
    public string CountryBillingAddress { get; set; } = default!;
    public string StateBillingAddress { get; set; } = default!;
    public string ZipCodeBillingAddress { get; set; } = default!;
    
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string Cvv { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;

    public string OrderStatus { get; set; } = string.Empty;

    public List<EventOrderItem> OrderItems { get; set; } = [];
}