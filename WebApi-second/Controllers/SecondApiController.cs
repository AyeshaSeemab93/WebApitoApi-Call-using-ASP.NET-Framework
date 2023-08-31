using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebApi_second.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecondApiController : Controller
    {

        [HttpGet("Get")]
        public IEnumerable<string> Get()
        {
            Console.WriteLine("Go get in Api 2"); //fordebugging

            System.IO.File.AppendAllLines("log2.txt", new[] { "Running the Api" });//for booklog

            System.Threading.Thread.Sleep(400);



            return new[] { "Moi from 2nd Api" };

        }






        //Method to handle the call request(from other Api) and provide response
        [HttpGet("RecieveFromApi")]                       //Route for receieving request
        public IActionResult RecieveFromApi1()
        {
            var message = "Successfully received call from Api1 in Api2";
            return Ok(message);

        }

        //Method to make Call
        [HttpGet("CallApi1")]
        public async Task<string> CallApi1()
        {

            HttpClient client = new HttpClient();//For sending request

            var response = await client.GetAsync("https://localhost:7131/api/FirstApi/RecieveFromApi");

            if (response.IsSuccessStatusCode)
            {
                //response: obtained from above GET request. Content is content of the response
                //ReadAsStringAsync(): Method of Content class that reads the content of the HTTP response as a string.

                var responseData = await response.Content.ReadAsStringAsync();

                return ("Successfully called API-1 " + responseData); //return content of response
            }
            else
            {
                return ("Error!" + response.StatusCode); //return code
            }
        }
    }
}

