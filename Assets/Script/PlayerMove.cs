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
    Vector3 _nowSpeed;//���݂̑��x�x�N�g�����擾���A���Ԃ������邱�Ƃŏu�Ԉړ��������\��

    float _move;
    bool _isMoving = false;
    bool _isJumping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _bounds = GetComponent<Renderer>().bounds.extents;//�L�����̃T�C�Y�̔���
    }

    /// <summary>
    /// Time.timeScale�ɉe������Ȃ�
    /// </summary>
    void Update()
    {
        //�ړ�
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

        //�W�����v
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
    /// Time.timeScale�ɉe�������
    /// </summary>
    private void FixedUpdate()
    {
        //�ړ�
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
