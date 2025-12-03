using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScore : MonoBehaviour
{
    private GameSession gameSystem;
    private ScoreTextUI scoreTextUI;
    private int currentScore;

    private void Start()
    {
        gameSystem = FindObjectOfType<GameSession>();
        scoreTextUI = FindObjectOfType<ScoreTextUI>();
    }

    public void AddScoreForCorrectAnswer(MathCube mathCube)
    {
        gameSystem.PlusScore(mathCube.mathTask.GetScoreForCorrectAnswer());
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentScore = gameSystem.GetScore();
        scoreTextUI.SetScoreText(currentScore);
    }
}
