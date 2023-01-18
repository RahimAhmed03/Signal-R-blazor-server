using System.Net.Http;
using System.Web;
using System.Text.Json;

namespace BlazorServerSignalRApp.Data;

public class StickerService {
    private HttpClient httpClient;

    public StickerService(string apiKey) {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("apiKey", apiKey);
    }

    public async Task<Sticker> Search(string userId, string? term) {
        var uriBuilder = new UriBuilder("https://messenger.stipop.io/v1/search");
        var query = HttpUtility.ParseQueryString(String.Empty);
        query["q"] = term ?? "";
        query["userId"] = userId;
        uriBuilder.Query = query.ToString();
        var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) {
            throw new InvalidOperationException("Failed to call sticker API");
        }
        var content = await response.Content.ReadAsStringAsync();
        var obj = JsonSerializer.Deserialize<Sticker>(content);
        if (obj == null) {
            throw new InvalidDataException();
        }
        return obj;
    }
}