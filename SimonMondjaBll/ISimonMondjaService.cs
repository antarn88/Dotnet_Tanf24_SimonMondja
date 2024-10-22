namespace SimonMondjaBll
{
    public interface ISimonMondjaService
    {
        IEnumerable<int> Numbers { get; }
        int CorrectGuesses { get; }

        (bool Correct, int? NextNumber) Guess(int number);

        int NewGame();

        void increaseCorrectGuesses();
        void clearCorrectGuesses();
    }
}
