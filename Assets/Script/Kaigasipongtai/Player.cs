using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    public Rigidbody2D rb2D;
    public float force = 1f;
    private Vector2 input;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;
    public Vector3 MousePosition;
    public GameObject ArrowIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void ProccessInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        input = input.normalized;
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
        
    }

    public void ShootHook()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        ProccessInput();
        Animate();
        if (input.x < 0 && !facingLeft || input.x > 0 && facingLeft)
        {
            Flip();
        }
        
        
    }
}
