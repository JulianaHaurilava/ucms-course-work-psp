namespace CMSLib.DTO
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public required Company Company { get; set; }
    }
}
