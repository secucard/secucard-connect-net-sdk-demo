using System.Web.Hosting;
using System.Web.Http;
using Secucard.Connect.Auth;
using Secucard.Connect.Auth.Model;
using Secucard.Connect.Client.Config;
using Secucard.Connect.Product.Payment.Event;
using Secucard.Connect.Product.Payment.Model;
using Secucard.Connect.Storage;

namespace Secucard.Connect.DemoWebApp.Controllers
{
    public class ValuesController : ApiController
    {
        private SecucardConnect client;

        // POST api/values
        [HttpPost]
        public IHttpActionResult Post(dynamic value)
        {
            InitSecucardConnectClient();

            // Register event listiener
            var service = client.Payment.Secupaycreditcards;
            service.PaymentEvent += PaymentEvent;

            // Handle event message
            client.HandleEvent(this, "" + value);

            // Return HTTP 200 OK -> To tell that this push notification was processed and the server can cease to send this notification (again).
            return Ok();
        }

        // GET api/values
        [HttpGet]
        public string Get()
        {
            InitSecucardConnectClient();

            // Create a new customer
            return new CreateCustomer().Run(client);
        }

        private SecucardConnect InitSecucardConnectClient()
        {
            if (client == null)
            {
                // Load default properties
                var properties = Secucard.Connect.Client.Config.Properties.Load(HostingEnvironment.MapPath(@"~/App_Data/SecucardConnect.config"));

                // Perpare client config. Implement your own Auth Details
                var _clientConfiguration = new ClientConfiguration(properties)
                {
                    ClientAuthDetails = new PaymentAuthDetails(),
                    DataStorage = new MemoryDataStorage()
                };

                // Create the client, set credentials and open the connection
                client = SecucardConnect.Create(_clientConfiguration);
            }

            return client;
        }

        /// <summary>
        /// Gets called on new checkins
        /// </summary>
        private static void PaymentEvent(object sender, PaymentEventEventArgs<SecupayCreditcard> args)
        {
            var service = (Secucard.Connect.Product.Payment.SecupayCreditcardsService)sender;
            var transaction = args.SecucardEvent.Data[0];
        }

        /// <summary>
        /// You have to adjust the clientid and clientsecret
        /// </summary>
        private class PaymentAuthDetails : AbstractClientAuthDetails, IClientAuthDetails
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