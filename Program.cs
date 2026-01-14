using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

try
{
    // The API token from your original code
    var apiToken = "2209414843cdbf81d7eb22c3d64c65a2";
    
    // Use HttpClient to call the API directly, removing the need for external SDKs
    using var client = new HttpClient();
    client.BaseAddress = new Uri("https://send.api.mailtrap.io");
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");

    var emailData = new
    {
        from = new { email = "hello@demomailtrap.co", name = "Mailtrap Test" },
        to = new[] { new { email = "azanabbaswani@gmail.com" } },
        subject = "You are awesome!",
        text = "Congrats for sending test email with Mailtrap!",
        category = "Integration Test"
    };

    Console.WriteLine("Sending email...");
    
    var response = await client.PostAsJsonAsync("/api/send", emailData);
    
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Email sent successfully!");
        var responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response: " + responseBody);
    }
    else
    {
        var errorBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Failed to send. Status: {response.StatusCode}");
        Console.WriteLine($"Error: {errorBody}");
    }
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred while sending email: {0}", ex);
}
