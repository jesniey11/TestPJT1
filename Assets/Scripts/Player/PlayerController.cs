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

    public float playerSpeed = 5.5f;
    public float jumpPower = 7.0f;

    // WASD �̵� (ī�޶� ���� ���� ����)
    private void Move()
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

    private void Dead() {
        player.SetActive(false);
        PlayerMain.isDead = true;
        animator.SetBool("isDead", PlayerMain.isDead);

        // 5�ʵ� ��Ȱ - ��ư ������ ��Ȱ�ϰ� �ٲܱ�?
        Invoke("Respawn", 3);
    }

    private void Respawn() {

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
        Move();
        Jump();

        if(PlayerMain.isDead) { Dead(); }
    }
}
