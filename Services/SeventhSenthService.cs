using Newtonsoft.Json;
using Reisinger_Intelliface_1_0.FaceRecognition;
using Reisinger_Intelliface_1_0.Helper;
using Reisinger_Intelliface_1_0.Model;

namespace Reisinger_Intelliface_1_0.Services;

public class SeventhSenthService : IFaceRecognizer
{
    private readonly IConfiguration _configuration;
    private string? Url;
    private string Region;
    private string? ApiKey;


    public SeventhSenthService(IConfiguration configuration)
    {
        _configuration = configuration;

        IConfigurationSection seventSenthConfig = configuration.GetSection("7Senth");


        Url = seventSenthConfig.GetValue<string>("Url");
        ApiKey = seventSenthConfig.GetValue<string>("ApiKey");
    }


    private HttpClient GetClient()
    {
        if (Url == null || ApiKey == null)
        {
            throw new ArgumentException("7Senth Config missing");
        }

        var client = new HttpClient
        {
            BaseAddress = new Uri(Url)
        };

        client.DefaultRequestHeaders.Add("X-API-Key", ApiKey);

        return client;
    }

    public async Task<RecognizeResult> SeventhRecognize(List<FaceImage> recordedImages)
    {
        using (var client = GetClient())
        {

            var searchFaceRequest = new
            {
                images = Base64ImageHelper.ToBase64Images(recordedImages),
                max_results = 10,
                min_score = 0.7,
                search_mode = "ACCURATE"
            };



            string jsonRequest = JsonConvert.SerializeObject(searchFaceRequest);
            HttpContent content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            // Senden Sie die POST-Anfrage.
            HttpResponseMessage response = await client.PostAsync("search", content);

            if (response.IsSuccessStatusCode)
            {
                // Verarbeiten Sie die erfolgreiche API-Antwort hier.
                string jsonResponse = await response.Content.ReadAsStringAsync();




                List<RecognizeResult> result = JsonConvert.DeserializeObject<List<RecognizeResult>>(jsonResponse);

                List<RecognizeResult> ordererdByScore = result.OrderByDescending(m => m.score).ToList();

                RecognizeResult? bestResult = ordererdByScore.FirstOrDefault();


                if (bestResult != null)
                {
                    string name = bestResult.name;
                    double score = bestResult.score;
                }

                return bestResult;
            }
            else
            {
                // Handhaben Sie den Fall, wenn die Anfrage fehlschlägt, z.B., durch Ausgabe des Statuscodes und der Fehlermeldung.
                throw new Exception($"API-Anfrage fehlgeschlagen: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
    public async Task<bool> SeventhTeach(User employee, List<string?> teachData)
    {
        // prüfen ob die person schon auf 7senth exisitert ( api getperson)

        List<string?> base64ImagesOfEmployee = new List<string?>();
        foreach (string faceImage in teachData)
        {
            base64ImagesOfEmployee.Add(faceImage);
        }

        // request für create (falls noch nicht exisiert zusammenbauen
        var personRequest = new
        {
            collections = new[] { "string" }, // Hier kannst du die Sammlungen anpassen
            date_of_birth = employee.DateOfBirth.ToString("yyyy-MM-dd"),
            gender = employee.Gender, // Passe dies entsprechend deiner Datenbank an
            id = employee.ID.ToString(),
            images = base64ImagesOfEmployee, // Wandele die Bilder in Base64 um
            is_bulk_insert = false, // Passe dies nach Bedarf an
            name = $"{employee.First_Name} {employee.Last_Name}",
            nationality = employee.Nationality,
            notes = employee.Notes
        };

        using (var client = GetClient())
        {
            string jsonRequestContent = JsonConvert.SerializeObject(personRequest);

            StringContent content = new StringContent(jsonRequestContent, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("person", content);

            if (response.IsSuccessStatusCode)
            {
                // Verarbeite die erfolgreiche API-Antwort hier, falls benötigt
                string jsonResponse = await response.Content.ReadAsStringAsync();
                RecognizeResult result = JsonConvert.DeserializeObject<RecognizeResult>(jsonResponse);

                System.Diagnostics.Debug.WriteLine("var response erhalten" + result);
                return true;
            }
        }


        throw new NotImplementedException();
    }

    public async Task<bool> SeventhDeletePerson(string userId)
    {
        using (var client = GetClient())
        {
            // Die URL für die Anfrage, um eine Person zu löschen
            string deletePersonUrl = $"person/{userId}";

            // Senden Sie die DELETE-Anfrage.
            HttpResponseMessage response = await client.DeleteAsync(deletePersonUrl);

            if (response.IsSuccessStatusCode)
            {
                // Verarbeiten Sie die erfolgreiche API-Antwort hier, wenn benötigt.
                string jsonResponse = await response.Content.ReadAsStringAsync();
                RecognizeResult result = JsonConvert.DeserializeObject<RecognizeResult>(jsonResponse);
                // ... Weitere Verarbeitung ...

                // Beispiel: Drucken Sie die erfolgreiche Antwort auf der Konsole aus
                Console.WriteLine($"Person with ID {userId} successfully deleted.");
                return true;
            }
            else
            {
                // Handhaben Sie den Fall, wenn die Anfrage fehlschlägt, z.B., durch Ausgabe des Statuscodes und der Fehlermeldung.
                Console.WriteLine($"API-Anfrage fehlgeschlagen: {response.StatusCode} - {response.ReasonPhrase}");
                Console.WriteLine($"Person with ID {userId} not deleted.");
                return false;
            }
        }
    }


}