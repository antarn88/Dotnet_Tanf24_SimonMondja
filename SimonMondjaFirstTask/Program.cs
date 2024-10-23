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

            app.MapGet("/guess", (HttpContext context) =>
            {
                var simonMondjaService = context.RequestServices.GetService<ISimonMondjaService>();
                int randomNumber = simonMondjaService!.NewGame();

                return Results.Ok(randomNumber);
            });

            app.MapGet("/guess/{givenNumber:int}", (HttpContext context, int givenNumber) =>
            {
                var simonMondjaService = context.RequestServices.GetService<ISimonMondjaService>();
                var (success, nextNumber) = simonMondjaService!.Guess(givenNumber);

                if (success)
                {
                    if (nextNumber.HasValue)
                    {
                        return Results.Text($"Great! The next number is {nextNumber}.");
                    }
                    else
                    {
                        return Results.Text($"Number {simonMondjaService.CorrectGuesses} is correct!");
                    }
                }
                else
                {
                    if (nextNumber.HasValue)
                    {
                        return Results.Text($"Oh no! The correct number was {nextNumber}.");
                    }
                    else
                    {
                        return Results.Text("Oh no! The game has been reset.");
                    }
                }
            });

            app.Run();
        }
    }
}
