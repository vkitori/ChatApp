using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ChatApp.Models;

namespace ChatApp.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;

        public ElasticsearchService(IConfiguration configuration)
        {
            var url = configuration["ElasticsearchSettings:Url"];
            var settings = new ElasticsearchClientSettings(new Uri(url))
                .DefaultIndex(configuration["ElasticsearchSettings:Index"]);

            _client = new ElasticsearchClient(settings);
        }

        public async Task IndexMessageAsync(Message message)
        {
            var response = await _client.IndexAsync(message, i => i.Index("messages"));

            if (response.IsValidResponse)
            {
                Console.WriteLine("Message indexed successfully!");
            }
            else
            {
                Console.WriteLine($"Failed to index message: {response.DebugInformation}");
            }
        }
    }
}