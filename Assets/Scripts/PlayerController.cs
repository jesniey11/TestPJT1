using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;
    private Vector3 moveDir;

    private Transform cameraArm;
    private Transform playerBody;
    private GameObject player;

    public float cameraSpeed = 2.0f;
    public float playerSpeed = 5.5f;
    public float jumpPower = 10.0f;

    // 마우스 움직임에 따른 화면 회전
    private void CameraRotate() {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * cameraSpeed, Input.GetAxis("Mouse Y") * cameraSpeed);
        Vector3 cameraAngle = cameraArm.rotation.eulerAngles; // 카메라의 rotation 값을 오일러 각으로 바꿈

        float x = cameraAngle.x - mouseDelta.y;

        // 카메라 각도 위쪽 70도, 아래쪽 25도 제한
        if (x < 180f) { x = Mathf.Clamp(x, -1.0f, 70.0f); }
        else { x = Mathf.Clamp(x, 335f, 361f); }

        cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
    }

    // WASD 이동 (카메라가 보는 방향 기준)
    private void Move()
    {
        if (!playerControl.isDead) {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), )
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
