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
            Converters = { new GuidConverter() }
        };
        public static async Task<List<T>> LoadDataAsync<T>(string fileName, ILogger? logger = null)
        {
            try
            {
                string filePath = Path.Combine(_jsonPath, fileName);
                if (!File.Exists(filePath))
                {
                    logger?.LogWarning($"El archivo JSON {fileName} no existe en la carpeta Assets.");
                    return new List<T>();
                }
                string jsonData = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonData, _jsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                logger?.LogError($"Error al cargar datos desde {fileName}: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
