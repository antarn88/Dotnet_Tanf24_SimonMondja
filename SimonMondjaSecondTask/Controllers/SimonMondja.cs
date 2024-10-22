using Microsoft.AspNetCore.Mvc;
using SimonMondjaBll;

namespace SimonMondjaSecondTask.Controllers
{
    [ApiController]
    [Route("api/guess")]
    public class SimonMondja : ControllerBase
    {
        private ISimonMondjaService _simonMondjaService;

        public SimonMondja(ISimonMondjaService simonMondjaService)
        {
            _simonMondjaService = simonMondjaService;
        }

        [HttpGet]
        public async Task<IActionResult> NewGame()
        {
            return Ok(_simonMondjaService.NewGame());
        }

        [HttpGet("{givenNumber}")]
        public async Task<IActionResult> GuessNumber([FromRoute] string givenNumber)
        {
            if (int.TryParse(givenNumber, out int number))
            {
                (bool success, int? next) = _simonMondjaService.Guess(number);

                if (success)
                {
                    if (next.HasValue)
                    {
                        return Ok($"Great! The next number is {next}.");
                    }
                    else
                    {
                        return Ok($"Number {_simonMondjaService.CorrectGuesses} is correct!");
                    }
                }
                else
                {
                    if (next.HasValue)
                    {
                        return Ok($"Oh no! The correct number was {next}.");
                    }
                    else
                    {
                        return Ok("Oh no! The game has been reset.");
                    }
                }
            }

            return BadRequest("The number format is invalid.");
        }
    }
}
