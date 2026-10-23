using UnityEngine;
using System.Collections;
using TMPro;

public class GhostManagers : MonoBehaviour
{
    public Ghosts[] ghosts;
    public TMP_Text scaredTimer;
    public AudioSource musicSource;
    public AudioClip normalClip;
    public AudioClip scaredClip;

    public static string currentState = "Normal"; // what a revived ghost should become
    private Coroutine routine;

    public void StartScared()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ScaredRoutine());
    }

    IEnumerator ScaredRoutine()
    {
        SetAll("Scared", scaredClip);
        scaredTimer.gameObject.SetActive(true);

        float timeLeft = 10f;
        while (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            scaredTimer.text = Mathf.CeilToInt(timeLeft).ToString();
            if (timeLeft <= 3f && currentState != "Recovery")
            {
                SetAll("Recovery", scaredClip);
            }

            yield return null;
        }

        SetAll("Normal", normalClip);
        scaredTimer.gameObject.SetActive(false);
    }

    public AudioClip deadClip;

    public void UpdateMusic()
    {
        bool anyDead = false;
        foreach (Ghosts g in ghosts)
        {
            if (g.state == "Dead")
            {
                anyDead = true;
            }
        }

        AudioClip wanted = normalClip;
        if (anyDead)
        {
            wanted = deadClip;
        }
        else if (currentState == "Scared"|| currentState == "Recovery")
        {
            wanted = scaredClip;
        }

        if (musicSource.clip == wanted && musicSource.isPlaying) return;
        musicSource.clip = wanted;
        musicSource.loop = true;
        musicSource.Play();

    } 

    void SetAll(string state, AudioClip music)
    {
        currentState = state;
        foreach (Ghosts g in ghosts)
        {
            if (g.state != "Dead") g.SetState(state);   
        }
       UpdateMusic();
    }

    public void ResetAll()
    {
        StopAllCoroutines();
        scaredTimer.gameObject.SetActive(false);
        currentState = "Normal";
        foreach (Ghosts g in ghosts) g.ResetGhosts();
        SetAll("Normal", normalClip);
    }
}