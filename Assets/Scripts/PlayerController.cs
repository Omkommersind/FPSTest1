using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float MouseSensitivity = 1f;
    public Transform CameraTransform;

    private Rigidbody _rb;

    private Vector3 _moveInput;
    private Vector3 _mouseInput;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        _moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 moveX = transform.right * _moveInput.x;
        Vector3 moveZ = transform.forward * _moveInput.z;

        _rb.velocity = (moveX + moveZ) * MoveSpeed;

        // View
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * MouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + _mouseInput.x,
            transform.rotation.eulerAngles.z);

        CameraTransform.localRotation = Quaternion.Euler(CameraTransform.localRotation.eulerAngles + new Vector3(-_mouseInput.y, 0, 0));
    }
}
