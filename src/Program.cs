using CSharp.Jo_Ken_Po;

Console.WriteLine("Welcome to the CSharp Jo Ken Po");
Random rnd = new Random();

int playerScore = 0;
int opponentScore = 0;

while (true)
{
    if (playerScore > 0 || opponentScore > 0)
    {
        Console.WriteLine("Current Score:");
        Console.WriteLine(string.Format("Player: {0} x {1} Opponent", playerScore, opponentScore));
    }

    Console.WriteLine("Please choose an option:");
    Console.WriteLine("1 - Rock");
    Console.WriteLine("2 - Paper");
    Console.WriteLine("3 - Scissors");
    Console.WriteLine("4 - Exit");

    string? choice = Console.ReadLine();
    Console.Clear();

    if (choice == "4")
    {
        if (playerScore > opponentScore)
        {
            Console.WriteLine("You won the game !");
        }
        else if (playerScore == opponentScore)
        {
            Console.WriteLine("The game ended in a draw !");
        }
        else
        {
            Console.WriteLine("You lost the game !");
        }

        break;
    }

    if (Enum.TryParse(choice, out JoKenPoOptions playerOption)
        && Enum.IsDefined(playerOption))
    {
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
            Console.WriteLine("You Won !");
            playerScore += 1;
            continue;
        }

        Console.WriteLine("You Lost !");
        1 += 1;
        continue;
    }

    Console.WriteLine("Invalid input. Please enter a number.");
}