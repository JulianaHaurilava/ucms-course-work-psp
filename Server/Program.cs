using CMSLib.Model;
using System.Net;

namespace Server
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                context.Init();
                context.SaveChanges();
            }
            Server server = new Server(IPAddress.Any, 5050);
            await server.StartServerAsync();
        }
    }
}