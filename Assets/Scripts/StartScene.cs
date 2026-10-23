using TMPro;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       int highscore = PlayerPrefs.GetInt("HighScore", 0);
       highScoreText.text = highscore.ToString("D6");
       
       float bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
       int minutes = (int)(bestTime / 60f);
       int seconds = (int)(bestTime % 60f);
       int hundredths = (int)((bestTime * 100f) % 100f);
       timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
