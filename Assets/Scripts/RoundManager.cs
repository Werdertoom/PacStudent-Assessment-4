using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public TMP_Text countdownText;

    public GameObject block;

    public static bool gameRunning = false;
    
    public TMP_Text TimerText;
    public TMP_Text gameOverText;
    private float timer = 0;
    private bool isGameover = false;
    public Button exitButton;
    public AudioManager  audioManager;

    IEnumerator GameOverRoutine(int finalScore)
    {
        gameRunning = false;
        block.SetActive(true);
        exitButton.interactable = false;
        gameOverText.gameObject.SetActive(true);
        
        int savedScore = PlayerPrefs.GetInt("HighScore", 0);
        float savedTime =  PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (finalScore > savedScore || (finalScore == savedScore && timer < savedTime))
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
            PlayerPrefs.SetFloat("BestTime", timer);
            PlayerPrefs.Save();
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("StartScene");
    }

    public void GameOver(int finalScore)
    {
        if (isGameover) return;
        isGameover = true;
        StartCoroutine(GameOverRoutine(finalScore));
    }
    IEnumerator Countdown()
    {
        gameRunning = false;
        audioManager.introMusic();
        block.SetActive(true);
        countdownText.gameObject.SetActive(true);
        
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
        block.SetActive(false);
        gameRunning = true;
        audioManager.normalMusic();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Countdown());
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!gameRunning) return;
        
        timer += Time.deltaTime;
        int minutes = (int)(timer / 60f);
        int seconds = (int)(timer % 60f);
        int hundredths = (int)((timer * 100f) % 100f);
        TimerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);

    }
}
