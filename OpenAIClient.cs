using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;

namespace transcribeApp;

public class OpenAIClient
{
    private readonly string _apiKey;
    public OpenAIClient(string key)
    {
        _apiKey = key;
    }

    public async Task<string> TranscribeAudio()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "audio\\my_recording.mp3");

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            using (var form = new MultipartFormDataContent())
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mp3"); // Change based on file type

                    form.Add(fileContent, "file", Path.GetFileName(filePath));
                    form.Add(new StringContent("whisper-1"), "model");

                    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);

                    string result = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(result);
                    return result;
                }
            }
        }
    }
}
