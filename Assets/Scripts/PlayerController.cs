using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float MouseSensitivity = 1f;
    public Camera Camera;

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
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y + _mouseInput.x,
            transform.rotation.eulerAngles.z);

        Camera.transform.localRotation =
            Quaternion.Euler(Camera.transform.localRotation.eulerAngles + new Vector3(-_mouseInput.y, 0, 0));

        // Shoot
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Debug.Log("I am looking at " + hit.transform.name);
            }
        }
    }
}
