using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Intro;
    public AudioSource Ghost_Normal;
   

    public float duration = 3.0f;
    
    public void introMusic()
       {
           Intro.Play();
           Invoke("StopAudio", duration);
       }
    private void StopAudio()
    {
        if (Intro.isPlaying)
            Intro.Stop();
        Ghost_Normal.Play();
    }
    
    void Start()
    {
        introMusic();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
