﻿namespace CMSLib.DTO
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; } = double.MinValue;
        public required Site Site { get; set; }
    }
}
