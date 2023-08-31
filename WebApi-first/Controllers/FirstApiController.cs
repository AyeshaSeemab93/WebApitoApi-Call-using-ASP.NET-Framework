using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace WebApi_first.Controllers
{
    [ApiController]         //tell this class is API CONTROLLER and enable automatic validation os requests
    [Route("api/[controller]")]
    public class FirstApiController : Controller
    {
        //Main Method
        [HttpGet("Get")]
        public IEnumerable<string> Get() //IEnumerable:   bec we need to return a list of strings.
        {
            Console.WriteLine("Get into it"); //for debugging

            //Create and write file log1.txt(for keeping log of how many times it called)

            //string[] linesToAppend = new[] { "Go get called in Api1" };
            //System.IO.File.AppendAllLines("log1.txt", linesToAppend);  OR

            System.IO.File.AppendAllLines("log1.txt", new[] { "Go get called in Api1" });
            //whenever the Get method is called, a new line will be added to the file.

            System.Threading.Thread.Sleep(400); //400millisec delay/pause here in execution of next code


            String[] greeting = new[] { "Moi from 1st Api" };
            return greeting;
            //or            return new[] { "Moi from 1st Api" };
        }



        //Method to make Call
        [HttpGet("CallApi2")]
        public async Task<string> CallApi2() //this will eventually give a string after wait(task is ticket assigned)
        {
            //1. create instance/obj of HTTPClient class.It heandle HTTP Requests(call/Get/POST)
            HttpClient client = new HttpClient();//For sending request

            // Calling(Send a GET request and get the response)
            //GetAsync: sends an asynchronous HTTP GET request to the specified URL
            //await: wait for the reponse but let main method do next tasks

            var response = await client.GetAsync("https://localhost:7256/api/SecondApi/RecieveFromApi");

            if (response.IsSuccessStatusCode)
            {
                //response: obtained from above GET request. Content is content of the response
                //ReadAsStringAsync(): Method of Content class that reads the content of the HTTP response as a string.

                var responseData = await response.Content.ReadAsStringAsync();

                return ("Successfully called API-2 " + responseData); //return content of response
            }
            else
            {
                return ("Error!" + response.StatusCode); //return code
            }

        }



        //Method to handle the call request(from other Api) and provide response
        [HttpGet("RecieveFromApi")]                       //Route for receieving request
        public IActionResult RecieveFromApi2()
        {
            var message = "Successfully received call from Api2 in Api1";
            return Ok(message);

        }


    }
}

