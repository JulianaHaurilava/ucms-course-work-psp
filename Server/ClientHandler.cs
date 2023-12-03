using Newtonsoft.Json;
using Server.Services;
using System.Net.Sockets;
using CMSLib.DTO;
using CMSLib.Enum;
using CMSLib.TCP;

namespace Server
{
	public class ClientHandler
	{
		private Server server;
		private TcpClient client;

		private StreamReader reader;
		private StreamWriter writer;

		private UserService userService = new();
		private CompanyService companyService = new();
		private SiteService siteService = new();
		private CategoryService categoryService = new();
		private ItemService itemService = new();

		public ClientHandler(Server server, TcpClient client)
		{
			this.server = server;
			this.client = client;

			var stream = client.GetStream();
			reader = new StreamReader(stream);
			writer = new StreamWriter(stream);
		}

		public async Task ProcessAsync()
		{
			Console.WriteLine($"Клиент: {client.Client.RemoteEndPoint} подключился");
			try
			{
				while (true)
				{
					var request = await GetRequestAsync();
					switch (request.Type)
					{
						case RequestTypes.Login:
							Login(request.Message);
							break;

						case RequestTypes.GetCompanies:
							GetAllCompanies();
							break;
						case RequestTypes.UpsertCompany:
							UpsertCompany(request.Message);
							break;
						case RequestTypes.DeleteCompany:
							DeleteCompany(request.Message);
							break;

						//case RequestTypes.GetUsers:
						//	GetAllUsers();
						//	break;
						case RequestTypes.UpsertUser:
							UpsertUser(request.Message);
							break;
						//case RequestTypes.DeleteUser:
						//	DeleteUser(request.Message);
						//	break;

						case RequestTypes.GetSites:
                            GetAllSites();
							break;
						case RequestTypes.UpsertSite:
                            UpsertSite(request.Message);
							break;
						case RequestTypes.DeleteSite:
                            DeleteSite(request.Message);
							break;

						case RequestTypes.GetCategories:
							GetAllCategories();
							break;
						case RequestTypes.UpsertCategory:
                            UpsertCategory(request.Message);
							break;
						case RequestTypes.DeleteCategory:
                            DeleteCategory(request.Message);
							break;

						case RequestTypes.GetItems:
                            GetAllItems();
							break;
						case RequestTypes.UpsertItem:
                            UpsertItem(request.Message);
							break;
						case RequestTypes.DeleteItem:
							DeleteItem(request.Message);
							break;

						//case RequestTypes.GetTickets:
						//	GetAllTickets();
						//	break;
						//case RequestTypes.UpsertTicket:
						//	UpsertTicket(request.Message);
						//	break;
						//case RequestTypes.DeleteTicket:
						//	DeleteTicket(request.Message);
						//	break;
						case RequestTypes.GenerateSite:
							GenerateSite(request.Message);
							break;

						default:
							Console.WriteLine("Unknown request type");
							break;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				await Console.Out.WriteLineAsync($"Client {client.Client.RemoteEndPoint} is Disconnected");
				server.RemoveClient(this);
				Close();
			}
		}

		private async Task<Request> GetRequestAsync()
		{
			var message = await reader.ReadLineAsync();
			Console.WriteLine($"Клиент {client.Client.RemoteEndPoint} отправил: " + message);
			var request = JsonConvert.DeserializeObject<Request>(message);
			return request;
		}

		private async void SendResponseAsync(Response response)
		{
			string message = JsonConvert.SerializeObject(response);
			await writer.WriteLineAsync(message);
			writer.Flush();
		}

		private void Login(string requestMessage)
		{
			var requestUser = JsonConvert.DeserializeObject<User>(requestMessage);

			var users = userService.GetAll();
			var user = users.Find(u => u.Email.Equals(requestUser.Email, StringComparison.OrdinalIgnoreCase));
			Response response;
			if (user != null)
			{
				if (user.Password.Equals(requestUser.Password))
				{
					response = new Response(ResponseTypes.Ok, "Авторизация подтверждена", JsonConvert.SerializeObject(user));
				}
				else
				{
					response = new Response(ResponseTypes.NotOk, "Неверный пароль");
				}
			}
			else
			{
				response = new Response(ResponseTypes.NotOk, "Такого пользователя не существует");
			}
			SendResponseAsync(response);
		}

		private void GetAllCompanies()
		{
			var companies = companyService.GetAll();
			string data = JsonConvert.SerializeObject(companies);
			Response response = new Response(ResponseTypes.Ok, "", data);
			SendResponseAsync(response);
		}

		private void UpsertCompany(string requestMessage)
		{
			var requestCompany = JsonConvert.DeserializeObject<Company>(requestMessage);

			companyService.Upsert(requestCompany);
			Response response = new Response(ResponseTypes.Ok, "Компания успешно добавлена");
			SendResponseAsync(response);
		}

		private void DeleteCompany(string requestMessage)
		{
			var company = companyService.Get(int.Parse(requestMessage));

            companyService.Remove(company);
			Response response = new Response(ResponseTypes.Ok, "Компания успешно удалена");
			SendResponseAsync(response);
		}

		//private void GetAllUsers()
		//{
		//	var users = userService.GetAll();
		//	string data = JsonConvert.SerializeObject(users);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		private void UpsertUser(string requestMessage)
		{
			var requestUser = JsonConvert.DeserializeObject<User>(requestMessage);

			var users = userService.GetAll();
			var user = users.Find(u => u.Email.Equals(requestUser.Email, StringComparison.OrdinalIgnoreCase));
			Response response;
			if (user != null)
			{
				if (user.Id != requestUser.Id)
				{
					response = new Response(ResponseTypes.NotOk, "Такой пользователь уже существует");
				}
				else
				{
					userService.Upsert(requestUser);
					response = new Response(ResponseTypes.Ok, "Успешно");
				}
			}
			else
			{
				userService.Upsert(requestUser);
				response = new Response(ResponseTypes.Ok, "Успешно");
			}
			SendResponseAsync(response);
		}

		//private void DeleteUser(string requestMessage)
		//{
		//	var user = userService.Get(int.Parse(requestMessage));

		//	userService.Remove(user);
		//	Response response = new Response(ResponseTypes.Ok, "Пользователь успешно удален");
		//	SendResponseAsync(response);
		//}

		private void GetAllSites()
		{
			var sites = siteService.GetAll();
			string data = JsonConvert.SerializeObject(sites);
			Response response = new Response(ResponseTypes.Ok, "", data);
			SendResponseAsync(response);
		}
		private void UpsertSite(string requestMessage)
		{
			var requestSite = JsonConvert.DeserializeObject<Site>(requestMessage);

			siteService.Upsert(requestSite);
			Response response = new Response(ResponseTypes.Ok, "Успешно");

			SendResponseAsync(response);
		}

		private void DeleteSite(string requestMessage)
		{
			var cinema = siteService.Get(int.Parse(requestMessage));

			siteService.Remove(cinema);
			Response response = new Response(ResponseTypes.Ok, "Сайт успешно удален");
			SendResponseAsync(response);
		}

        private void GenerateSite(string requestMessage)
        {
            var siteToGenerate = JsonConvert.DeserializeObject<Site>(requestMessage);
			siteService.GenerateSite(siteToGenerate);

            Response response = new Response(ResponseTypes.Ok, "Сайт успешно сгенерирован!");
            SendResponseAsync(response);
        }


		private void GetAllCategories()
		{
			var categories = categoryService.GetAll();
			string data = JsonConvert.SerializeObject(categories);
			Response response = new Response(ResponseTypes.Ok, "", data);
			SendResponseAsync(response);
		}

		private void UpsertCategory(string requestMessage)
		{
			var requestCategory = JsonConvert.DeserializeObject<Category>(requestMessage);

			categoryService.Upsert(requestCategory);
			Response response = new Response(ResponseTypes.Ok, "Успешно");

			SendResponseAsync(response);
		}

		private void DeleteCategory(string requestMessage)
		{
			var category = categoryService.Get(int.Parse(requestMessage));

            categoryService.Remove(category);
			Response response = new Response(ResponseTypes.Ok, "Зал успешно удален");
			SendResponseAsync(response);
		}

		private void GetAllItems()
		{
			var items = itemService.GetAll();
			string data = JsonConvert.SerializeObject(items);
			Response response = new Response(ResponseTypes.Ok, "", data);
			SendResponseAsync(response);
		}

		private void UpsertItem(string requestMessage)
		{
			var requestItem = JsonConvert.DeserializeObject<Item>(requestMessage);

            itemService.Upsert(requestItem);
			Response response = new Response(ResponseTypes.Ok, "Успешно");

			SendResponseAsync(response);
		}

		private void DeleteItem(string requestMessage)
		{
			var item = itemService.Get(int.Parse(requestMessage));

            itemService.Remove(item);
			Response response = new Response(ResponseTypes.Ok, "Сеанс успешно удален");
			SendResponseAsync(response);
		}

		//private void GetAllTickets()
		//{
		//	var tickets = ticketService.GetAll();
		//	string data = JsonConvert.SerializeObject(tickets);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		//private void UpsertTicket(string requestMessage)
		//{
		//	var requestTicket = JsonConvert.DeserializeObject<Ticket>(requestMessage);

		//	ticketService.Upsert(requestTicket);
		//	Response response = new Response(ResponseTypes.Ok, "Успешно");

		//	SendResponseAsync(response);
		//}

		//private void DeleteTicket(string requestMessage)
		//{
		//	var ticket = ticketService.Get(int.Parse(requestMessage));

		//	ticketService.Remove(ticket);
		//	Response response = new Response(ResponseTypes.Ok, "Билет успешно удален");
		//	SendResponseAsync(response);
		//}

		private void Close()
		{
			writer.Close();
			reader.Close();
			client.Close();
		}
	}
}
