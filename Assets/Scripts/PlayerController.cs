using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float JumpSpeed = 1f;
    public float MouseSensitivity = 1f;
    public Transform CameraTransform;

    private Rigidbody _rb;
    private CapsuleCollider _collider;

    private Vector3 _moveInput;
    private Vector3 _mouseInput;

    private float _distToGround;
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _distToGround = _collider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement intention
        _moveInput += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Jump intention
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _moveInput += new Vector3(0, JumpSpeed, 0);
        }

        // View
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _mouseInput.x,
            transform.rotation.eulerAngles.z);

        CameraTransform.localRotation = Quaternion.Euler(CameraTransform.localRotation.eulerAngles + new Vector3(-_mouseInput.y, 0, 0));
    }

    private void FixedUpdate()
    {
        // Movement
        if (IsGrounded())
        {
            if (_moveInput != Vector3.zero)
            {
                Vector3 moveX = transform.right * _moveInput.x;
                Vector3 moveY = transform.up * _moveInput.y;
                Vector3 moveZ = transform.forward * _moveInput.z;

                _rb.velocity = (moveX + moveY + moveZ) * MoveSpeed;
                _moveInput = Vector3.zero;
            }
        }
    }

    private bool IsGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _distToGround + 0.1f);
        return _isGrounded;
    }
}
