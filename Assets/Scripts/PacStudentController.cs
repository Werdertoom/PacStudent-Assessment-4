using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Animator animator;
    private Tweener tweener;
    private float speed = 5.0f;
    private AudioSource audiosource;
    private float tileSpacingX = 0.263f;
    private float tileSpacingY = 0.3f;
    public int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,8},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    private Vector3 currentInput;

    private Vector3 lastInput;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator =  GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        currentInput = Vector3.right;
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
        
        if (!tweener.TweenExists(transform) && lastInput != Vector3.zero)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPos =  currentPosition + new Vector3(
                         lastInput.x * tileSpacingX,
                         lastInput.y * tileSpacingY,
                         0
                         );
            float duration = 1f / speed;
            
            
            tweener.AddTween(transform, transform.position, targetPos, duration);
        }

        
    }
    
}
