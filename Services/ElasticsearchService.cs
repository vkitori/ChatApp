using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using ChatApp.Models;
using Microsoft.Extensions.Logging;

namespace ChatApp.Services
{
    public class ElasticsearchService
    {
        private readonly ElasticsearchClient _client;
        private readonly ILogger<ElasticsearchService> _logger;

        public ElasticsearchService(ElasticsearchClient client, ILogger<ElasticsearchService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task IndexMessageAsync(Message message)
        {
            try
            {
                var response = await _client.IndexAsync(message, i => i.Index("messages"));

                if (response.IsValidResponse)
                {
                    _logger.LogInformation($"Message indexed successfully! ID: {response.Id}");
                }
                else
                {
                    _logger.LogError($"Failed to index message: {response.DebugInformation}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while indexing the message");
            }
        }
    }
}