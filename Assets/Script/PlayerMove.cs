using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    //Inspector
    [Header("GroundLayer")]
    [SerializeField] LayerMask _groundLayer;

    [Header("Status")]
    [SerializeField] float _run;
    [SerializeField] float _dash;
    [SerializeField] float _jump;

    //field
    Rigidbody2D _rb2d;

    RaycastHit2D _hitGround;

    float _move;
    bool _isMoving = false;
    bool _isJumping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Time.timeScale‚É‰e‹¿‚³‚ê‚È‚¢
    /// </summary>
    void Update()
    {
        //ˆÚ“®
        _move = Input.GetAxisRaw("Horizontal");
        if (_move != 0)
        {
            transform.localScale = new Vector3(_move, 1, 1);
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }

        //ƒWƒƒƒ“ƒv
        Debug.DrawLine(transform.position, transform.position + Vector3.down);
        _hitGround = Physics2D.Linecast(transform.position, transform.position + Vector3.down, _groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && _hitGround)
        {
            _isJumping = true;
        }
        else if(_hitGround)
        {
            _isJumping = false;
        }
    }

    /// <summary>
    /// Time.timeScale‚É‰e‹¿‚³‚ê‚é
    /// </summary>
    private void FixedUpdate()
    {
        //ˆÚ“®
        if (_isMoving)
        {
            _rb2d.linearVelocityX = _run * _move;
        }
        else
        {
            _rb2d.linearVelocityX = 0.0f;
        }


    }
}
