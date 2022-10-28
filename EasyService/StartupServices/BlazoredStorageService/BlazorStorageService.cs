using System.Text;
using System.Text.Json;
using Blazored.SessionStorage;

namespace EasyService.StartupServices.BlazoredStorageService;

public static class BlazorStorageService
{
    public static async Task SaveItemEncryptedAsync<T>(this ISessionStorageService storageService, string Key, T item)
    {
        var ItemJson = JsonSerializer.Serialize(item);
        var ItemJsonBytes = Encoding.UTF8.GetBytes(ItemJson);
        var value = Convert.ToBase64String(ItemJsonBytes);
        await storageService.SetItemAsync(Key, value);
    }

    public static async Task<T> ReadEncryptedItemAsync<T>(this ISessionStorageService storageService, string Key)
    {
        var value = await storageService.GetItemAsync<string>(Key);
        var ItemJsonBytes = Convert.FromBase64String(value);
        var ItemJson = Encoding.UTF8.GetString(ItemJsonBytes);
        var Item = JsonSerializer.Deserialize<T>(ItemJson);
        return Item;
    }
}