namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Secupay_Sofort_Transaction
    {
        public SecupaySofort Run(SecucardConnect client, Customer customer)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay sofort transaction sample ### " + Environment.NewLine);
            
            var sofort = new SecupaySofort();
            sofort.Demo = true;
            sofort.Amount = 100; // Amount in cents (or in the smallest unit of the given currency)
            sofort.Currency = "EUR"; // The ISO-4217 code of the currency
            sofort.Purpose = "Your purpose from TestShopName";
            sofort.OrderId = "201600123"; // The shop order id
            sofort.Customer = customer;

            // if you want to create sofort payment for a cloned contract (contract that you created by cloning main contract)
            //var contract = new Contract();
            //contract.Id = "PCR_XXXX";
            //contract.Object = "payment.contracts";
            //sofort.Contract = contract;
            

            try
            {
                sofort = client.Payment.Secupaysofort.Create(sofort);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (!string.IsNullOrEmpty(sofort.Id))
            {
                Console.WriteLine($"Created secupay sofort transaction with id: {sofort.Id}");
                Debug.WriteLine($"Sofort data: {sofort.ToString()}");
                Console.WriteLine($"CHECKOUT URL: {sofort.RedirectUrl.UrlIframe}");
            }
            else
            {
                Console.WriteLine("Sofort creation failed");
            }

            // To cancel the transaction you would use:
            // service.Cancel(sofort.Id);

            return sofort;
        }
    }
}
