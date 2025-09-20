using CSharp.Jo_Ken_Po;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the CSharp Jo Ken Po");
        var mlTrainer = new JoKenPoML();
        mlTrainer.TrainAndSaveModel();
        var mlContext = new MLContext();
        ITransformer loadedModel = mlContext.Model.Load("jankenpo_model.zip", out var modelSchema);
        var predictionEngine = mlContext.Model.CreatePredictionEngine<GameData, PredictionResult>(loadedModel);
        Random rnd = new Random();

        var playerHistory = new List<JoKenPoOptions>();
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
                var opponentOption = (JoKenPoOptions)rnd.Next(1, 4);

                if (playerHistory.Count >= 3)
                {
                    var input = new GameData
                    {
                        PlayerLastMove1 = (float)playerHistory[playerHistory.Count - 3],
                        PlayerLastMove2 = (float)playerHistory[playerHistory.Count - 2],
                        PlayerLastMove3 = (float)playerHistory[playerHistory.Count - 1]
                    };

                    var prediction = predictionEngine.Predict(input);
                    var predictedPlayerMove = (JoKenPoOptions)prediction.PredictedNextMove;

                    opponentOption = GetWinningMove(predictedPlayerMove);
                }

                playerHistory.Add(playerOption);

                Console.WriteLine("Rock, Paper, Scissors !");
                Thread.Sleep(1000);

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
                opponentScore += 1;
                continue;
            }

            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }

    public static JoKenPoOptions GetWinningMove(JoKenPoOptions predictedMove)
    {
        return predictedMove switch
        {
            JoKenPoOptions.Rock => JoKenPoOptions.Paper,
            JoKenPoOptions.Paper => JoKenPoOptions.Scissors,
            JoKenPoOptions.Scissors => JoKenPoOptions.Rock,
            _ => JoKenPoOptions.Rock,
        };
    }
}