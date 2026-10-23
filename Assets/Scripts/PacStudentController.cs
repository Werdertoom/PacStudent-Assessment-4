        using System.Collections;
        using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    private Animator animator;
    private Tweener tweener;
    private float speed = 5.0f;
    [SerializeField] private AudioClip eatingAudio;
    [SerializeField] private AudioClip walkingAudio;
    private AudioSource audiosource;
    private AudioClip currentAudioClip;
    public AudioClip cherryAudio;
    private float tileSpacingX = 0.3f;
    private float tileSpacingY = 0.3f;
    public ParticleSystem dustParticles;
    public ParticleSystem wallParticles;
    private int score = 0;
    public TMP_Text scoreText;
    public Transform leftTeleport;
    public Transform rightTeleport;
    public bool hitWall = false;
    public AudioClip wallAudio;
    public GameObject[] lifeIndicator;
    public AudioClip deathClip;
    public ParticleSystem deathParticles;
    private int lives = 3;
    private bool isDead = false;
    private Vector3 initialPosition;
    public RoundManager roundManager;
    private int pelletsLeft;
    public GhostManagers ghostManager;

    public int[,] quadrantMap =
    {
        { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7 },
        { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 4 },
        { 2, 6, 4, 0, 0, 4, 5, 4, 0, 0, 0, 4, 5, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 4, 4, 4, 3, 5, 3 },
        { 2, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 },
        { 2, 5, 3, 4, 4, 3, 5, 3, 3, 5, 3, 4, 4, 4 },
        { 2, 5, 3, 4, 4, 3, 5, 4, 4, 5, 3, 4, 4, 3 },
        { 2, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 4 },
        { 1, 2, 2, 2, 2, 1, 5, 4, 3, 4, 4, 3, 0, 4 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 3, 4, 4, 3, 0, 3 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 2, 5, 4, 4, 0, 3, 4, 4, 8 },
        { 2, 2, 2, 2, 2, 1, 5, 3, 3, 0, 4, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 4, 0, 0, 0 },
    };

    private int[,] levelMap = new int[29, 28];

    void MapBuild()
    {
        for (int r = 0; r < 15; r++)
        {
            for (int c = 0; c < 14; c++)
            {
                int tile = quadrantMap[r, c];
                levelMap[r, c] = tile;
                levelMap[r, 27 - c] = tile;
                levelMap[28 - r, c] = tile;
                levelMap[28 - r, 27 - c] = tile;

            }
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        audiosource.Stop();
        dustParticles.Stop();
        lives--;
        if (lives >= 0 && lives < lifeIndicator.Length)
        {
            lifeIndicator[lives].SetActive(false);
        }
        deathParticles.Play();
        audiosource.PlayOneShot(deathClip);
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.5f);
        if (lives <= 0)
        {
            roundManager.GameOver(score);
            yield break;
        }
        transform.position = initialPosition;
        row = 1;
        column = 1;
        currentInput = Vector3.zero;
        lastInput = Vector3.zero;
        animator.SetBool("Dead", false);
        animator.SetInteger("Direction", 0);
        animator.SetBool("Ismoving", false);
        isDead = false;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pellet"))
            {
                score += 10;
                scoreText.text = score.ToString("D6");
                Destroy(other.gameObject);
                pelletsLeft--;
                if (pelletsLeft <= 0)
                {
                    roundManager.GameOver(score);
                }
            }

        if (other.CompareTag("PowerPellet"))
        {
            score += 50;
            scoreText.text = score.ToString("D6");
            ghostManager.StartScared(); 
            Destroy(other.gameObject);
            pelletsLeft--;
            if (pelletsLeft <= 0)
            {
                roundManager.GameOver(score);
            }
        }

        if (other.CompareTag("Cherry"))
        {
            score += 100;
            scoreText.text = score.ToString("D6");
            audiosource.PlayOneShot(cherryAudio);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Ghost") && !isDead)
        {
            StartCoroutine(Die());
        }
    }
    private Vector3 currentInput;

    private Vector3 lastInput;

    bool IsWalkable(int row, int column)
    {
        int tile = levelMap[row, column];
        if (tile == 1 || tile == 2 || tile == 3 || tile == 4 || tile == 7 || tile == 8)
        {
            return false;
        }

        return true;
    }

    private int row = 1;
    private int column = 1;

    void PlayStepSound()
    {
        int tile = levelMap[row, column];
        AudioClip clip;
        if (tile == 5 || tile == 6)
        {
            clip = eatingAudio;
        }
        else
        {
            clip = walkingAudio;
        }

        audiosource.clip = clip;
        audiosource.Play();

    }
    bool canWalk(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return false;
        }
        int nextrow = row;
        int nextcolumn = column;
        if (direction == Vector3.up)
        {
            nextrow = row - 1;
        }
        if (direction == Vector3.down)
        {
            nextrow = row + 1;
        }
        if (direction == Vector3.right)
        {
            nextcolumn = column + 1;
        }

        if (direction == Vector3.left)
        {
            nextcolumn = column - 1;
        }
        if (nextrow < 0 || nextrow >= 29)
        {
            return false;
        }

        if (nextcolumn < 0 || nextcolumn >= 28)
        {
            return false;
        }
        return IsWalkable(nextrow, nextcolumn);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator =  GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.playOnAwake = false;
        currentInput = Vector3.zero;
        animator.SetInteger("Direction", 0);
        MapBuild();
        initialPosition = transform.position;
        pelletsLeft = GameObject.FindGameObjectsWithTag("Pellet").Length +
                      GameObject.FindGameObjectsWithTag("PowerPellet").Length;
    }

    // Update is called once per frame
    void Update()
        {
            if (!RoundManager.gameRunning)
            {
                return;
            }

            if (isDead)
            {
                return;
            }
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
                if (row == 14 && column == 0)
                {
                    transform.position = rightTeleport.position;
                    column = 27;
                    currentInput = Vector3.left;
                }
                else if (row == 14 && column == 27)
                {
                    transform.position = leftTeleport.position;
                    column = 0;
                    currentInput = Vector3.right;
                }
                    
                if (canWalk(lastInput))
                {
                    currentInput = lastInput;
                }
                else if (!canWalk(currentInput))
                {
                    animator.SetBool("IsMoving", false);
                    
                    dustParticles.Stop();
                    
                    if (!hitWall && currentInput != Vector3.zero)
                    {
                        hitWall = true;
                        audiosource.Stop();
                        wallParticles.Play();
                        audiosource.PlayOneShot(wallAudio);
                    }

                    return;
                }
                Vector3 currentPosition = transform.position;
                Vector3 targetPos = currentPosition + new Vector3(
                    currentInput.x * tileSpacingX,
                    currentInput.y * tileSpacingY,
                    0
                );
                float duration = 1f / speed;

                tweener.AddTween(transform, transform.position, targetPos, duration);
                row -= (int)currentInput.y;
                column += (int)currentInput.x;
                PlayStepSound();
                hitWall = false;
                if (!dustParticles.isPlaying)
                {
                    dustParticles.Play();
                }
                if (currentInput == Vector3.up)
                {
                    animator.SetInteger("Direction", 1);
                }
                if (currentInput == Vector3.right)
                {
                    animator.SetInteger("Direction", 0);
                }
                if (currentInput == Vector3.left)
                {
                    animator.SetInteger("Direction", 2);
                }
                if (currentInput == Vector3.down)
                {
                    animator.SetInteger("Direction", 3);
                }
            }
            animator.SetBool("IsMoving", tweener.TweenExists(transform));
            
        }
    
}
