namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.General.Model;
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Sub_Contract
    {
        public CreateSubContractResponse Run(SecucardConnect client)
        {
            Console.WriteLine(Environment.NewLine + "### Create Sub-Contract sample ### " + Environment.NewLine);

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
            // specifying email for merchant is important, so the merchant can receive Mandate information
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

            var request = new CreateSubContractRequest();
            request.Contact = contact;
            request.Project = "Test " + DateTime.Now;
            request.PayoutAccount = new Data
            {
                Iban = "DE37503240001000000524",
                Bic = "FTSBDEFAXXX",
                Owner = "Test #1"
            };
            request.IframeOpts = new IframeOptData
            {
                ShowBasket = true,
                BasketTitle = "Projext XY unterstützen",
                SubmitButtonTitle = "Zahlungspflichtig unterstützen"
            };
            request.PayinAccount = false;


            CreateSubContractResponse merchant;
            try
            {
                // Create a new merchant and get the object back
                merchant = client.Payment.Contracts.CreateSubContract(request);

                if (!string.IsNullOrEmpty(merchant.Apikey))
                {
                    Console.WriteLine($"Created Sub-Contract with id: {merchant.Id}");
                    Console.WriteLine($"Sub-Contract data: {merchant.ToString()}");
                }
                else
                {
                    Console.WriteLine("Sub-Contract creation failed");
                }

                return merchant;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            return null;
        }
    }
}
