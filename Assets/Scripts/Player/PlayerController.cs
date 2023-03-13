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
	private Transform cameraArm;
    [SerializeField]
    private Transform playerCamera;

	public float playerSpeed = 5.5f;
    public float jumpPower = 7.0f;
	public float cameraMoveSpeed = 2.0f;
	public float cameraScrollSpeed = 1000.0f;

	// ���콺 �����ӿ� ���� ȭ�� ȸ��
	private void CameraRotate()
	{

		Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X") * cameraMoveSpeed, Input.GetAxis("Mouse Y") * cameraMoveSpeed);
		Vector3 cameraAngle = cameraArm.rotation.eulerAngles; // ī�޶��� rotation ���� ���Ϸ� ������ �ٲ�

		float x = cameraAngle.x - mouseDelta.y;

		// ī�޶� ���� ���� 40��, �Ʒ��� 25�� ���� 
		if (x < 180f) { x = Mathf.Clamp(x, -1.0f, 40.0f); }
		else { x = Mathf.Clamp(x, 335f, 361f); }

		cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
	}

    // �÷��̾ ���󰡴� CameraArm
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
		
		//Vector3 cameraPos = playerCamera.transform.position;
		//cameraPos.z += Time.deltaTime * scrollWheel * cameraScrollSpeed;

		//Debug.Log(scrollWheel);
		
        // ������ z�� �̵� - ��ũ�� amount * deltaTime * 
	}




	// WASD �̵� (ī�޶� ���� ���� ����)
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
        PlayerMain.isDead = true;
        animator.SetBool("isDead", PlayerMain.isDead);

        // 5�ʵ� ��Ȱ - ��ư ������ ��Ȱ�ϰ� �ٲܱ�?
        Invoke("Respawn", 3);
    }

    private void Respawn() 
    {

        //�÷��̾� ���൵�� ���� ���� ������ ���(�ϵ��ڵ�X)�� �̵�
        //�ӽ÷� 0 0 0���� �̵��ϰ� �ص�

        PlayerMain.isDead = false;
        player.SetActive(true);

        player.transform.position = tmpSpawnManager.spawnPosition;

        //����?
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
