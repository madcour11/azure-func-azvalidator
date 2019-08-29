using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace core_azvalidator
{
    public static class alphabetValidator
    {
        [FunctionName("alphabetValidator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "api/azValidator/validate")] HttpRequest req,
            ILogger log)
        {
            var input = "";
            log.LogInformation("alphabetValidator function has processed a request.");
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic body = JsonConvert.DeserializeObject(reqBody);

            input = body?.input;
            if (input == null || !(input is string))
            {
                //Error
                return new BadRequestObjectResult("Please provide a valid string.");
            }
            //Reasoning: If there are fewer letters in the string than in the alphabet, it fails
            else if (String.IsNullOrWhiteSpace(input) || input.Length < 26)
            {
                //Ok
                return new OkObjectResult(false);
            }
            var hasAllLetters = false;
             try
            {
                //Reasoning: Normalize the input that this works with a set data structure
                var loweredInput = input.ToLower();
                var alphabetSet = new SortedSet<char>();
                foreach (var c in input)
                {
                    if (Char.IsLetter(c))
                    {
                        alphabetSet.Add(c);
                    }
                    //Reasoning: If the alphabet tracking set reaches 26 distinct letters, the whole alphabet was in the string
                    //and we can stop iterating over the input early
                    if (alphabetSet.Count == 26)
                    {
                        hasAllLetters = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //Error: Input processing failed
                var errorResponse = new StatusCodeResult(500);
                return errorResponse;
            }
            
            //Ok
            return (ActionResult)new OkObjectResult(hasAllLetters);
        }
    }
}
