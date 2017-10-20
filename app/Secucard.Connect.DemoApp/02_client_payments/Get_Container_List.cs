namespace Secucard.Connect.DemoApp._02_client_payments
{
    using System;

    public class Get_Container_List
    {
        public void Run(SecucardConnect client)
        {
            Console.WriteLine(Environment.NewLine + "### Get container list sample ### " + Environment.NewLine);

            var service = client.Payment.Containers;

            // You may obtain a global list of available containers
            var containers = service.GetList(null);

            if (containers == null)
            {
                throw new Exception("No Containers found.");
            }

            Console.WriteLine(containers.ToString());
        }
    }
}
