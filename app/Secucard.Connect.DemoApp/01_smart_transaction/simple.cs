namespace Secucard.Connect.DemoApp._01_smart_transaction
{
    using Auth;
    using Auth.Model;
    using Client;
    using Client.Config;
    using Product.Smart;
    using Product.Smart.Event;
    using Product.Smart.Model;
    using Storage;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    public static class simple
    {
        public static void Run()
        {
            // Load default properties
            var properties = Properties.Load("SecucardConnect.config");

            // Perpare client config. Implement your own Auth Details
            var _clientConfiguration = new ClientConfiguration(properties)
            {
                ClientAuthDetails = new DeviceAuthDetails(),
                DataStorage = new MemoryDataStorage()
            };

            // Create client and attach client event handlers
            var smartClient = SecucardConnect.Create(_clientConfiguration);
            smartClient.AuthEvent += ClientOnAuthEvent;
            smartClient.ConnectionStateChangedEvent += ClientOnConnectionStateChangedEvent;
            smartClient.Open();

            // register at smart.checkin events (incoming customers)
            var checkinService = smartClient.Smart.Checkins;
            checkinService.CheckinEvent += CheckinEvent;

            // get reference to transaction and ident service
            var transactionService = smartClient.Smart.Transactions;
            var identService = smartClient.Smart.Idents;

            // register eventhandler für transaction service --> progress during transaction
            transactionService.TransactionCashierEvent += SmartTransactionCashierEvent;

            // select an ident
            var availableIdents = identService.GetList(null);
            if (availableIdents == null || availableIdents.Count == 0)
            {
                throw new Exception("No idents found.");
            }
            var ident = availableIdents.List.First(o => o.Id == "smi_1");
            ident.Value = "pdo28hdal";

            var selectedIdents = new List<Ident> { ident };

            // prepare basket with items locally
            var groups = new List<ProductGroup>
            {
                new ProductGroup {Id = "group1", Desc = "beverages", Level = 1}
            };

            // Add items to the basket
            var basket = new Basket();
            basket.AddProduct(new Product
            {
                Id = 1,
                ArticleNumber = "3378",
                Ean = "5060215249804",
                Desc = "desc1",
                Quantity = 5m,
                PriceOne = 1999,
                Tax = 7,
                Groups = groups
            });
            basket.AddProduct(new Product
            {
                Id = 2,
                ArticleNumber = "art2",
                Ean = "5060215249805",
                Desc = "desc2",
                Quantity = 1m,
                PriceOne = 999,
                Tax = 19,
                Groups = groups
            });
            basket.AddProduct(new Text { Id = 1, ParentId = 2, Desc = "text1" });
            basket.AddProduct(new Text { Id = 2, ParentId = 2, Desc = "text2" });

            var basketInfo = new BasketInfo { Sum = 1, Currency = "EUR" };

            // build transaction object
            var newTrans = new Transaction
            {
                BasketInfo = basketInfo,
                Basket = basket,
                Idents = selectedIdents,
                MerchantRef = "merchant21",
                TransactionRef = "transaction99"
            };

            // create transaction on server
            var transaction = transactionService.Create(newTrans);

            // you may edit some transaction data and update
            //newTrans.MerchantRef = "merchant";
            //transaction.TransactionRef = "trans1";
            //transaction = transactionService.Update(transaction);

            // All possible transaction types are defined in TransactionsService
            // demo instructs the server to simulate a different (random) transaction for each invocation of startTransaction
            var type = TransactionsService.TYPE_DEMO;

            // start transaction (this takes some time, consider another thread) 
            var result = transactionService.Start(transaction.Id, type);

            Console.WriteLine("transaction start: " + result.ToString());

            // cancel loyalty transaction
            var cancelResult = transactionService.Cancel(transaction.Id);

            Console.WriteLine("transaction cancel: " + cancelResult.ToString());

            // cancel cash transaction
            //var cancelCash = transactionService.CancelPayment(transaction.ReceiptNumber);

            smartClient.Close();
        }

        /// <summary>
        ///     Handles device authentication. Enter pin thru web interface service
        /// </summary>
        private static void ClientOnAuthEvent(object sender, AuthEventArgs args)
        {
            if (args.Status == AuthStatusEnum.Pending)
            {
                // Device code retrieved successfully - present this data to user
                // User must visit an URL and enter some codes
                Console.WriteLine($"Please visit {args.DeviceAuthCodes.VerificationUrl} and enter code: {args.DeviceAuthCodes.UserCode}");
            }
            else if (args.Status == AuthStatusEnum.Ok)
            {
                // Present to the user - user has device codes typed in and the auth was successful
                Console.WriteLine("Gratulations, you are now authenticated!");
            }
        }

        /// <summary>
        /// Handles connect and disconnect events
        /// </summary>
        private static void ClientOnConnectionStateChangedEvent(object sender, ConnectionStateChangedEventArgs args)
        {
            Console.WriteLine("Client Connected={0}", args.Connected);
        }

        /// <summary>
        /// Handles events during transaction
        /// </summary>
        private static void SmartTransactionCashierEvent(object sender, TransactionCashierEventArgs args)
        {
            Console.WriteLine(args.SecucardEvent.Data.Text);
        }

        /// <summary>
        /// Gets called on new checkins
        /// </summary>
        private static void CheckinEvent(object sender, CheckinEventEventArgs args)
        {
            Console.WriteLine(args.SecucardEvent.Data.CustomerName);
        }

        /// <summary>
        /// You have to adjust the clientid, clientsecret and deviceid
        /// </summary>
        private class DeviceAuthDetails : AbstractClientAuthDetails, IClientAuthDetails
        {
            public OAuthCredentials GetCredentials()
            {
                return new DeviceCredentials("611c00ec6b2be6c77c2338774f50040b",
                    "dc1f422dde755f0b1c4ac04e7efbd6c4c78870691fe783266d7d6c89439925eb",
                    "/vendor/unknown/cashier/dotnettest1");
            }

            public ClientCredentials GetClientCredentials()
            {
                return (ClientCredentials)GetCredentials();
            }

            public new Token GetCurrent()
            {
                return (Token)MemoryDataStorage.LoadFromFile(Path.GetTempPath() + "token.storage").Get("token");
            }

            public new void OnTokenChanged(Token token)
            {
                _storage.Save("token", token);
                (_storage as MemoryDataStorage).SaveToFile(Path.GetTempPath() + "token.storage");
            }
        }
    }
}
