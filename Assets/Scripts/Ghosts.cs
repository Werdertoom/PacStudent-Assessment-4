using UnityEngine;
using System.Collections;

public class Ghosts : MonoBehaviour
{
    public Animator animator;
    public string state = "Normal";
    public GhostManagers manager;

    private Vector3 startPos;

    IEnumerator Revive()
    {
        yield return new WaitForSeconds(3f);
        SetState(GhostManagers.currentState);
        manager.UpdateMusic();
    }
    public void Eaten()
    {
        SetState("Dead");
        manager.UpdateMusic();
        StartCoroutine(Revive());
    }
    public void ResetGhosts()
    {
        StopAllCoroutines();
        transform.position = startPos;
        SetState("Normal");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    public void SetState(string newState)
    {
        state = newState;
        animator.Play(newState);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
