using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [Header("Score & Lives")]
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;

    [Header("UI")]
    [SerializeField] private Image[] heartImages;

    LevelManager levelManager;

    void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() => score;

    public void PlusScore(int value)
    {
        score += value;
    }

    public void LoseLife()
    {
        if (lives <= 0) return;

        lives--;
        if (heartImages != null && lives < heartImages.Length)
        {
            heartImages[lives].enabled = false;
        }

        if (lives == 0)
        {
            levelManager.LoadGameOverMenu();
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
