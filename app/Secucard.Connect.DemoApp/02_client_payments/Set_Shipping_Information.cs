namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Set_Shipping_Information
    {
        public void Run(SecucardConnect client, SecupayDebit debit)
        {
            Console.WriteLine(Environment.NewLine + "### Set shipping information sample ### " + Environment.NewLine);

            var service = client.Payment.Secupaydebits;

            try
            {
                var data = new ShippingInformation();

                // Set the invoice number (optional, but recommended)
                data.InvoiceNumber = "201800218";

                // Set the tracking information of the parcel (optional)
                data.TrackingId = "00340433836442636597";
                data.Carrier = "DHL";

                var res = service.SetShippingInformation(debit.Id, data);

                if (res)
                {
                    Console.WriteLine($"Set shipping information for transaction with id: {debit.Id}");
                }
                else
                {
                    Console.WriteLine($"Set shipping information failed with id: {debit.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set shipping information failed");
                Debug.WriteLine($"Error message: {ex.Message}");
            }
        }
    }
}
