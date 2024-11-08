using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    [SerializeField] private GameObject Spawner;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject GameinfoPanel;
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private float gameDuration;

    private int lives = 3;
    private int score;
    private float currentTime;
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (currentTime > 0f && !gameEnded)
        {
            currentTime -= Time.deltaTime;
            Timer();
        }
        else
        {
            gameOver();
        }
    }

    private void gameOver()
    {
        GameOverPanel.SetActive(true);
        Spawner.SetActive(false);
        gameEnded = true;
        Invoke("QuitGame", 5f);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }
    }

    public void NewGame()
    {
        MainMenuPanel.SetActive(false);
        GameinfoPanel.SetActive(true);
        Spawner.SetActive(true);
        currentTime = gameDuration;
        Timer();
    }

    private void Timer()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = "Timer: " + timerString;
    }

    internal void Explode()
    {
        lives--;
        if (lives == 2) livesText.text = "X";
        if (lives == 1) livesText.text = "XX";
        if (lives == 0)
        {
            livesText.text = "XXX";
            gameOver();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}