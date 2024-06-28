using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using Services.Interfaces;


namespace ChatBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private readonly IKeywordService _keywordService;
        private readonly IScrapingService _scrapingService;

        public ChatBotController(IKeywordService keywordService, IScrapingService scrapingService)
        {
            _keywordService = keywordService;
            _scrapingService = scrapingService;
        }

        [HttpPost("get-answer")]
        public async Task<IActionResult> GetAnswer([FromBody] UserQuestion request)
        {
            var keywords = _keywordService.GetKeywords(request.Question);
            var query = string.Join(" ", keywords);
            var answer = await _scrapingService.GetFirstResultAsync(query);
            return Ok(new { answer });
        }
    }
}
