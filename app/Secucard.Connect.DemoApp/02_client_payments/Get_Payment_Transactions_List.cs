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
            //filter.Query = "trans_id=6781920";

            var transactions = service.GetList(filter);

            if (transactions == null)
            {
                throw new Exception("No Payment_Transactions found.");
            }

            Console.WriteLine(transactions.ToString());
            Console.WriteLine(transactions.List[0].ToString());


            // Get the details
            Console.WriteLine(transactions.List[0].Id);

            var service2 = client.Payment.Secupayprepays;
            var transaction = service2.Get(transactions.List[0].Id);

            if (transaction == null)
            {
                throw new Exception("No transaction found.");
            }

            Console.WriteLine(transaction.ToString());
        }
    }
}
