using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;
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
        if (!PlayerMain.isDead) {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            animator.SetBool("isMove", isMove);

            if (isMove && !PlayerMain.isJump) {
                Vector3 lookDir = new Vector3(cameraArm.forward.x, 0.0f, cameraArm.forward.z);
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0.0f, cameraArm.right.z).normalized;
                moveDir = (lookDir * moveInput.y) + (lookRight * moveInput.x);

                playerBody.forward = moveDir;
            }

            transform.position += moveDir * Time.deltaTime * playerSpeed;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!PlayerMain.isJump) {
				PlayerMain.isJump = true;
				rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else { return; }
        }
    }

    //GameManager로 분리예정
    private void Dead() {
        player.SetActive(false);
		PlayerMain.isDead = true;

        // 5초뒤 부활 - 버튼 눌러야 부활하게 바꿀까?
        Invoke("Respawn", 5);
    }

    private void Respawn() {
		PlayerMain.isDead = false;
        //플레이어 진행도에 따라 랜덤 리스폰 장소(하드코딩X)로 이동
        //임시로 0 0 0으로 이동하게 해둠
        transform.position = new Vector3(0, 0, 0);

        //무적?
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = playerBody.GetComponent<Animator>();
		rigid = playerBody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotate();
        Move();
        Jump();

        if(PlayerMain.isDead) { Dead(); }
    }
}
