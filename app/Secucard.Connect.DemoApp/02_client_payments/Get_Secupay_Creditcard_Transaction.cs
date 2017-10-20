namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;

    public class Get_Secupay_Creditcard_Transaction
    {
        public void Run(SecucardConnect client, string creditcardId)
        {
            Console.WriteLine(Environment.NewLine + "### Get secupay creditcard transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaycreditcards;

            // You may obtain a global list of available containers
            var creditcard = service.Get(creditcardId);

            if (creditcard == null)
            {
                throw new Exception("No creditcard transaction found.");
            }

            Console.WriteLine(creditcard.ToString());
        }
    }
}
