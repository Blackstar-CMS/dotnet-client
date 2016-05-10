using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blackstar
{
    public class Client
    {
        public Getter Get { get; private set; }

        public Client(Uri server)
        {
            Get = new Getter(server);
        }
    }

    public class Getter
    {
        HttpClient _client;
        BlackstarServerRoutes _routes;

        public Getter(Uri server)
        {
            if (server == null) throw new ArgumentNullException("server");
            _routes = new BlackstarServerRoutes(server);
            _client = new HttpClient();
        }

        public async Task<IEnumerable<Chunk>> All()
        {
            return await FetchChunksAt(_routes.AllChunks());
        }

        public async Task<IEnumerable<Chunk>> ByIds(IEnumerable<int> ids)
        {
            return await FetchChunksAt(_routes.GetByIds(ids));
        }

        public async Task<IEnumerable<Chunk>> ByNames(IEnumerable<string> names)
        {
            return await FetchChunksAt(_routes.GetByNames(names));
        }

        public async Task<IEnumerable<Chunk>> ByTags(IEnumerable<string> tags)
        {
            return await FetchChunksAt(_routes.GetByTags(tags));
        }

        private async Task<IEnumerable<Chunk>> FetchChunksAt(Uri address)
        {
            HttpResponseMessage response = await _client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Chunk[]>(body);
            }
            return new Chunk[0];
        }
    }

    public class BlackstarServerRoutes
    {
        Uri _server;
        string _apiPath = "api/content";
        Uri _api;
        public BlackstarServerRoutes(Uri server)
        {
            if (server == null) throw new ArgumentNullException("server");
            _server = server;
            _api = new Uri(server, _apiPath);
        }

        public Uri AllChunks()
        {
            return _api;
        }

        public Uri GetByIds(IEnumerable<int> ids)
        {
            return new Uri(_server, _apiPath + "/byids/" + string.Join("/", ids));
        }

        public Uri GetByNames(IEnumerable<string> names)
        {
            return new Uri(_server, _apiPath + "/bynames/" + string.Join("/", names));
        }

        public Uri GetByTags(IEnumerable<string> tags)
        {
            return new Uri(_server, _apiPath + "/bytags/" + string.Join("/", tags));
        }
    }

    public class Chunk
    {
        public int Id { get; private set; }
        public string[] Tags { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }

        public Chunk(int id, string[] tags, string name, string value)
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
        }
    }
}
