namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Secupay_Invoice_Transaction
    {
        public void Run(SecucardConnect client, Customer customer)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay invoice transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupayinvoices;

            var invoice = new SecupayInvoice();
            invoice.Amount = 100; // Amount in cents (or in the smallest unit of the given currency)
            invoice.Currency = "EUR"; // The ISO-4217 code of the currency
            invoice.Purpose = "Your purpose from TestShopName";
            invoice.OrderId = "201600123"; // The shop order id
            invoice.Customer = customer;

            try
            {
                invoice = service.Create(invoice);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (invoice.Id != string.Empty)
            {
                Console.WriteLine($"Created secupay invoice transaction with id: {invoice.Id}");
                Console.WriteLine($"Invoice data: {invoice.ToString()}");
            }
            else
            {
                Console.WriteLine("Invoice creation failed");
            }

            // To cancel the transaction you would use:
            // service.Cancel(invoice.Id);
        }
    }
}
