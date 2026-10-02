using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int FinalScore { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI speedText;

    public Transform player;

    private float starty;
    private bool isGameOver;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        instance = this;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        starty = player.position.y;
    }

    void Update()
    {
        if (isGameOver) return;

        float distance = player.position.y - starty;
        distance = Mathf.Max(0, distance);

        FinalScore = Mathf.FloorToInt(distance);
        if (scoreText != null)
            scoreText.text = "Score: " + FinalScore;

        if (speedText != null && playerRb != null)
        {
            float speed = playerRb.linearVelocity.magnitude; // السرعة الإجمالية
            speedText.text = speed.ToString("F1") + " X";
        }
    }

    public void StopScore()
    {
        isGameOver = true;
    }

    public void ResetScore()
    {
        isGameOver = false;
        FinalScore = 0;

        if (player != null)
        {
            starty = player.position.y;

            if (playerRb != null)
            {
                // 👇 إعادة تصفير السرعة
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }
        }

        if (scoreText != null)
            scoreText.text = "Score: 0";

        if (speedText != null)
            speedText.text = "0.0 X";
    }

    
}
