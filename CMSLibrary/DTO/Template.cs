namespace CMSLib.DTO
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string[] HeaderCode { get; private set; } = {
            @"<!DOCTYPE html>
                <html>
                    <head>
                        <title>{0}</title>",
                    @"</head>
                        <body>
                            <h1>{0}</h1>
                            <p>{1}</p>
                            <h2>Товары</h2>", };
        public string ItemsCode { get; private set; } = 
            @"<div>
                <h3>{0}</h3>
                <p>{1}</p>
                <p>Цена: {2}</p>
              </div>";
        public string EndCode { get; private set; } = "</body></html>";

        public string Style {  get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public required Company Company { get; set; }
    }
}
