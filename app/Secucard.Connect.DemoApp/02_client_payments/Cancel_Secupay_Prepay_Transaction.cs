namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Cancel_Secupay_Prepay_Transaction
    {
        public void Run(SecucardConnect client, SecupayPrepay prepay)
        {
            Console.WriteLine(Environment.NewLine + "### Cancel secupay prepay transaction sample ### " + Environment.NewLine);

            var service = client.Payment.Secupayprepays;

            try
            {
                // Cancel the transaction by id and get the response if it was successfull
                var res = service.Cancel(prepay.Id);

                if (res)
                {
                    Console.WriteLine($"Canceled secupay prepay transaction with id: {prepay.Id}");
                }
                else
                {
                    Console.WriteLine($"Prepay cancellation failed with id: {prepay.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Prepay cancellation failed");
                Debug.WriteLine($"Error message: {ex.Message}");
            }
        }
    }
}
