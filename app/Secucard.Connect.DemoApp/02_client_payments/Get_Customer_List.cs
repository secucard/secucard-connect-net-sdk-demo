namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;

    public class Get_Customer_List
    {
        public void Run(SecucardConnect client)
        {
            Console.WriteLine(Environment.NewLine + "### Get customer list sample ### " + Environment.NewLine);

            var service = client.Payment.Customers;

            // Create a filter to reduce the result amount
            var filter = new Product.Common.Model.QueryParams();
            filter.Count = 5;

            // You may obtain a global list of available customers, filtered by you
            var customers = service.GetList(filter);

            if (customers == null)
            {
                throw new Exception("No Customers found.");
            }

            Console.WriteLine(customers.ToString());
        }
    }
}
