using UnityEngine;

public class Physics_player : MonoBehaviour
{
    #region Serialize Variables 
    [Header("GroundCheck")]
    [Tooltip("слой земли (Layer)")]
    [SerializeField] private LayerMask _groundLayerMask; // слой земли (Layer)
    [Tooltip("радиус сферы проверяющей стоит ли плеер на земле")]
    [SerializeField] private float _groundCheckRadius = 0.6f; // радиус сферы проверяющей стоит ли плеер на земле
    [Tooltip("позиция от куда осузествляеться проверка на землю")]
    [SerializeField] private Transform _groundCheckPosition; // позиция от куда осузествляеться проверка на землю

    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = 10f;
    [SerializeField] private float _mass = 18f;
    [SerializeField] private float _jumpForce = 35f;
    #endregion

    #region Public Variables
    #endregion

    #region Private Variables
    private CharacterController _controller;
    private bool _isGrounded; // стоит ли на земле обьект
    private float _verticalVelocity;
    private float _fallAcceleration;
    private bool _isJumping = false;
    #endregion

    #region Help Variables
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        if (!TryGetComponent<CharacterController>(out _controller))
            Debug.LogError($"Script MovePlayer | Game obj {gameObject.name} | Missed component CharacterController");
    }


    private void Update()
    {
        // check if the player is staing on the ground
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded) // if on the ground
        {
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime); // gravitation is default

            _fallAcceleration = 0f; // acceleration of the player falling sets to the default because he is staying on the ground
            _isJumping = false; // make sure player isn`t jumping now
        } // _controller.isGrounded
        else if (_isJumping) // if the player is jumping
        {
            if (_verticalVelocity < (_jumpForce / 100) * 48) // last 48% of the player jump distance set gravity lower 
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.68f;
            }
            else // other parts of jump
            {
                if (_verticalVelocity > 0) // when the player is rising up 
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else // when the player is falling down after the jump is over
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            } // else
        } // _isJumping
        else if (!_controller.isGrounded && !_isJumping) // is the player just fall down without jumping, like from the rock
        {
            _fallAcceleration += Time.deltaTime;
            //_verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 2.96f * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        if (_isGrounded) // chack if the player staying on the ground and can jump
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
                _isJumping = true;
            }
        }


        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime); // apply all physics to the player
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion



}

/*
 
 
 using UnityEngine;

public class Physics_player : MonoBehaviour
{
    //----------------Classes------------------//
    private CharacterController _controller;


    //----------------GroundCheck------------------//
    [Header("GroundCheck")]

    [Tooltip("слой земли (Layer)")]
    [SerializeField] private LayerMask _groundLayerMask; // слой земли (Layer)

    [Tooltip("радиус сферы проверяющей стоит ли плеер на земле")]
    [SerializeField] private float _groundCheckRadius = 0.6f; // радиус сферы проверяющей стоит ли плеер на земле

    [Tooltip("позиция от куда осузествляеться проверка на землю")]
    [SerializeField] private Transform _groundCheckPosition; // позиция от куда осузествляеться проверка на землю
    private bool _isGrounded; // стоит ли на земле обьект


    //----------------Gravity------------------//
    [Header("Gravity Settings")]
    [SerializeField] private float _gravity = 10f;
    [SerializeField] private float _mass = 18f;
    [SerializeField] private float _jumpForce = 35f;

    private float _verticalVelocity;
    private float _fallAcceleration;
    private bool _isJumping = false;


    private void Start()
    {
        if (!TryGetComponent<CharacterController>(out _controller))
            Debug.LogError($"Script MovePlayer | Game obj {gameObject.name} | Missed component CharacterController");
    }
    private void Update()
    {
        // check if the player is staing on the ground
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded) // if on the ground
        {
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime); // gravitation is default

            _fallAcceleration = 0f; // acceleration of the player falling sets to the default because he is staying on the ground
            _isJumping = false; // make sure player isn`t jumping now

        } // _controller.isGrounded
        else if (_isJumping) // if the player is jumping
        {
            if (_verticalVelocity < (_jumpForce / 100) * 48) // last 48% of the player jump distance set gravity lower 
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.68f;
            }
            else // other parts of jump
            {
                if (_verticalVelocity > 0) // when the player is rising up 
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else // when the player is falling down after the jump is over
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            } // else
        } // _isJumping
        else if (!_controller.isGrounded && !_isJumping) // is the player just fall down without jumping, like from the rock
        {
            _fallAcceleration += Time.deltaTime;
            //_verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 2.96f * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        if (_isGrounded) // chack if the player staying on the ground and can jump
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
                _isJumping = true;
            }
        }

        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime); // apply all physics to the player
    }
}
 
 
 */

/* THE BEST VARIANT 2
 
 
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded)
        {
            _fallAcceleration = 0f;
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            _isJumping = false;
        }
        else if (_isJumping)
        {
            if (_verticalVelocity < (_jumpForce / 100) * 48)
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.68f;
            }
            else
            {
                if(_verticalVelocity > 0)
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 1.8f*(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            }
        }
        else if (!_controller.isGrounded && !_isJumping)
        {
            _fallAcceleration += Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 2f * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        if (_isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity =  _jumpForce;
                _isJumping = true;
            }


        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    } 
 */

/*
 
     private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded)
        {
            _fallAcceleration = 0f;
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            _isJumping = false;
        }
        else if (_isJumping)
        {
            if (_verticalVelocity < (_jumpForce / 100) * 15)
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.572f;
            }
            else
            {
                if(_verticalVelocity > 0)
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 1.8f*(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            }
        }
        else if (!_controller.isGrounded && !_isJumping)
        {
            _fallAcceleration += Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;


        }

        if (_isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity =  _jumpForce;
                _isJumping = true;
            }


        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }
 
 */

/*
 
     private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded)
        {
            _fallAcceleration = 0f;
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            _isJumping = false;
        }
        else if (_isJumping)
        {
            if (_verticalVelocity < (_jumpForce / 100) * 15)
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.7f;
            }
            else
            {
                if(_verticalVelocity > 0)
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
                }
                else
                {
                    _fallAcceleration += Time.deltaTime;
                    _verticalVelocity -= ((_gravity * _mass * 2f*(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
                }
            }
        }
        else if (!_controller.isGrounded && !_isJumping)
        {
            _fallAcceleration += Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * 1.8f * (Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        if (_isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity =  _jumpForce;
                _isJumping = true;
            }


        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }
 
 */

/* THE BEST VARIANT 1
 
     private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded)
        {
            _fallAcceleration = 0f;
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            _isJumping = false;
        }
        else if (_isJumping)
        {
            if(_verticalVelocity < (_jumpForce/100)*18)
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.2f;
            }
            else
            {
                _fallAcceleration += Time.deltaTime;
                _verticalVelocity -= ((_gravity * _mass * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
            }
        }
        else if(!_controller.isGrounded && !_isJumping)
        {
            _fallAcceleration += Time.deltaTime;
            _verticalVelocity -= ((_gravity * _mass * Mathf.Sqrt(Time.deltaTime)) + _fallAcceleration) * Time.deltaTime;
        }

        if (_isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
                _isJumping = true;
            }
       

        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }
 
 
 */

/*
 
    private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);
        if(_isGrounded)
        {
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Space))
                _verticalVelocity = _jumpForce;
        }
        else
        {
            _verticalVelocity -= (_gravity * _mass * Time.deltaTime);
        }

        
        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }
 */

/*
 
 
     private void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheckPosition.position, _groundCheckRadius, _groundLayerMask);

        if (_controller.isGrounded)
        {
            _fallAcceleration = 0f;
            _verticalVelocity = (-_gravity * _mass * Time.deltaTime);
            _isJumping = false;
        }
        else if (_isJumping)
        {
            if(_verticalVelocity < (_jumpForce/100)*15)
            {
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) / 2.2f;
            }
            else
            {
                _fallAcceleration += Time.deltaTime;
                _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
            }
        }
        else if(!_controller.isGrounded && !_isJumping)
        {
            _fallAcceleration += Time.deltaTime;
            _verticalVelocity -= (_gravity * _mass * Time.deltaTime) + _fallAcceleration;
        }

        if (_isGrounded)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpForce;
                _isJumping = true;
            }


        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }
 
 */