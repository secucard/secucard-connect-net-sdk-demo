namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Capture_Debit_Transaction
    {
        public void Run(SecucardConnect client, SecupayDebit debit)
        {
            Console.WriteLine(Environment.NewLine + "### Capture debit transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaydebits;

            try
            {
                var res = service.Capture(debit.Id);

                if (res)
                {
                    Console.WriteLine($"Capture debit transaction with id: {debit.Id}");
                }
                else
                {
                    Console.WriteLine($"Capture debit transaction failed with id: {debit.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Capture debit transaction failed");
                Debug.WriteLine($"Error message: {ex.Message}");
            }
        }
    }
}
