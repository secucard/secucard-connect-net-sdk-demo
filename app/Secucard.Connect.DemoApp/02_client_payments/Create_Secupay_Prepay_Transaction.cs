namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Secupay_Prepay_Transaction
    {
        public SecupayPrepay Run(SecucardConnect client, Customer customer)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay prepay transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupayprepays;

            var prepay = new SecupayPrepay();
            prepay.Amount = 100; // Amount in cents (or in the smallest unit of the given currency)
            prepay.Currency = "EUR"; // The ISO-4217 code of the currency
            prepay.Purpose = "Your purpose from TestShopName";
            prepay.OrderId = "201600123"; // The shop order id
            prepay.Customer = customer;

            // if you want to create prepay payment for a cloned contract (contract that you created by cloning main contract)
            //var contract = new Contract();
            //contract.Id = "PCR_XXXX";
            //contract.Object = "payment.contracts";
            //prepay.Contract = contract;
            

            try
            {
                prepay = service.Create(prepay);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (prepay.Id != string.Empty)
            {
                Console.WriteLine($"Created secupay prepay transaction with id: {prepay.Id}");
                Console.WriteLine($"Prepay data: {prepay.ToString()}");
            }
            else
            {
                Console.WriteLine("Prepay creation failed");
            }

            // To cancel the transaction you would use:
            // service.Cancel(prepay.Id);

            return prepay;
        }
    }
}
