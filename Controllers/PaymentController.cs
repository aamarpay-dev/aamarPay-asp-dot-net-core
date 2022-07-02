using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace aamarPay_asp_dot_net_core.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentController(IHttpClientFactory httpClientFactory) { _httpClientFactory = httpClientFactory; }

        [HttpPost("payment")]
        public async Task<ActionResult> Payment([FromBody] dynamic formData)
        {
            var payment = new StringContent(
                        JsonSerializer.Serialize(formData),
                        System.Text.Encoding.UTF8,
                        "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.PostAsync("https://sandbox.aamarpay.com/jsonpost.php", payment);
            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            return Ok(response);
        }
        [HttpPost("payment-callback")]
        public async Task<ActionResult> Callback([FromBody] dynamic responsePayment)
        {
            //Trace.WriteLine("oauhcouahoecihoie",responsePayment);
            Console.WriteLine("ascasjcpiojsp ",responsePayment);
            
            return Ok(responsePayment);
        }
    }
    
}

public class FormData
{
    public string? store_id { get; set; }
    public string? signature_key { get; set; }
    public string? tran_id { get; set; }
    public string? success_url { get; set; }
    public string? fail_url { get; set; }
    public string? cancel_url { get; set; }
    public int? amount { get; set; }
    public string? currency { get; set; }
    public string? desc { get; set; }
    public string? cus_name { get; set; }
    public string? cus_email { get; set; }
    public string? cus_add1 { get; set; }
    public string? cus_add2 { get; set; }    
    public string? cus_city { get; set; }
    public string? cus_state { get; set; }
    public string? cus_country { get; set; }
    public string? cus_phone { get; set; }
    public string? opt_a { get; set; }   
    public string? opt_b { get; set; }
    public string? opt_c { get; set; }
    public string? opt_d { get; set; }
    public string type { get; set; }
}