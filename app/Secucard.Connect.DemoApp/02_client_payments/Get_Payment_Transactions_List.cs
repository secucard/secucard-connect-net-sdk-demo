namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;

    public class Get_Payment_Transactions_List
    {
        public void Run(SecucardConnect client)
        {
            Console.WriteLine(Environment.NewLine + "### Get Payment_Transactions list sample ### " + Environment.NewLine);

            var service = client.Payment.PaymentTransactions;

            // Create a filter to reduce the result amount
            var filter = new Product.Common.Model.QueryParams();
            filter.Count = 1;
            filter.SortOrder = new System.Collections.Generic.Dictionary<string, string>();
            filter.SortOrder.Add("trans_id", "desc");
            filter.Query = "product:Vorkasse";

            var transactions = service.GetList(filter);

            if (transactions == null)
            {
                throw new Exception("No Payment_Transactions found.");
            }

            Console.WriteLine(transactions.ToString());
            Console.WriteLine(transactions.List[0].ToString());


            // Get the details via "Secupayprepays"
            var service2 = client.Payment.Secupayprepays;
            var transaction = service2.Get(transactions.List[0].TransactionHash);
            if (transaction == null)
            {
                throw new Exception("No transaction found.");
            }
            Console.WriteLine(transaction.ToString());


            // Get the details via "PCI"
            var transaction2 = service.Get(transactions.List[0].Id);
            if (transaction2 == null)
            {
                throw new Exception("No transaction found.");
            }
            Console.WriteLine(transaction2.ToString());


            // Get the details via "GetOldFormat"
            var transaction3 = service.GetOldFormat(transactions.List[0].TransactionHash);
            if (transaction3 == null)
            {
                throw new Exception("No transaction found.");
            }
            Console.WriteLine(transaction3.ToString());
        }
    }
}
