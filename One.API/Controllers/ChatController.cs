using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using One.Core.Models;
using One.Repository;
using System.Net.Http;
using System.Text.Json;

namespace One.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _appDbContext;

        public ChatController(IHttpClientFactory httpClientFactory, AppDbContext dbContext, AppDbContext appDbContext)
        {
            _httpClient=httpClientFactory.CreateClient("OpenAI");
            _appDbContext=appDbContext;
        }


        [HttpPost]
        public async Task<ActionResult<string>> PostAsync(string message)
        {
            _appDbContext.Users.Add(new User { Name = "Kadir", Email = "kadir@gmail.com", Message = message });
            await _appDbContext.SaveChangesAsync();

            //Send user input to OPENAI API

            var input = new { prompt = message, temperature = 0.5 }; 
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("completions", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<OpenAIResponse>(jsonResult);
                return Ok(result);
            } else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
    }


    }
}
