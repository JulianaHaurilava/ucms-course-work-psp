using CMSLib.DTO;
using CMSLib.TCP;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace UCMSApp.Client
{
    public class Client
    {
        private TcpClient client;
        private string hostName;
        private int port;

        private StreamReader Reader = null;
        private StreamWriter Writer = null;

        private static Client instance;
        public static Client Instance
        {
            get
            {
                return instance;
            }
            set => instance = value;
        }

        public User CurrentUser { get; set; }

        public Client(string hostName, int port)
        {
            this.hostName = hostName;
            this.port = port;
            client = new TcpClient();
            instance = this;
        }

        public void Connect()
        {
            try
            {
                client.Connect(hostName, port);
                Reader = new StreamReader(client.GetStream());
                Writer = new StreamWriter(client.GetStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Response> GetResponseAsync()
        {
            string message = await Reader.ReadLineAsync();
            Response response = JsonConvert.DeserializeObject<Response>(message);
            return response;
        }

        public async Task SendRequestAsync(Request request)
        {
            string message = JsonConvert.SerializeObject(request);
            await Writer.WriteLineAsync(message);
            Writer.Flush();
        }
    }
}
