using System.Net;
using System.Net.Sockets;

namespace Server
{
	public class Server
	{
		private TcpListener tcpListener;
		private List<ClientHandler> clients;

		public Server(IPAddress address, int port)
		{
			tcpListener = new(address, port);
			clients = new List<ClientHandler>();
		}

		public async Task StartServerAsync()
		{
			try
			{
				tcpListener.Start();
				Console.WriteLine("Сервер запущен...");

				while (true)
				{
					var client = await tcpListener.AcceptTcpClientAsync();
					var clientHandler = new ClientHandler(this, client);
					clients.Add(clientHandler);
					Task.Run(clientHandler.ProcessAsync);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				tcpListener.Stop();
			}
		}

		public void RemoveClient(ClientHandler clientHandler)
		{
			clients.Remove(clientHandler);
		}
	}
}
