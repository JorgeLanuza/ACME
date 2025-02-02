namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Entities;
    using ACME.DataAccess.Repositories.Tools;
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
            string? basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (basePath != null)
                _jsonFilePath = Path.Combine(basePath, "Assets", "visits.json");
            _logger.LogInformation("JSON file path resolved to: {JsonFilePath}", _jsonFilePath);
        }
        public async Task<IEnumerable<VisitEntity>> GetAllFromJsonAsync()
        {
            try
            {
                var visitEntities = await JsonDataLoader.LoadDataAsync<VisitEntity>("visits.json", _logger);
                if (visitEntities == null || !visitEntities.Any())
                {
                    _logger.LogWarning("JSON file is empty or could not be deserialized.");
                    return new List<VisitEntity>();
                }
                _logger.LogInformation("Successfully read {Count} records from JSON file.", visitEntities.Count);
                return visitEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading JSON file");
                return new List<VisitEntity>();
            }
        }
        public async Task AddToJsonAsync(VisitEntity newVisit)
        {
            var visits = await GetAllFromJsonAsync() as List<VisitEntity> ?? new List<VisitEntity>();
            visits.Add(newVisit);
            await SaveToJsonAsync(visits);
        }
        public async Task UpdateInJsonAsync(VisitEntity currentVisit)
        {
            var visits = await GetAllFromJsonAsync() as List<VisitEntity> ?? new List<VisitEntity>();
            var index = visits.FindIndex(visit => visit.Id == currentVisit.Id);
            if (index != -1)
            {
                visits[index] = currentVisit;
                await SaveToJsonAsync(visits);
            }
        }
        public async Task<bool> DeleteFromJsonAsync(Guid selectedVisit)
        {
            if (await GetAllFromJsonAsync() is not List<VisitEntity> visits)
                return false;
            visits.RemoveAll(visit => visit.Id == selectedVisit);
            await SaveToJsonAsync(visits);
            return true;
        }
        public async Task<VisitEntity?> GetByIdAsync(Guid id)
        {
            try
            {
                IEnumerable<VisitEntity> visits = await GetAllFromJsonAsync();
                return visits.FirstOrDefault(v => v.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting element");
                return null;
            }
        }
        private async Task SaveToJsonAsync(List<VisitEntity> visits)
        {
            string jsonData = JsonSerializer.Serialize(visits, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_jsonFilePath, jsonData);
        }
    }
}
