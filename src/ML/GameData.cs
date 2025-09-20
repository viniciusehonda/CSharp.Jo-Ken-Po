using Microsoft.ML.Data;

namespace CSharp.Jo_Ken_Po;

public class GameData
{
    [LoadColumn(0)]
    public float PlayerLastMove1;
    [LoadColumn(1)]
    public float PlayerLastMove2;
    [LoadColumn(2)]
    public float PlayerLastMove3;
    [LoadColumn(3)]
    [ColumnName("Label")]
    public float PlayerNextMove;
}