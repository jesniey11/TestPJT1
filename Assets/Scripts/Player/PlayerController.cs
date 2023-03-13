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

    // WASD 이동 (카메라가 보는 방향 기준)
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

        // 5초뒤 부활 - 버튼 눌러야 부활하게 바꿀까?
        Invoke("Respawn", 3);
    }

    private void Respawn() {

        //플레이어 진행도에 따라 랜덤 리스폰 장소(하드코딩X)로 이동
        //임시로 0 0 0으로 이동하게 해둠

        PlayerMain.isDead = false;
        player.SetActive(true);

        player.transform.position = tmpSpawnManager.spawnPosition;

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
        CameraMove();
        Move();
        Jump();

        if(PlayerMain.isDead) { Dead(); }
    }
}
