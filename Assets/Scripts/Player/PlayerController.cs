using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;    
    private Vector3 moveDir;

    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject cookie;


    [SerializeField]
	private Transform cameraArm;
    [SerializeField]
    private Transform playerCamera;

	public float playerSpeed = 5.5f;
    public float jumpPower = 7.0f;
	public float cameraMoveSpeed = 2.0f;
	public float cameraScrollSpeed = 1000.0f;

	// 마우스 움직임에 따른 화면 회전
	private void CameraRotate()
	{

		Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * cameraMoveSpeed, Input.GetAxis("Mouse Y") * cameraMoveSpeed);
		Vector3 cameraAngle = cameraArm.rotation.eulerAngles; // 카메라의 rotation 값을 오일러 각으로 바꿈

		float x = cameraAngle.x - mouseDelta.y;

		// 카메라 각도 위쪽 40도, 아래쪽 25도 제한 
		if (x < 180f) { x = Mathf.Clamp(x, -1.0f, 40.0f); }
		else { x = Mathf.Clamp(x, 335f, 361f); }

		cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
	}

    // 플레이어를 따라가는 CameraArm
    private void CameraMove() 
    {
       Vector3 cameraArmPos = player.transform.position;
       cameraArm.position = cameraArmPos;
    }

    // 
    private void CameraZoom()
	{
		float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheel != 0) {
			Vector3 cameraDir = playerCamera.rotation * Vector3.forward;
			playerCamera.transform.position += cameraDir * Time.deltaTime * scrollWheel * cameraScrollSpeed;
		}
	}




	// WASD 이동 (카메라가 보는 방향 기준)
	private void PlayerMove()
    {
        if (!PlayerMain.isDead) {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            bool isMove = moveInput.magnitude != 0;
            animator.SetBool("isMove", isMove);

            if (isMove && !PlayerMain.isJump) {
                Vector3 lookDir = new Vector3(cameraArm.forward.x, 0.0f, cameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(cameraArm.right.x, 0.0f, cameraArm.right.z).normalized;
                moveDir = (lookDir * moveInput.y) + (lookRight * moveInput.x);

                playerBody.forward = moveDir;
            }

            transform.position += moveDir * Time.deltaTime * playerSpeed;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!PlayerMain.isJump) {
				PlayerMain.isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else { return; }
        }
    }

    private void Dead() 
    {
        player.SetActive(false);
        animator.SetBool("isDead", PlayerMain.isDead);

        Invoke("TmpRespawn", 3);
    }

    // 테스트를 위한 리스폰
    private void TmpRespawn() 
    {

        //플레이어 진행도에 따라 랜덤 리스폰 장소(하드코딩X)로 이동
        //임시로 0 0 0으로 이동하게 해둠

        PlayerMain.isDead = false;
        Debug.Log("isDead : " + PlayerMain.isDead);
        player.SetActive(true);

        player.transform.position = tmpSpawnManager.spawnPosition;
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
        CameraMove();
        CameraZoom();
		PlayerMove();
		PlayerJump();

        if(PlayerMain.isDead) { Dead(); }
    }
}
