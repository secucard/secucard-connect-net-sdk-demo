namespace Secucard.Connect.DemoApp._02_client_payments
{
    using Product.Payment.Model;
    using System;
    using System.Diagnostics;

    public class Create_Container
    {
        public Container Run(SecucardConnect client, Customer customer)
        {
            Console.WriteLine(Environment.NewLine + "### Create container sample ### " + Environment.NewLine);

            var service = client.Payment.Containers;

            // You may obtain a global list of available containers
            var containers = service.GetList(null);
            if (containers == null)
            {
                throw new Exception("No Containers found.");
            }

            // new container creation:

            // create new Data subobject for contrainer
            var container_data = new Data();
            container_data.Iban = "DE62100208900001317270";
            container_data.Owner = "Max Mustermann";

            // the customer reference for the container is optional, but we strongly recommend it
            //customer = new Customer();
            //customer.Object = "payment.customers";
            //customer.Id = "@your-already-created-customer-id";

            var container = new Container();
            container.PrivateData = container_data;
            container.Customer = customer;
            Console.WriteLine("object data initialized");

            try
            {
                container = service.Create(container);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error message: {ex.Message}");
            }

            if (container.Id != string.Empty)
            {
                Console.WriteLine($"Created Container with id: {container.Id}");
                Console.WriteLine($"Container data: {container.ToString()}");
            }
            else
            {
                Console.WriteLine("Container creation failed");
            }

            return container;
        }
    }
}
