using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    public GameObject startPanel;
    public GameObject hudPanel;
    public GameObject gameOverPanel;

    [Header("Camera")]
    public cameraController cameraFollow;

    [Header("Game Over UI")]
    public TextMeshProUGUI finalScoreText;

    private static bool hasStartedBefore = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (!hasStartedBefore)
            ShowStartPanel();
        else
            StartGame();
    }

    public void ShowStartPanel()
    {
        Time.timeScale = 0f;

        startPanel.SetActive(true);
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        hasStartedBefore = true;

        startPanel.SetActive(false);
        hudPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        if (ScoreManager.instance != null)
            ScoreManager.instance.ResetScore();
    }

    public void GameOver()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);

        if (cameraFollow != null)
            cameraFollow.StopCamera();

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.StopScore();

            if (finalScoreText != null)
                finalScoreText.text =
                    "Score: " + ScoreManager.instance.FinalScore;
        }

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;

        startPanel.SetActive(false);
        hudPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        if (ScoreManager.instance != null)
            ScoreManager.instance.ResetScore();

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.ResetPlayer();

        cameraController cam = FindObjectOfType<cameraController>();
        if (cam != null)
            cam.ResetCamera();
    }
}
