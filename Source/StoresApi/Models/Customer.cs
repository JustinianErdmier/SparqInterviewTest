using System.ComponentModel.DataAnnotations;

namespace StoresApi.Models;

public sealed class Customer
{
    public int CustomerId { get; set; }

    [ DataType(DataType.EmailAddress) ]

    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string Email { get; set; } = null!;

    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int StoreId { get; set; }
}
