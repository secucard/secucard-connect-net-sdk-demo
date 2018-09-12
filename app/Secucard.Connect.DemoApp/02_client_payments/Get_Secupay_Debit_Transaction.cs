namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Secucard.Connect.Product.Payment.Model;
    using System;

    public class Get_Secupay_Debit_Transaction
    {
        public SecupayDebit Run(SecucardConnect client, string debitId)
        {
            Console.WriteLine(Environment.NewLine + "### Get secupay debit transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaydebits;

            // You may obtain a global list of available containers
            var debit = service.Get(debitId);

            if (debit == null)
            {
                throw new Exception("No debit transaction found.");
            }

            Console.WriteLine(debit.ToString());

            return debit;
        }
    }
}
