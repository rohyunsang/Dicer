using UnityEngine;
using Fusion;
using NetworkRigidbody2D = Fusion.Addons.Physics.NetworkRigidbody2D;
using static UnityEngine.EventSystems.PointerEventData;

public class PlayerRigidBodyMovement : NetworkBehaviour
{
    [Header("Movement")]
    private PlayerBehaviour _behaviour;
    [SerializeField] private LayerMask _groundLayer;
    private NetworkRigidbody2D _rb;
    private InputController _inputController;

    [SerializeField] float _speed = 10f;
    [SerializeField] float _jumpForce = 10f;
    [SerializeField] float _maxVelocity = 8f;

    [SerializeField] private float fallMultiplier = 3.3f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private Vector2 _groundHorizontalDragVector = new Vector2(.1f, 1);
    private Vector2 _airHorizontalDragVector = new Vector2(.98f, 1);


    [Networked]
    private float CoyoteTimeThreshold { get; set; } = .1f;
    [Networked]
    private float TimeLeftGrounded { get; set; }
    [Networked]
    private NetworkBool CoyoteTimeCD { get; set; }
    [Networked]
    private NetworkBool WasGrounded { get; set; }

    [Networked] public Vector3 Velocity { get; set; }

    void Awake()
    {
        _rb = GetComponent<NetworkRigidbody2D>();
        _behaviour = GetBehaviour<PlayerBehaviour>();
    }

    public override void Spawned()
    {
        // Runner.SetPlayerAlwaysInterested(Object.InputAuthority, Object, true);
    }

    /// <summary>
    /// Detects grounded and wall sliding state
    /// </summary>

    public override void FixedUpdateNetwork()
    {
        if (GetInput<InputData>(out var input))
        {
            Debug.Log(input);
            var pressed = input.GetButtonPressed(_inputController.PrevButtons);
            // pressed -> will using Jump, Attack ..etc :: Fix
            _inputController.PrevButtons = input.Buttons;
            UpdateMovement(input);
        }

        Velocity = _rb.Rigidbody.velocity;
    }

    void UpdateMovement(InputData input)
    {
        Debug.Log(input);
        if (input.GetButton(InputButton.LEFT) && _behaviour.InputsAllowed)
        {
            //Reset x velocity if start moving in opposite direction.
            if (_rb.Rigidbody.velocity.x > 0)
            {
                _rb.Rigidbody.velocity *= Vector2.up;
            }
            _rb.Rigidbody.AddForce(Vector2.left * _speed * Runner.DeltaTime, ForceMode2D.Force);
        }
        else if (input.GetButton(InputButton.RIGHT) && _behaviour.InputsAllowed)
        {
            //Reset x velocity if start moving in opposite direction.
            if (_rb.Rigidbody.velocity.x < 0 )
            {
                _rb.Rigidbody.velocity *= Vector2.up;
            }
            _rb.Rigidbody.AddForce(Vector2.right * _speed * Runner.DeltaTime, ForceMode2D.Force);
        }
        else
        {
        }

        // Adding vertical movement
        if (input.GetButton(InputButton.UP) && _behaviour.InputsAllowed)
        {
            //Reset y velocity if start moving in opposite direction.
            if (_rb.Rigidbody.velocity.y < 0)
            {
                _rb.Rigidbody.velocity *= Vector2.right;
            }
            _rb.Rigidbody.AddForce(Vector2.up * _speed * Runner.DeltaTime, ForceMode2D.Force);
        }
        else if (input.GetButton(InputButton.DOWN) && _behaviour.InputsAllowed)
        {
            //Reset y velocity if start moving in opposite direction.
            if (_rb.Rigidbody.velocity.y > 0)
            {
                _rb.Rigidbody.velocity *= Vector2.right;
            }
            _rb.Rigidbody.AddForce(Vector2.down * _speed * Runner.DeltaTime, ForceMode2D.Force);
        }
        else
        {
        }
    }
}