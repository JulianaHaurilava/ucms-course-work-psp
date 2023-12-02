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

						//case RequestTypes.GetCompanies:
						//	GetAllCo();
						//	break;
						//case RequestTypes.UpsertFilm:
						//	UpsertFilm(request.Message);
						//	break;
						//case RequestTypes.DeleteFilm:
						//	DeleteFilm(request.Message);
						//	break;

						//case RequestTypes.GetUsers:
						//	GetAllUsers();
						//	break;
						//case RequestTypes.UpsertUser:
						//	UpsertUser(request.Message);
						//	break;
						//case RequestTypes.DeleteUser:
						//	DeleteUser(request.Message);
						//	break;

						//case RequestTypes.GetCinemas:
						//	GetAllCinemas();
						//	break;
						//case RequestTypes.UpsertCinema:
						//	UpsertCinema(request.Message);
						//	break;
						//case RequestTypes.DeleteCinema:
						//	DeleteCinema(request.Message);
						//	break;

						//case RequestTypes.GetHalls:
						//	GetAllHalls();
						//	break;
						//case RequestTypes.UpsertHall:
						//	UpsertHall(request.Message);
						//	break;
						//case RequestTypes.DeleteHall:
						//	DeleteHall(request.Message);
						//	break;

						//case RequestTypes.GetSessions:
						//	GetAllSessions();
						//	break;
						//case RequestTypes.UpsertSession:
						//	UpsertSession(request.Message);
						//	break;
						//case RequestTypes.DeleteSession:
						//	DeleteSession(request.Message);
						//	break;

						//case RequestTypes.GetTickets:
						//	GetAllTickets();
						//	break;
						//case RequestTypes.UpsertTicket:
						//	UpsertTicket(request.Message);
						//	break;
						//case RequestTypes.DeleteTicket:
						//	DeleteTicket(request.Message);
						//	break;

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
			var user = users.Find(u => u.Login.Equals(requestUser.Login, StringComparison.OrdinalIgnoreCase));
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

		//private void GetAllFilms()
		//{
		//	var films = filmService.GetAll();
		//	string data = JsonConvert.SerializeObject(films);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		//private void UpsertFilm(string requestMessage)
		//{
		//	var requestFilm = JsonConvert.DeserializeObject<Film>(requestMessage);

		//	filmService.Upsert(requestFilm);
		//	Response response = new Response(ResponseTypes.Ok, "Фильм успешно добавлен");
		//	SendResponseAsync(response);
		//}

		//private void DeleteFilm(string requestMessage)
		//{
		//	var film = filmService.Get(int.Parse(requestMessage));

		//	filmService.Remove(film);
		//	Response response = new Response(ResponseTypes.Ok, "Фильм успешно удален");
		//	SendResponseAsync(response);
		//}

		//private void GetAllUsers()
		//{
		//	var users = userService.GetAll();
		//	string data = JsonConvert.SerializeObject(users);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		//private void UpsertUser(string requestMessage)
		//{
		//	var requestUser = JsonConvert.DeserializeObject<User>(requestMessage);

		//	var users = userService.GetAll();
		//	var user = users.Find(u => u.Login.Equals(requestUser.Login, StringComparison.OrdinalIgnoreCase));
		//	Response response;
		//	if (user != null)
		//	{
		//		if (user.Id != requestUser.Id)
		//		{
		//			response = new Response(ResponseTypes.NotOk, "Такой пользователь уже существует");
		//		}
		//		else
		//		{
		//			userService.Upsert(requestUser);
		//			response = new Response(ResponseTypes.Ok, "Успешно");
		//		}
		//	}
		//	else
		//	{
		//		userService.Upsert(requestUser);
		//		response = new Response(ResponseTypes.Ok, "Успешно");
		//	}
		//	SendResponseAsync(response);
		//}

		//private void DeleteUser(string requestMessage)
		//{
		//	var user = userService.Get(int.Parse(requestMessage));

		//	userService.Remove(user);
		//	Response response = new Response(ResponseTypes.Ok, "Пользователь успешно удален");
		//	SendResponseAsync(response);
		//}

		//private void GetAllCinemas()
		//{
		//	var Cinemas = cinemaService.GetAll();
		//	string data = JsonConvert.SerializeObject(Cinemas);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}
		//private void UpsertCinema(string requestMessage)
		//{
		//	var requestCinema = JsonConvert.DeserializeObject<Cinema>(requestMessage);

		//	cinemaService.Upsert(requestCinema);
		//	Response response = new Response(ResponseTypes.Ok, "Успешно");

		//	SendResponseAsync(response);
		//}

		//private void DeleteCinema(string requestMessage)
		//{
		//	var cinema = cinemaService.Get(int.Parse(requestMessage));

		//	cinemaService.Remove(cinema);
		//	Response response = new Response(ResponseTypes.Ok, "Кинотеатр успешно удален");
		//	SendResponseAsync(response);
		//}

		//private void GetAllHalls()
		//{
		//	var halls = hallService.GetAll();
		//	string data = JsonConvert.SerializeObject(halls);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		//private void UpsertHall(string requestMessage)
		//{
		//	var requestHall = JsonConvert.DeserializeObject<Hall>(requestMessage);

		//	hallService.Upsert(requestHall);
		//	Response response = new Response(ResponseTypes.Ok, "Успешно");

		//	SendResponseAsync(response);
		//}

		//private void DeleteHall(string requestMessage)
		//{
		//	var hall = hallService.Get(int.Parse(requestMessage));

		//	hallService.Remove(hall);
		//	Response response = new Response(ResponseTypes.Ok, "Зал успешно удален");
		//	SendResponseAsync(response);
		//}

		//private void GetAllSessions()
		//{
		//	var sessions = sessionService.GetAll();
		//	string data = JsonConvert.SerializeObject(sessions);
		//	Response response = new Response(ResponseTypes.Ok, "", data);
		//	SendResponseAsync(response);
		//}

		//private void UpsertSession(string requestMessage)
		//{
		//	var requestSession = JsonConvert.DeserializeObject<Session>(requestMessage);

		//	sessionService.Upsert(requestSession);
		//	Response response = new Response(ResponseTypes.Ok, "Успешно");

		//	SendResponseAsync(response);
		//}

		//private void DeleteSession(string requestMessage)
		//{
		//	var session = sessionService.Get(int.Parse(requestMessage));

		//	sessionService.Remove(session);
		//	Response response = new Response(ResponseTypes.Ok, "Сеанс успешно удален");
		//	SendResponseAsync(response);
		//}

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
