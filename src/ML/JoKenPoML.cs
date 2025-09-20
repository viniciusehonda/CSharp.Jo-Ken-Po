using Microsoft.ML;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Jo_Ken_Po;

public class JoKenPoML
{
    public readonly MLContext mLContext = new MLContext();
    private ITransformer? model;

    public void TrainAndSaveModel()
    {
        var trainingData = new List<GameData>
        {
            new GameData { PlayerLastMove1 = 1, PlayerLastMove2 = 2, PlayerLastMove3 = 3, PlayerNextMove = 1 },
            new GameData { PlayerLastMove1 = 1, PlayerLastMove2 = 1, PlayerLastMove3 = 2, PlayerNextMove = 2 },
            new GameData { PlayerLastMove1 = 2, PlayerLastMove2 = 3, PlayerLastMove3 = 1, PlayerNextMove = 3 },
        };

        var dataView = mLContext.Data.LoadFromEnumerable(trainingData);

        var pipeline = mLContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Label", outputColumnName: "Label")
            .Append(mLContext.Transforms.Concatenate("Features", "PlayerLastMove1", "PlayerLastMove2", "PlayerLastMove3"))
            .Append(mLContext.MulticlassClassification.Trainers.LightGbm(labelColumnName: "Label", featureColumnName: "Features"))
            .Append(mLContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        model = pipeline.Fit(dataView);

        mLContext.Model.Save(model, dataView.Schema, "jankenpo_model.zip");
        Console.WriteLine("Model trained and saved successfully!");
    }
}