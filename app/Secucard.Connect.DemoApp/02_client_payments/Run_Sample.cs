namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Auth;
    using Auth.Model;
    using Client.Config;
    using Storage;
    using System;

    public static class Run_Sample
    {
        public static void runAllSamples()
        {
            // Load default properties
            var properties = Properties.Load("SecucardConnect.config");

            // Perpare client config. Implement your own Auth Details
            var _clientConfiguration = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new PaymentAuthDetais(),
                DataStorage = new MemoryDataStorage()
            };

            // Create the client, set credentials and open the connection
            var paymentClient = SecucardConnect.Create(_clientConfiguration);
            paymentClient.Open();

            // Create a new customer
            var customer = new Create_Customer().Run(paymentClient);

            // Create a new payment container (only needed for secupay debit, not for prepay)
            var container = new Create_Container().Run(paymentClient, customer);

            // Create a new payment transaction with secupay debit
            new Create_Secupay_Debit_Transaction().Run(paymentClient, customer, container);

            // Create a new payment transaction with secupay invoice
            new Create_Secupay_Invoice_Transaction().Run(paymentClient, customer);

            // Create a new payment transaction with secupay prepay
            var prepay = new Create_Secupay_Prepay_Transaction().Run(paymentClient, customer);

            // Cancel a created payment transaction (with secupay prepay)
            new Cancel_Secupay_Prepay_Transaction().Run(paymentClient, prepay);

            // Create a new payment transaction with secupay credit card
            var creditcardId = new Create_Secupay_Creditcard_Transaction().Run(paymentClient, customer);

            // Get the status of a created payment transaction (with credit card)
            new Get_Secupay_Creditcard_Transaction().Run(paymentClient, creditcardId);

            // List all created customers
            new Get_Customer_List().Run(paymentClient);

            // List all created payment containers
            new Get_Container_List().Run(paymentClient);

            // Close the client
            paymentClient.Close();

            Console.WriteLine(Environment.NewLine + "Samples done");
            Console.Read();
        }

        /// <summary>
        /// You have to adjust the clientid and clientsecret
        /// </summary>
        private class PaymentAuthDetais : AbstractClientAuthDetails, IClientAuthDetails
        {
            public OAuthCredentials GetCredentials()
            {
                return new ClientCredentials(
                    "09ae83af7c37121b2de929b211bad944",
                    "9c5f250b69f6436cb38fd780349bc00810d8d5051d3dcf821e428f65a32724bd");
            }

            public ClientCredentials GetClientCredentials()
            {
                return (ClientCredentials)GetCredentials();
            }
        }
    }
}
