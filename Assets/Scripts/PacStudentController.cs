using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Animator animator;
    private Tweener tweener;
    private float speed = 5.0f;
    private AudioSource audiosource;
    private float tileSpacingX = 0.263f;
    private float tileSpacingY = 0.3f;

    private Vector3 currentInput;

    private Vector3 lastInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator =  GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3.right;
        }
        
        if (!tweener.TweenExists(transform))
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPos =  currentPosition + new Vector3(
                         lastInput.x * tileSpacingX,
                         lastInput.y * tileSpacingY,
                         0
                         );
            float duration = Vector3.Distance(transform.position, targetPos) / speed;
            
            
            tweener.AddTween(transform, transform.position, targetPos, duration);
        }
    }
    
}
