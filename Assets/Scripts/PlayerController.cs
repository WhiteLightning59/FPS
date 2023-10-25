using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float moveSpeed;
    private Vector3 direction;

    [SerializeField] private float cameraSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private Vector3 camRot;

    private Rigidbody playerRB;
    private Camera playerCam;
    private Animator anim;
    [SerializeField] private GameObject arms;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerRB = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleRunning();
        HandleMovement();
        HandleCamera();
        HandleAnimations();
    }

    private void HandleMovement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical");

        direction = (transform.right * dirX + transform.forward * dirZ).normalized;
        playerRB.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
    }

    private void HandleRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        camRot.x -= mouseY * cameraSensitivity;
        camRot.x = Mathf.Clamp(camRot.x, -cameraRotationLimit, cameraRotationLimit);
        camRot.y = mouseX * cameraSensitivity;

        playerCam.transform.localEulerAngles = new Vector3(camRot.x, 0f, 0f);
        arms.transform.localEulerAngles = playerCam.transform.localEulerAngles;
        playerRB.MoveRotation(transform.rotation * Quaternion.Euler(0f, camRot.y, 0f));
    }

    private void HandleAnimations()
    {
        if (direction == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }
        else if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
        }
        else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
        }
    }
}
