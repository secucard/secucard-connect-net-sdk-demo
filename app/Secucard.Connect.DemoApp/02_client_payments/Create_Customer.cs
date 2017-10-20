namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.General.Model;
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Customer
    {
        public Customer Run(SecucardConnect client)
        {
            Console.WriteLine(Environment.NewLine + "### Create customer sample ### " + Environment.NewLine);

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
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (customer.Id != string.Empty)
            {
                Console.WriteLine($"Created Customer with id: {customer.Id}");
                Console.WriteLine($"Customer data: {customer.ToString()}");
            }
            else
            {
                Console.WriteLine("Customer creation failed");
            }

            return customer;
        }
    }
}
