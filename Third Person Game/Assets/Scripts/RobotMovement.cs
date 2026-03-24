using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    CharacterController RobotController;
    [SerializeField] InputActionAsset inputActions;
    InputActionMap actionMap;
    InputAction moveAction;
    float currentVelocity;
    //Vector2 moveInput;
    //Vector3 moveDirection;

    [SerializeField] float Speed = 5f;
    [SerializeField] float rotationSmoothTime = 0.1f;

    void Awake()
    {
        actionMap = inputActions.FindActionMap("Player");
        moveAction = actionMap.FindAction("Move");
    }
    void OnEnable()
    {
        actionMap.Enable();   
    }
    void OnDisable()
    {
        actionMap.Disable();
    }
    void Start()
    {
     RobotController = GetComponent<CharacterController>();   
    }

    void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, rotationSmoothTime);
        Debug.Log(smoothAngle);
        transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

        RobotController.Move(moveDirection * Time.deltaTime * Speed);
    }
}
