using UnityEngine;
using System.Collections;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public TMP_Text countdownText;

    public GameObject block;

    public static bool gameRunning = false;
    
    public TMP_Text TimerText;
    private float timer = 0;

    IEnumerator Countdown()
    {
        gameRunning = false;
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
        TimerText.text = string.Format("{00:00}:{1:00}:{2:00}", minutes, seconds, hundredths);;

    }
}
