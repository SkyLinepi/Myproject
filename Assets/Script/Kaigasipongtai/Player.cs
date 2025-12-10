using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    public Rigidbody2D rb2D;
    public float force;
    public float ChargeUpMoveSpeed = 5f;
    public float collected;
    private Vector2 input;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;
    public Vector3 MousePosition;
    public GameObject ArrowIndicator;
    public GameObject Harpoon;
    public float harpoonSpeed = 10f;
    public Transform shootPoint;
    public float reloadTime;
    private float reloadTimer;
    private bool isReloading = false;
    public float oxygen;
    void Start()
    {
        _animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        float moveXInput = Input.GetAxis("Horizontal");
        float moveYInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(moveXInput, moveYInput).normalized;
        rb2D.linearVelocity = direction.normalized * force;
        bool CheckMove = direction.sqrMagnitude > 0.01f;
        _animator.SetBool("isMoveing", CheckMove);
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
    public float hideDelay = 0.6f;
    public float hideTimer = 0f;
    void ProccessInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // store last move
        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
            lastMoveDirection = input;

        input = new Vector2(moveX, moveY).normalized;

        // Hold right-click to charge
        if (Input.GetMouseButton(1))
        {
            preparedHook();
            hideTimer = hideDelay;

            // Fire once on LMB click
            if (Input.GetMouseButtonDown(0))
                ShootHook();
        }
        else
        {
            force = collected;
            _animator.SetBool("ImmaShoot", false);
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
                ArrowIndicator.SetActive(false);
        }
    }


    void Animate()
    {
        _animator.SetFloat("MoveX", input.x);
        _animator.SetFloat("MoveY", input.y);
        _animator.SetFloat("MoveMagnitude", input.sqrMagnitude);
        _animator.SetFloat("LastMoveX", lastMoveDirection.x);
        _animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    public void preparedHook()
    {
        force = ChargeUpMoveSpeed;
        ArrowIndicator.SetActive(true);
        _animator.SetBool("ImmaShoot", true);
    }

    public void ShootHook()
    {
        if (isReloading) return;     // prevent shooting while reloading

        force = collected;
        _animator.SetBool("ImmaShoot", false);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector3 dir = (mouseWorld - shootPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject harpoon = Instantiate(Harpoon, shootPoint.position, Quaternion.Euler(0, 0, angle));
        Rigidbody2D rb = harpoon.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * harpoonSpeed;

        StartReload();
    }

    void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadTime;
    }

    void UpdateReload()
    {
        if (!isReloading) return;

        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0f)
            isReloading = false;
    }

    void FixedUpdate()
    {
        Move();
        ProccessInput();
        Animate();
        UpdateReload();
        if (input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }


    }
    void OnDisable()
    {
        if (rb2D != null)
            rb2D.linearVelocity = Vector2.zero;
    }
}
