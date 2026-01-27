using Newtonsoft.Json;
using ShopTARgv24.Core.Domain;
public class ChuckNorrisServices
{
    private readonly HttpClient _httpClient;

    public ChuckNorrisServices(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetRandomJokeAsync()
    {
        string url = "https://api.chucknorris.io/jokes/random";

        var response = await _httpClient.GetStringAsync(url);
        var joke = JsonConvert.DeserializeObject<ChuckNorris>(response);

        return joke.Value;
    }
}