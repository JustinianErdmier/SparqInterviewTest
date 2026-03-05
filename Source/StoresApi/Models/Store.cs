using JetBrains.Annotations;

namespace StoresApi.Models;

public sealed class Store
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string CountryCode { get; set; } = null!;

    public ICollection<Customer> Customers { [ UsedImplicitly ] get; set; } = [];

    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public int StoreId { get; set; }
}
