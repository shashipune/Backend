using HomeAssignment.Core;
using HomeAssignment.Data;
using System.Text.Json;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly string _filePath;
    private List<T> _data;

    public Repository()
    {
        // Get the base directory (output directory)
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var typeName = typeof(T).Name;
        // Construct the full path to the JSON file from HomeAssignment.Core project
        var jsonFilePath = Path.Combine(baseDirectory, $"{typeName}.json");
        _filePath = jsonFilePath;
        _data = new List<T>();
        LoadDataFromFile();
    }

    private async Task SaveEmptyFile()
    {
        string emptyJson = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, emptyJson);
    }
    private void LoadDataFromFile()
    {
        //try
        //{
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);

            // Check if the json string is not empty or whitespace
            if (!string.IsNullOrWhiteSpace(json))
            {
                _data = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            else
            {
                _data = new List<T>();
                SaveEmptyFile();
            }
        }
        else
        {
            _data = new List<T>();
            SaveEmptyFile();
        }
        //}
        //catch { }
    }

    private async Task SaveDataToFileAsync()
    {
        string json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_filePath, json);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Task.FromResult(_data);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = _data.FirstOrDefault(e => GetEntityId(e) == id);
        return await Task.FromResult(entity!);
    }

    public async Task AddAsync(T entity)
    {
        var result = await GetAllAsync();
        int id = 1;
        if (result != null && result.Count() > 0)
        {
            id = result.Max(c => c.Id)+1;
        }
        entity.Id = id;
        _data.Add(entity);
        await SaveDataToFileAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        var existingEntity = _data.FirstOrDefault(e => GetEntityId(e) == GetEntityId(entity));
        if (existingEntity != null)
        {
            _data.Remove(existingEntity);
            entity.UpdateDate = DateTime.UtcNow;
            _data.Add(entity);
            await SaveDataToFileAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var entity = _data.FirstOrDefault(e => GetEntityId(e) == id);
        if (entity != null)
        {
            _data.Remove(entity);
            await SaveDataToFileAsync();
        }
    }

    private int GetEntityId(T entity)
    {
        var propertyInfo = typeof(T).GetProperty("Id");
        return (int)(propertyInfo?.GetValue(entity) ?? 0);
    }
}
