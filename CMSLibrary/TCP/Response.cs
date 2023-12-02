using CMSLib.Enum;

namespace CMSLib.TCP
{
	public class Response
	{
		public ResponseTypes Type { get; set; }
		public string Message { get; set; }
		public string Data { get; set; }

		public Response() { }

		public Response(ResponseTypes type, string message, string data)
		{
			Type = type;
			Message = message;
			Data = data;
		}

		public Response(ResponseTypes type, string message)
		{
			Type = type;
			Message = message;
			Data = "";
		}
	}
}
