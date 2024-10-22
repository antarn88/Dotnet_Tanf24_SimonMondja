using SimonMondjaBll;

namespace SimonMondjaFirstTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ISimonMondjaService, SimonMondjaService>();

            var app = builder.Build();

            app.MapGet("/guess", (ISimonMondjaService simonMondjaService) =>
            {
                int randomNumber = simonMondjaService.NewGame();
                return Results.Ok(randomNumber);
            });

            app.MapGet("/guess/{givenNumber:int}", (int givenNumber, ISimonMondjaService simonMondjaService) =>
            {
                var (success, nextNumber) = simonMondjaService.Guess(givenNumber);

                if (success)
                {
                    if (nextNumber.HasValue)
                    {
                        return Results.Ok($"Great! The next number is {nextNumber}.");
                    }
                    else
                    {
                        return Results.Ok($"Number {simonMondjaService.CorrectGuesses} is correct!");
                    }
                }
                else
                {
                    if (nextNumber.HasValue)
                    {
                        return Results.Ok($"Oh no! The correct number was {nextNumber}.");
                    }
                    else
                    {
                        return Results.Ok("Oh no! The game has been reset.");
                    }
                }
            });

            app.Run();
        }
    }
}
