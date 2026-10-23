using UnityEngine;
using System.Collections;
using TMPro;

public class GhostManagers : MonoBehaviour
{
    public Animator[] ghosts;
    public TMP_Text scaredTimer;
    public AudioSource musicSource;
    public AudioClip normalClip;
    public AudioClip scaredClip;
    
    private Coroutine scaredRoutine;

    IEnumerator ScaredRoutine()
    {
        SetAll("Scared");
        Playmusic(scaredClip);
        scaredTimer.gameObject.SetActive(true);

        float timeLeft = 10f;
        bool recovering = false;

        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            scaredTimer.text = Mathf.CeilToInt(timeLeft).ToString();

            if (!recovering && timeLeft <= 3f)
            {
                recovering = true;
                SetAll("Recovery");
            }
            yield return null;
        }
        SetAll("Normal");
        Playmusic(normalClip);
        scaredTimer.gameObject.SetActive(false);
        scaredRoutine = null;
    }

    void SetAll(string statename)
    {
        foreach (Animator ghost in ghosts)
        {
            ghost.Play(statename);
        }
    }

    void Playmusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void StartScared()
    {
        if (scaredRoutine != null)
        {
            StopCoroutine(scaredRoutine);
        }

        scaredRoutine = StartCoroutine(ScaredRoutine());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
