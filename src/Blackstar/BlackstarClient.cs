using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blackstar
{
    public class BlackstarClient
    {
        readonly HttpClient _client;
        readonly BlackstarServerRoutes _routes;
        Action _addTokenIfRequired;
        Lazy<SigningCredentials> _credentials;

        public BlackstarClient(string serverUrl, string username="", string APIKey="")
        {
            if (serverUrl == null) throw new ArgumentNullException(nameof(serverUrl));
            _routes = new BlackstarServerRoutes(new Uri(serverUrl));
            _client = new HttpClient();
            _addTokenIfRequired = () => {
                AddTokenIfRequired(username, APIKey);
            };
            _credentials = new Lazy<SigningCredentials>(() => 
                string.IsNullOrEmpty(APIKey) ? null :
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(APIKey)), SecurityAlgorithms.HmacSha256));
            _addTokenIfRequired();
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

        public async Task<ContentChunk[]> GetByTagAsync(string tag)
        {
            return await GetByTagsAsync(new[] {tag});
        }

        public async Task<ContentChunk[]> GetByTagsAsync(IEnumerable<string> tags)
        {
            return await FetchChunksAtAsync(_routes.GetByTags(tags));
        }

        private async Task<ContentChunk[]> FetchChunksAtAsync(Uri address)
        {
            _addTokenIfRequired();
            var response = await _client.GetAsync(address);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ContentChunk[]>(body);
            }
            return new ContentChunk[0];
        }

        /// <summary>
        /// If a username and APIKey have been provided then create a JWT token
        /// and include it in the Authorization header.
        /// </summary>
        /// <param name="username">The username to be encoded in the token and sent to the server for authorization</param>
        /// <param name="APIKey">The API key to use</param>
        private void AddTokenIfRequired(string username, string APIKey)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(APIKey))
            {
                var jwt = new JwtSecurityToken(claims: new Claim[]
                        {
                            new Claim("name", username)
                        },
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: _credentials.Value);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    encodedJwt);
            }
        }

    }
}
