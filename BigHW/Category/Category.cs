using System;

namespace BigHW.Category
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Type { get; set; } 
        public string Name { get; set; }

        public Category(string name, string type)
        {
            Id = Guid.NewGuid();
            Type = type;
            Name = name;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nType: {Type}\nName: {Name}";
        }
    }
}