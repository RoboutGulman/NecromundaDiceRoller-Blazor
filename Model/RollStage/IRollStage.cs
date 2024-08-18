namespace NecromundaDiceRoller.Model.RollStage;

public interface IRollStage
{
    public int GetStageResult(int previousStageResult, int[] rolls, int offset);
}