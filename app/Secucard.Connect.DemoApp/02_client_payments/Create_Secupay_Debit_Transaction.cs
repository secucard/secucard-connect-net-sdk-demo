namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;
    using Product.Payment.Model;
    using System.Diagnostics;

    public class Create_Secupay_Debit_Transaction
    {
        public void Run(SecucardConnect client, Customer customer, Container container)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay debit transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaydebits;

            var debit = new SecupayDebit();
            debit.Amount = 245; // Amount in cents (or in the smallest unit of the given currency)
            debit.Currency = "EUR"; // The ISO-4217 code of the currency
            debit.Purpose = "Your purpose from TestShopName";
            debit.OrderId = "201600123"; // The shop order id
            debit.Customer = customer;
            debit.Container = container;
            debit.Basket = new Basket[2]; // We want to store two items

            // if you want to create debit payment for a cloned contract (contract that you created by cloning main contract)
            // var contract = new Contract();
            //contract.Id = "PCR_XXXX";
            //contract.Object = "payment.contracts";
            //debit.Contract = contract;

            // Create basket

            // Add the first item
            var article = new Basket();
            article.ArticleNumber = "3211";
            article.Ean = "4123412341243";
            article.ItemType = Basket.ItemTypeArticle;
            article.Name = "Testname 1";
            article.PriceOne = 25;
            article.Quantity = 2;
            article.Tax = 19;
            article.Total = 50;
            debit.Basket[0] = article;

            // Add the shipping costs
            var shipping = new Basket();
            shipping.ItemType = Basket.ItemTypeShipping;
            shipping.Name = "Deutsche Post Warensendung";
            shipping.Tax = 19;
            shipping.Total = 145;
            debit.Basket[1] = shipping;

            try
            {
                debit = service.Create(debit);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (debit.Id != string.Empty)
            {
                Console.WriteLine($"Created secupay debit transaction with id: {debit.Id}");
                Console.WriteLine($"Debit data: {debit.ToString()}");
            }
            else
            {
                Console.WriteLine("Debit creation failed");
            }

            // To cancel the transaction you would use:
            // service.Cancel(debit.Id);
        }
    }
}
