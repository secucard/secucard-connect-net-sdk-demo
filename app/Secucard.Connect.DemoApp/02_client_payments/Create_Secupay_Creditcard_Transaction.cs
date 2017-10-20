namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Secupay_Creditcard_Transaction
    {
        public string Run(SecucardConnect client, Customer customer)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay creditcard transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaycreditcards;

            var creditcard = new SecupayCreditcard();
            creditcard.Amount = 100; // Amount in cents (or in the smallest unit of the given currency)
            creditcard.Currency = "EUR"; // The ISO-4217 code of the currency
            creditcard.Purpose = "Your purpose from TestShopName";
            creditcard.OrderId = "201600123"; // The shop order id
            creditcard.Customer = customer;

            try
            {
                creditcard = service.Create(creditcard);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (creditcard.Id != string.Empty)
            {
                Console.WriteLine($"Created secupay creditcard transaction with id: {creditcard.Id}");
                Console.WriteLine($"Creditcard data: {creditcard.ToString()}");
            }
            else
            {
                Console.WriteLine("Creditcard creation failed");
            }

            return creditcard.Id;

            // To cancel the transaction you would use:
            // service.Cancel(creditcard.Id);
        }
    }
}
