using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

/**
 *
 * 
 *This is only for JSON data
 *
 * 
 */

namespace aamarPay_asp_dot_net_core.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        

        public PaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost("payment")]
        [Consumes("application/json")]
        public async Task<ActionResult> Payment([FromBody] dynamic formData)
        {
            var payment = new StringContent(
                JsonSerializer.Serialize(formData),
                System.Text.Encoding.UTF8,
                "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage =  await httpClient.PostAsync("https://sandbox.aamarpay.com/jsonpost.php", payment) ;
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            return Ok(response);
        }

        /**
         * This api receives form-data. Content-Type is x-www-form-urlencoded
         */
        [HttpPost("payment-callback")]
        [Consumes("application/x-www-form-urlencoded")]
        public Task<ActionResult> Callback()
        {
            dynamic response;
            var dict = new Dictionary<string, string>();
            
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                var value = HttpContext.Request.Form[key];
                dict.Add(key, value);
            }
            
            response = JsonConvert.SerializeObject(dict);
            
            return Task.FromResult<ActionResult>(Ok(response));
        }
    }
}