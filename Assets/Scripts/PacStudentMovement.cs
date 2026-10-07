using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PacStudentMovement : MonoBehaviour
{
    private Animator animator;
    private Tweener tweener;
    private float speed = 1.0f;
    private int targetPos = 0;
    private AudioSource audiosource;

    private Vector3 topLeft = new Vector3(0.14f, -0.2f, 0f);
    private Vector3 topRight = new Vector3(1.452f, -0.2f, 0f);
    private Vector3 bottomRight  = new Vector3(1.452f, -1.4f, 0f);
    private Vector3 bottomLeft  = new Vector3(0.14f, -1.4f, 0f);
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator =  GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
        float distance = Vector3.Distance(transform.position, topLeft) / speed;
        tweener.AddTween(transform, transform.position, topLeft, distance);
        animator.SetInteger("Direction", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!tweener.TweenExists(transform))
        {
            if (targetPos == 0)
            {
                float duration = Vector3.Distance(transform.position, topRight) / speed;
                tweener.AddTween(transform, transform.position, topRight, duration);
                animator.SetInteger("Direction", 0);
                
                targetPos = 1;
                
            }

            else if (targetPos == 1)
            {
                float length = Vector3.Distance(transform.position, bottomRight) / speed;
                tweener.AddTween(transform, transform.position, bottomRight, length);
                animator.SetInteger("Direction", 3);
                targetPos = 2;
            }

            else if (targetPos == 2)
            {
                float longer = Vector3.Distance(transform.position, bottomLeft) / speed;
                tweener.AddTween(transform, transform.position, bottomLeft, longer);
                animator.SetInteger("Direction", 2);
                targetPos = 3;
            }
            else if (targetPos == 3)
            {
                float distance = Vector3.Distance(transform.position, topLeft) / speed;
                tweener.AddTween(transform, transform.position, topLeft, distance);
                animator.SetInteger("Direction", 1);
                targetPos = 0;
            }
        }
    }
}
