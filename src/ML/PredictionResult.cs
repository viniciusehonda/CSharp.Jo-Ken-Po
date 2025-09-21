using Microsoft.ML.Data;

namespace CSharp.Jo_Ken_Po;

public class PredictionResult
{
    [ColumnName("PredictedLabel")]
    public float PredictedNextMove;

    [ColumnName("Score")]
    public float[] Scores;
}