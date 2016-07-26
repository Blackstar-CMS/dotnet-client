using System;
using System.Collections.Generic;

namespace Blackstar
{
    class BlackstarServerRoutes
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
}