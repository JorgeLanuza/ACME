namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Repositories.Tools;
    using ACME.Dtos;
    using Microsoft.Extensions.Logging;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    public class VisitJsonRepository
    {
        private readonly ILogger<VisitJsonRepository> _logger;

        private readonly string _jsonFilePath;
        public VisitJsonRepository(ILogger<VisitJsonRepository> logger)
        {
            _logger = logger;
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _jsonFilePath = Path.Combine(basePath, "Assets", "visits.json");
            _logger.LogInformation("JSON file path resolved to: {JsonFilePath}", _jsonFilePath);
        }
        public async Task<IEnumerable<VisitDto>> GetAllFromJsonAsync()
        {
            try
            {
                if (!File.Exists(_jsonFilePath))
                {
                    _logger.LogWarning("JSON file not found: {JsonFilePath}", _jsonFilePath);
                    return new List<VisitDto>();
                }
                string jsonData = await File.ReadAllTextAsync(_jsonFilePath);
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(), new GuidConverter() }
                };
                List<VisitDto> deserializedData = JsonSerializer.Deserialize<List<VisitDto>>(jsonData, options);
                if (deserializedData == null || deserializedData.Count == 0)
                {
                    _logger.LogWarning("JSON file is empty or could not be deserialized.");
                    return new List<VisitDto>();
                }
                _logger.LogInformation("Successfully read {Count} records from JSON file.", deserializedData.Count);
                return deserializedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading JSON file");
                return new List<VisitDto>();
            }
        }
        public async Task AddToJsonAsync(VisitDto visitDto)
        {
            var visits = await GetAllFromJsonAsync() as List<VisitDto> ?? new List<VisitDto>();
            visits.Add(visitDto);
            await SaveToJsonAsync(visits);
        }
        public async Task UpdateInJsonAsync(VisitDto visitDto)
        {
            var visits = await GetAllFromJsonAsync() as List<VisitDto> ?? new List<VisitDto>();
            var index = visits.FindIndex(v => v.Id == visitDto.Id);
            if (index != -1)
            {
                visits[index] = visitDto;
                await SaveToJsonAsync(visits);
            }
        }
        public async Task DeleteFromJsonAsync(Guid id)
        {
            var visits = await GetAllFromJsonAsync() as List<VisitDto> ?? new List<VisitDto>();
            visits.RemoveAll(v => v.Id == id);
            await SaveToJsonAsync(visits);
        }
        private async Task SaveToJsonAsync(List<VisitDto> visits)
        {
            var jsonData = JsonSerializer.Serialize(visits, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_jsonFilePath, jsonData);
        }
    }
}
