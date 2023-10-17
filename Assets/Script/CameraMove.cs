using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public float minYAngle = -50.0f;
    public float maxYAngle = 50.0f;
    public float minDistance = 2.0f;
    public float maxDistance = 10.0f;
    public Vector2 rotationSpeed = new Vector2(2, 2);
    public Vector2 verticalMinMax = new Vector2(-40, 85);
    public float stopCameraAtY = 1.0f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float currentDistance = 5.0f;

    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;

    public bool isFirstPerson = false;

    private Vector3 cameraOffset;

    public LayerMask obstacleLayer; // 장애물 레이어 마스크

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraOffset = transform.position - player.position; // 카메라와 플레이어 간의 초기 오프셋 설정
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            isFirstPerson = true;
        }
        else
        {
            isFirstPerson = false;
        }

        if (isFirstPerson)
        {
            firstPersonCamera.enabled = true;
            thirdPersonCamera.enabled = false;
        }
        else
        {
            firstPersonCamera.enabled = false;
            thirdPersonCamera.enabled = true;
        }

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivityY;

        rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);

        Quaternion localRotation = Quaternion.Euler(rotationY, rotationX, 0);
        transform.rotation = localRotation;

        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * sensitivityY;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -currentDistance);
        Vector3 desiredPosition = player.position + localRotation * negDistance;

        desiredPosition.y = Mathf.Max(desiredPosition.y, stopCameraAtY);

        // 카메라 위치를 조절하여 장애물을 피하도록 함
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredPosition, out hit, obstacleLayer))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = desiredPosition;
        }

        transform.LookAt(player);

        player.rotation = Quaternion.Euler(0, rotationX, 0);
    }
}
     