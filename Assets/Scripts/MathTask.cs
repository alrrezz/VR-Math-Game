using UnityEngine;

[System.Serializable]
public class MathTask
{
    [SerializeField] private string expression;
    [SerializeField] private bool isCorrect;
    [SerializeField] private int targetNumber;
    [SerializeField] private int scoreForCorrectAnswered;

    public MathTask(string expr, bool isCorrect, int target, int correctAnswerScore)
    {
        this.expression = expr;
        this.isCorrect = isCorrect;
        this.targetNumber = target;
        this.scoreForCorrectAnswered = correctAnswerScore;
    }

    public string GetExpression() { return expression; }
    public bool IsCorrect() { return isCorrect; }
    public int GetTargetNumber() { return targetNumber; }
    public int GetScoreForCorrectAnswer() { return scoreForCorrectAnswered; }
}
