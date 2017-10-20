using System;
using Secucard.Connect;
using Secucard.Connect.Product.General.Model;
using Secucard.Connect.Product.Payment.Model;

public class CreateCustomer
{
    public string Run(SecucardConnect client)
    {

        var service = client.Payment.Customers;

        // Create a new contact and fill the needed data
        var contact = new Contact();

        contact.Salutation = "Mr.";
        contact.Title = "Dr.";
        contact.Forename = "John";
        contact.Surname = "Doe";
        contact.CompanyName = "Testfirma";
        contact.DateOfBirth = new DateTime(1971, 2, 3);
        contact.BirthPlace = "MyBirthplace";
        contact.Nationality = "DE";
        // specifying email for customer is important, so the customer can receive Mandate information
        contact.Email = "example@example.com";
        contact.Phone = "0049-123456789";

        var address = new Address();
        address.Street = "Example Street";
        address.StreetNumber = "6a";
        address.City = "ExampleCity";
        // For country use ISO 3166-2 code
        address.Country = "DE";
        address.PostalCode = "01234";

        contact.Address = address;

        var customer = new Customer();
        customer.Contact = contact;

        try
        {
            // Create a new customer and get the object back
            customer = service.Create(customer);
        }
        catch (Exception ex)
        {
            return $"Error message: {ex.Message}" + Environment.NewLine;
        }

        if (customer.Id != string.Empty)
        {
            return $"Created Customer with id: {customer.Id}" + Environment.NewLine +
                $"Customer data: {customer.ToString()}" + Environment.NewLine;
        }
        else
        {
            return "Customer creation failed" + Environment.NewLine;
        }
    }
}