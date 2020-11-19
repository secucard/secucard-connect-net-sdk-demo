namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;
    using Product.Payment.Model;
    using System.IO;
    using Secucard.Connect.Product.Document.Model;

    public class Create_Secupay_Payout_Transaction
    {
        public SecupayPayout Run(SecucardConnect client, Customer customer, Container container)
        {
            Console.WriteLine(Environment.NewLine + "### Create secupay payout transaction sample ### " + Environment.NewLine);
            
            var service = client.Payment.Secupaypayout;
            var payout = new SecupayPayout();
            payout.Amount = 300; // Amount in cents (or in the smallest unit of the given currency)
            payout.Currency = "EUR"; // The ISO-4217 code of the currency
            payout.Purpose = "Payout Test #3";
            payout.OrderId = "12345"; // The shop order id
            payout.Customer = customer.Id;
            payout.RedirectUrl = new RedirectUrl {
            };
            payout.TransactionList = new TransactionList[3]; // We want to store two items

            // if you want to create payout payment for a cloned contract (contract that you created by cloning main contract),
            // otherwise it will be assinged to the plattform contract, add the follwing line:
            // payout.Contract = "PCR_XXXX";
            
            // Sample for create a payout based on a payment instrument of the completed investment
            var item1 = new TransactionList();
            item1.ItemType = TransactionList.ItemTypeTransactionPayout;
            item1.ReferenceId = "123.1";
            item1.Name = "Payout Purpose 1";
            item1.TransactionHash = "ckaidxxxfskc1176505"; // you can use TransactionHash or TransactionId, if you transmit both only TransactionId will be used.
            item1.Total = 100;
            payout.TransactionList[0] = item1;

            // Sample for create a payout based on a completed investment with a new bank account
            var item2 = new TransactionList();
            item2.ItemType = TransactionList.ItemTypeTransactionPayout;
            item2.ReferenceId = "123.2";
            item2.Name = "Payout Purpose 2";
            item2.TransactionId = "PCI_WR67G325XTG2R45JJDNBG048PW4BN4"; // you can use TransactionHash or TransactionId, if you transmit both only TransactionId will be used.
            item2.ContainerId = container.Id; // f.e. "PCT_E4Z8U4W4J2N7MFV270ZAVWZFNJYHA3";
            item2.Total = 100;
            payout.TransactionList[1] = item2;

            // Sample for create a payout with a new bank account (and investor / payment customer)
            var item3 = new TransactionList();
            item3.ItemType = TransactionList.ItemTypeTransactionPayout;
            item3.ReferenceId = "123.3";
            item3.Name = "Payout Purpose 3";
            item3.ContainerId = container.Id; // f.e. "PCT_E4Z8U4W4J2N7MFV270ZAVWZFNJYHA3";
            item3.Total = 100;
            payout.TransactionList[2] = item3;

            try
            {
                payout = service.Create(payout);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error message: {ex.Message}");
            }

            if (!string.IsNullOrEmpty(payout.Id))
            {
                Console.WriteLine($"Created secupay payout transaction with id: {payout.Id}");
                Console.WriteLine($"Payout data: {payout.ToString()}");
            }
            else
            {
                Console.WriteLine("Payout creation failed");
            }

            return null;
        }
    }
}
