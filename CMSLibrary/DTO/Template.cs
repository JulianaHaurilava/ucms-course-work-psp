namespace CMSLib.DTO
{
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TemplateCode { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public required Company Company { get; set; }
    }
}
