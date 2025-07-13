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
    Vector3 _bounds;
    Vector3 _layerLine;
    Vector3 _nowSpeed;//現在の速度ベクトルを取得し、時間をかけることで瞬間移動を実装予定

    float _move;
    bool _isMoving = false;
    bool _isJumping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bounds = GetComponent<Renderer>().bounds.extents;//キャラのサイズの半分
    }

    /// <summary>
    /// Time.timeScaleに影響されない
    /// </summary>
    void Update()
    {
        //移動
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

        //ジャンプ
        _layerLine = transform.position + Vector3.down * _bounds.y * 1.05f;
        Debug.DrawLine(_layerLine + Vector3.right * _bounds.x, _layerLine + Vector3.left * _bounds.x);
        _hitGround = Physics2D.Linecast(_layerLine + Vector3.right * _bounds.x, _layerLine + Vector3.left * _bounds.x, _groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && _hitGround)
        {
            _isJumping = true;
            _rb2d.AddForce(Vector3.up * _jump, ForceMode2D.Impulse);
        }
        else if (_hitGround)
        {
            _isJumping = false;
        }
    }

    /// <summary>
    /// Time.timeScaleに影響される
    /// </summary>
    private void FixedUpdate()
    {
        //移動
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
