using CSharp.Jo_Ken_Po;

Console.WriteLine("Welcome to the CSharp Jo Ken Po");
Random rnd = new Random();

while (true)
{
    Console.WriteLine("Please choose an option:");
    Console.WriteLine("1 - Rock");
    Console.WriteLine("2 - Paper");
    Console.WriteLine("3 - Scissors");
    Console.WriteLine("4 - Exit");

    string? choice = Console.ReadLine();

    if (Enum.TryParse(choice, out JoKenPoOptions playerOption))
    {
        if (!Enum.IsDefined(playerOption))
        {
            break;
        }

        var opponentOption = (JoKenPoOptions)rnd.Next(1, 3);

        Console.WriteLine("Rock, Paper, Scissors !");

        if (playerOption == opponentOption)
        {
            Console.WriteLine("It's a draw !");
            continue;
        }

        if (playerOption == JoKenPoOptions.Rock && opponentOption == JoKenPoOptions.Scissors
        || playerOption == JoKenPoOptions.Paper && opponentOption == JoKenPoOptions.Rock
        || playerOption == JoKenPoOptions.Scissors && opponentOption == JoKenPoOptions.Paper)
        {
            Console.WriteLine("You Won the game !");
            continue;
        }

        Console.WriteLine("You Lost the game !");
    }
}