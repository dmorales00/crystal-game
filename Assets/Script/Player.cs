using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour

{
    // Variables

    // Bool
    private bool _onAir = false;
    private bool _pressedJump = false;
    private bool _moveRight = false;

    // Ints & Floats
    private int _currentRoad = 0;
    private float _jumpForce;
    private float _baseJump = 6f;
    private float _jumpDelay = 0;
    [SerializeField] private float _maxSpeed = 7;
    public int lifes = 3;
    public int money = 0;

    // Unity Vars
    private Rigidbody _rb;
    public static Player instance;
    private Animator _animator;



    // Body
    private void Awake()
    {
        instance = this;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        money = 0;
    }

    private void Update(){
        _jumpDelay += Time.deltaTime;

        //Inputs (On key press)
        if (Input.GetKeyDown(KeyCode.W)){
            if (_currentRoad < 2)
            {
                _currentRoad++;
                ZMovement();
            }
        }
        if (Input.GetKeyDown(KeyCode.S)){
            if (_currentRoad != 0)
            {
                _currentRoad--;
                ZMovement();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_onAir && _jumpDelay >= 1.25f){
            _animator.SetBool("isJumping", true);
            _rb.drag = 0;
            _pressedJump = true;
            AudioManager.instance.StartSFX(AudioManager.instance.allSFX[1]);
        }
        if (Input.GetKey(KeyCode.D) && !_onAir){
            _moveRight = true;
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isRunning", true);
        }

        // Inputs (On key release)

        if (Input.GetKeyUp(KeyCode.D)){
            _moveRight = false;
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isIdle", true);
        }
    }

    private void FixedUpdate(){
        if (_pressedJump){
            JumpPhysics();
        }

        if (_moveRight && _rb.velocity.x <= _maxSpeed){  // Speeds up the character. Limits the speed at "maxSpeed" value

            _rb.AddForce(Vector3.right, ForceMode.VelocityChange);
        }

        if (_onAir){ // Limit the speed when on air to a lower value. (Actual Values: 7 -> 5)
            _maxSpeed = 3;
        }
        else{
            _maxSpeed = 5;
        }
    }

    private void ZMovement() // Moves the character on the Z Axis.
    {
        _rb.MovePosition(new Vector3(this.gameObject.transform.position.x,
                                  this.gameObject.transform.position.y,
                                  PlayerPos.instance.allPos[_currentRoad].transform.position.z));
    }

    private void JumpPhysics()
    {
        if (_rb.velocity.x > 2.5f)
        {
            _rb.mass = 1.1f;

        }
        else
        {
            _rb.mass = 1f;
        }
        _rb.AddForce(transform.up * (_baseJump / _rb.mass), ForceMode.Impulse);
        _onAir = true;
        _jumpDelay = 0f;
        _pressedJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JumpTrigger"))
        {
            _rb.drag = 4;
            _onAir = false;
            _animator.SetBool("isJumping", false);
            _rb.mass = 1f;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Obstacle>().timer = 0;
            ParticlesPool.instance.particles[1].transform.position = other.transform.position;
            ParticlesPool.instance.particles[1].Play();
            PoolingSystem.instance.ReturnItem(other.gameObject, other.gameObject.transform.parent);
            lifes--;
            UIManager.instance.LifeUI();
            if (lifes == 0)
            {
                GameManager.instance.LifesSpent();
            }
            AudioManager.instance.StartSFX(AudioManager.instance.allSFX[3]);
        }

        if (other.gameObject.CompareTag("Collectible"))
        {
            other.GetComponent<Obstacle>().timer = 0;
            ParticlesPool.instance.particles[0].transform.position = other.transform.position;
            ParticlesPool.instance.particles[0].Play();
            PoolingSystem.instance.ReturnItem(other.gameObject, other.gameObject.transform.parent);
            money++;
            AudioManager.instance.StartSFX(AudioManager.instance.allSFX[2]);
            
        }
    }
}
