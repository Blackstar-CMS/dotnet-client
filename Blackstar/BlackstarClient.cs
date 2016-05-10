using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blackstar
{
    public class BlackstarClient
    {
        readonly HttpClient _client;
        readonly BlackstarServerRoutes _routes;

        public BlackstarClient(string serverUrl)
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            _routes = new BlackstarServerRoutes(new Uri(serverUrl));
            _client = new HttpClient();
        }

        public async Task<ContentChunk[]> GetAllAsync()
        {
            return await FetchChunksAtAsync(_routes.AllChunks());
        }

        public async Task<ContentChunk> GetByIdAsync(int id)
        {
            return (await GetByIdsAsync(new[] {id })).SingleOrDefault();
        }

        public async Task<ContentChunk[]> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await FetchChunksAtAsync(_routes.GetByIds(ids));
        }

        public async Task<ContentChunk> GetByNameAsync(string name)
        {
            return (await GetByNamesAsync(new[] {name})).SingleOrDefault();
        }

        public async Task<ContentChunk[]> GetByNamesAsync(IEnumerable<string> names)
        {
            return await FetchChunksAtAsync(_routes.GetByNames(names));
        }

        public async Task<ContentChunk> GetByTagAsync(string tag)
        {
            return (await GetByTagsAsync(new[] {tag})).SingleOrDefault();
        }

        public async Task<ContentChunk[]> GetByTagsAsync(IEnumerable<string> tags)
        {
            return await FetchChunksAtAsync(_routes.GetByTags(tags));
        }

        private async Task<ContentChunk[]> FetchChunksAtAsync(Uri address)
        {
            var response = await _client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContentChunk[]>(body);
            }
            return new ContentChunk[0];
        }
    }
}
