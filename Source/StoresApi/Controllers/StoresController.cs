using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

using StoresApi.Models;
using StoresApi.Services;

namespace StoresApi.Controllers;

/// <summary>
///     Controller responsible for handling store-related operations within the API. Provides endpoints for retrieving store information, retrieving specific store details, and
///     creating new customers associated with stores.
/// </summary>
[ ApiController ]
[ Route(template: "api/[controller]") ]
public sealed class StoresController : ControllerBase
{
    private const string CountryCodeHeaderName = "x-test-country-code";

    private readonly IRepository _repository;

    public StoresController(IRepository repository) => _repository = repository;

    [ HttpGet ]
    public IActionResult GetStores()
    {
        string? countryCode = GetCountryCodeHeader();

        if (countryCode is null)
        {
            return Unauthorized();
        }

        ICollection<Store> stores = _repository.GetStores(s => s.CountryCode == countryCode);

        return Ok(stores);
    }

    /// <summary>Retrieves store details based on the specified store ID. Optionally includes associated customer data.</summary>
    /// <param name="storeId">The unique identifier of the store to retrieve.</param>
    /// <param name="includeCustomers">A boolean value indicating whether to include associated customer data. Default is false.</param>
    /// <returns>An IActionResult containing the store details if found and accessible, or an appropriate HTTP status code if the store is not found, forbidden, or unauthorised.</returns>
    [ HttpGet(template: "{storeId:int}") ]
    public IActionResult GetStore(int storeId, [ FromQuery ] bool includeCustomers = false)
    {
        string? countryCode = GetCountryCodeHeader();

        if (countryCode is null)
        {
            return Unauthorized();
        }

        ICollection<Store> stores = _repository.GetStores(s => s.StoreId == storeId, includeCustomers);

        Store? store = stores.FirstOrDefault();

        if (store is null)
        {
            return NotFound();
        }

        if (store.CountryCode != countryCode)
        {
            return Forbid();
        }

        return Ok(store);
    }

    /// <summary>Creates a new customer and associates it with a store.</summary>
    /// <param name="customer">The customer object containing the details of the customer to be created. Must include a valid email and store association details.</param>
    /// <remarks>
    ///     The <see cref="Customer.Email" /> property is annotated with the <see cref="EmailAddressAttribute" />, however, Codility's test context for this task was not set up to
    ///     validate email addresses using this annotation; the test environment is missing the MVC infrastructure and controller context. Additionally, the annotation itself does not
    ///     enforce format validation. It simply denotes the format type. To get around this, I am using Microsoft's <c>MailAddress</c> class to parse the email. It will throw on
    ///     malformed addresses like "test9999@example.com@incorrect@mail", and the additional check "<c>addr.Address == customer.Email</c>" catches cases where the <c>MailAddress</c>
    ///     silently normalises the email into a different valid form.
    /// </remarks>
    /// <returns>
    ///     An IActionResult containing the newly created customer object if successful, or an appropriate HTTP status code if validation fails, country code is missing, or the input
    ///     data is invalid.
    /// </returns>
    [ HttpPost ]
    public IActionResult CreateCustomer([ FromBody ] Customer? customer)
    {
        string? countryCode = GetCountryCodeHeader();

        if (countryCode is null)
        {
            return Unauthorized();
        }

        if (customer is null)
        {
            return BadRequest();
        }

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            try
            {
                MailAddress addr = new(customer.Email);

                if (addr.Address != customer.Email)
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        Customer result = _repository.AddCustomer(customer);

        return Ok(result);
    }

    private string? GetCountryCodeHeader()
    {
        if (!Request.Headers.TryGetValue(CountryCodeHeaderName, out StringValues values))
        {
            return null;
        }

        if (values.Count != 1)
        {
            return null;
        }

        string? value = values[index: 0];

        return string.IsNullOrWhiteSpace(value) ? null : value;
    }
}
