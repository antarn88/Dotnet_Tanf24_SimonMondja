namespace SimonMondjaBll
{
    public class SimonMondjaService : ISimonMondjaService
    {
        public Random Random { get; }

        List<int> Numbers = new List<int>();

        int CorrectGuesses = 0;

        public SimonMondjaService()
        {
            Random = new Random();
        }

        public void increaseCorrectGuesses()
        {
            CorrectGuesses++;
        }

        public void clearCorrectGuesses()
        {
            CorrectGuesses = 0;
        }

        int ISimonMondjaService.CorrectGuesses => CorrectGuesses;
        IEnumerable<int> ISimonMondjaService.Numbers => Numbers;

        (bool Correct, int? NextNumber) ISimonMondjaService.Guess(int number)
        {
            if (CorrectGuesses < Numbers.Count && Numbers.Count > 0 && Numbers[CorrectGuesses] == number)
            {
                if (CorrectGuesses == Numbers.Count - 1)
                {
                    int nextNumber = Random.Next(1, 99);
                    Numbers.Add(nextNumber);
                    clearCorrectGuesses();

                    return (true, nextNumber);
                }

                increaseCorrectGuesses();

                return (true, null);
            }
            else
            {
                if (Numbers.Count > 0 && CorrectGuesses < Numbers.Count)
                {
                    int correctNextNumber = Numbers[CorrectGuesses];
                    Numbers.Clear();
                    clearCorrectGuesses();

                    return (false, correctNextNumber);
                }
                else
                {
                    return (false, null);
                }
            }
        }

        int ISimonMondjaService.NewGame()
        {
            int randomNumber = Random.Next(1, 99);
            Numbers.Clear();
            Numbers.Add(randomNumber);
            clearCorrectGuesses();
            return randomNumber;
        }
    }
}
