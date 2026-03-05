using JetBrains.Annotations;

using StoresApi.Models;

namespace StoresApi.Services;

/// <summary>Represents a contract for managing data operations related to stores and customers.</summary>
public interface IRepository
{
    /// <summary>Retrieves a collection of stores based on the specified filter criteria. Optionally includes associated customer data if specified.</summary>
    /// <param name="filter">A function that defines the criteria for filtering the stores.</param>
    /// <param name="includeCustomers">A boolean value indicating whether to include associated customer data. Default is false.</param>
    /// <returns>A collection of stores that match the specified criteria.</returns>
    ICollection<Store> GetStores(Func<Store, bool> filter, bool includeCustomers = false);

    /// <summary>Retrieves a collection of customers associated with the specified store.</summary>
    /// <param name="storeId">The unique identifier of the store whose customers are to be retrieved.</param>
    /// <returns>A collection of customers associated with the specified store.</returns>
    [ UsedImplicitly ]
    ICollection<Customer> GetCustomers(int storeId);

    /// <summary>Adds a new customer to the repository.</summary>
    /// <param name="customer">The customer object containing the details of the customer to be added.</param>
    /// <returns>The newly added customer object.</returns>
    Customer AddCustomer(Customer customer);
}
