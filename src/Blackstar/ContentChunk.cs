using System;
using System.Linq;

namespace Blackstar
{
    public class ContentChunk
    {
        public int Id { get; private set; }
        public string[] Tags { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }
        public string Html { get; private set; }

        public ContentChunk(int id, string[] tags, string name, string value, string html = "")
        {
            if (id < 0) throw new ArgumentException("id must be 0 or greater");
            if (tags == null) throw new ArgumentException("Tags is required");
            if (tags.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Tags cannot be empty");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
            if (value == null) throw new ArgumentException("Value is required");

            Id = id;
            Tags = tags;
            Name = name;
            Value = value;
            Html = html;
        }
    }
}