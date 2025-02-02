namespace ACME.DataAccess.Repositories
{
    using ACME.DataAccess.Repositories.Tools;
    using Microsoft.Extensions.Logging;
    using System.Text.Json;

    public static class JsonDataLoader
    {
        private static readonly string _jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new GuidConverter() }
        };
        public static async Task<List<T>> LoadDataAsync<T>(string fileName, ILogger? logger = null)
        {
            try
            {
                string filePath = Path.Combine(_jsonPath, fileName);
                if (!File.Exists(filePath))
                {
                    logger?.LogWarning($"The file JSON {fileName} dont exist in the current directory");
                    return new List<T>();
                }

                string jsonData = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonData, _jsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                logger?.LogError($"Error retrieving {fileName}: {ex.Message}");
                return new List<T>();
            }
        }
        public static async Task<bool> UpdateDataAsync<T>(string fileName, List<T> data, ILogger? logger = null)
        {
            try
            {
                string filePath = Path.Combine(_jsonPath, fileName);
                string updatedJson = JsonSerializer.Serialize(data, _jsonOptions);
                await File.WriteAllTextAsync(filePath, updatedJson);
                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError($"Error saving data {fileName}: {ex.Message}");
                return false;
            }
        }
    }
}
