using StoresApi.Models;

namespace StoresApi.Services;

public sealed class StoresRepository : IRepository
{
    private static readonly List<Customer> Customers =
    [
        new()
        {
            CustomerId = 1,
            StoreId    = 1,
            Email      = "cust1@us.com"
        },
        new()
        {
            CustomerId = 2,
            StoreId    = 1,
            Email      = "cust2@us.com"
        },
        new()
        {
            CustomerId = 3,
            StoreId    = 2,
            Email      = "cust3@uk.com"
        }
    ];

    private static readonly List<Store> Stores =
    [
        new()
        {
            StoreId     = 1,
            CountryCode = "US"
        },
        new()
        {
            StoreId     = 2,
            CountryCode = "UK"
        },
        new()
        {
            StoreId     = 3,
            CountryCode = "DE"
        }
    ];

    /// <inheritdoc />
    public ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomers = false)
    {
        List<Store> filteredStores = Stores.Where(filter)
                                           .ToList();

        if (!includeCustomers)
        {
            return filteredStores;
        }

        foreach (Store store in filteredStores)
        {
            store.Customers = Customers.Where(c => c.StoreId == store.StoreId)
                                       .ToList();
        }

        return filteredStores;
    }

    /// <inheritdoc />
    public ICollection<Customer> GetCustomers(int storeId)
        => Customers.Where(c => c.StoreId == storeId)
                    .ToList();

    /// <inheritdoc />
    public Customer AddCustomer(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        customer.CustomerId = Customers.Count > 0 ? Customers.Max(c => c.CustomerId) + 1 : 1;

        Customers.Add(customer);

        return customer;
    }
}
