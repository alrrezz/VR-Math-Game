using UnityEngine;
using TMPro;

public class ScoreTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        GameSession session = FindObjectOfType<GameSession>();
        SetScoreText(session.GetScore());
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
