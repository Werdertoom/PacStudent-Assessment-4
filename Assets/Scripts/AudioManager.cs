using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Intro;
    public AudioSource ghostNormal;
   

    public float duration = 3.0f;
    
    public void introMusic()
       {
           Intro.Play();
       }
    public void normalMusic()
    {
            Intro.Stop();
        ghostNormal.Play();
    }
    
    void Start()
    {
    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
