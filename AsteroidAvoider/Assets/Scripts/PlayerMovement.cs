using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;


    private Camera mainCamera;
    private Rigidbody myRigidbody;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();
        KeepPlayerOnScreen();
        ShipRotation();
    }

    void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }
        myRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);
        myRigidbody.velocity = Vector3.ClampMagnitude(myRigidbody.velocity, maxVelocity);
    }

    private void InputProcess()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldTouchPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            movementDirection = worldTouchPosition - transform.position;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void ShipRotation()
    {
        if (myRigidbody.velocity == Vector3.zero) { return; }
        Quaternion targetRotation = Quaternion.LookRotation(myRigidbody.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

   private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        if(viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        if(viewportPosition.x < 0)
        {
            newPosition.x = -(newPosition.x + 0.1f);
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        if (viewportPosition.y < 0)
        {
            newPosition.y = -(newPosition.y + 0.1f);
        }
        transform.position = newPosition;
    }

}
