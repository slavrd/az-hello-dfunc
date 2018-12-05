using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace helloazfunc
{
    public static class HelloFunctions
    {
        [FunctionName("hello")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("hello_say", "hello F1"));
            outputs.Add(await context.CallActivityAsync<string>("hello_say", "hello F2"));
            outputs.Add(await context.CallActivityAsync<string>("hello_say", "hello F3"));

            return outputs;
        }

        [FunctionName("hello_say")]
        public static string SayHello([ActivityTrigger] string word, ILogger log)
        {
            log.LogInformation($"saying word: {word}.");
            return $"{word}";
        }

        [FunctionName("hello_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("hello", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}