 using UnityEngine;

public class PlayerController : MonoBehaviour {
    GameController gc;

    //private Animator anim;

    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float climbSpeed;

    private float defaultRunSpeed = 8.0f;
    private float defaultClimbSpeed = 6.0f;

    [SerializeField]
    private float jumpForce;

    private float slowTimer;
    private float maxSlowTime;
    [SerializeField]
    private float slowMultiplier;
    [SerializeField]
    public int slowCount;

    public bool isDead;

    private bool isGrounded;
    private bool canClimb;
    public bool isSlow;

    // tutorial booleans
    public bool isRunning;
    public bool isJumping;
    public bool isClimbing;

    Rigidbody2D rb;
    Collider2D cldr;

    public LayerMask ground;
    public LayerMask climbableWall;

    void Start() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        runSpeed = defaultRunSpeed;
        climbSpeed = defaultClimbSpeed;

        jumpForce = 12.0f;

        maxSlowTime = 5.0f;
        slowMultiplier = 0.9f;
        slowCount = 0;

        rb = GetComponent<Rigidbody2D>();
        cldr = GetComponent<Collider2D>();

        isDead = false;

        isRunning = false;
        isJumping = false;
        isClimbing = false;

        // animations
        //anim = GetComponent<Animator>();
    }

    void Update() {
        CheckTutorialStatus();

        // check player status
        CheckGrounded();
        CheckClimbable();
        CheckIfSlow();

        // handle player movement
        Movement();

        // animations
        //anim.SetFloat("Speed", rb.velocity.x);
    }

    public void CheckTutorialStatus() {
        if (gc.isTutorial) {
            if (rb.velocity.x > 0) isRunning = true;
            else isRunning = false;

            if (rb.velocity.y > 0 && !isGrounded) isJumping = true;
            else isJumping = false;

            if (rb.velocity.y > 0 && canClimb) isClimbing = true;
            else isClimbing = false;
        }
    }

    //TODO make work for only tops of objects
    public void CheckGrounded() {
        isGrounded = Physics2D.IsTouchingLayers(cldr, ground) || Physics2D.IsTouchingLayers(cldr, climbableWall);
    }

    public void CheckClimbable() {
        canClimb = Physics2D.IsTouchingLayers(cldr, climbableWall);
    }

    public void CheckIfSlow() {
        if(isSlow) {
            runSpeed = defaultRunSpeed * (slowMultiplier / slowCount);
            climbSpeed = defaultClimbSpeed * (slowMultiplier / slowCount);
            slowTimer -= Time.deltaTime;
        }
        else {
            runSpeed = 8.0f;
            climbSpeed = 6.0f;
        }
        if (slowTimer <= 0) {
            isSlow = false;
            slowCount = 0;
        }
    }

    public void Movement() {
        // running
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, rb.velocity.y);

        // jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // wall climbing
        if (Input.GetKey(KeyCode.UpArrow) && canClimb) rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * climbSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject other = collision.gameObject;

        // collectables
        if(other.tag == "Collectable") {
            other.SetActive(false);

            // acorn
            if(other.name.Contains("Acorn")) {
                gc.score++;
            }
            // french fry
            if (other.name.Contains("French Fry")) {
                slowCount++;
                isSlow = true;
                slowTimer = maxSlowTime;
            }
        }

        // dying
        if (other.tag == "Killbox" || other.tag == "Pollution") {
            if (gc.isTutorial) {
                Debug.Log("do something here");
            }
            else {
                gameObject.SetActive(false);
                isDead = true;
            }
        }
    }
}
