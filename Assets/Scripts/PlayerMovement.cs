using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera PlayerCamera;
    public float WalkSpeed = 6f;
    public float RunSpeed = 12f;
    public float JumpPower = 7f;
    public float Gravity = 9.81f;
    public float LookSpeed = 2f;
    public float LookXLimit = 45f;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;

    private CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        #region Handles Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to Run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? RunSpeed : WalkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? RunSpeed : WalkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion


        #region Handles Jumping

        if (Input.GetButton("Jump") && _characterController.isGrounded)
        {
            _moveDirection.y = JumpPower;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }

        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= Gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);

        #endregion


        #region Handles Rotation

        _rotationX += -Input.GetAxis("Mouse Y") * LookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -LookXLimit, LookXLimit);
        PlayerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);

        #endregion
    }
}