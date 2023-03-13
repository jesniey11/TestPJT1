using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Transform cameraArm;
    public GameObject player;

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

    private void CameraMove()
    {
        Vector3 cameraPos = player.transform.position;
        cameraArm.position = cameraPos;
    }

    private void CameraZoom()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Vector3 cameraDir = cameraArm.rotation * Vector3.forward;

        cameraArm.transform.position += cameraDir * Time.deltaTime * scrollWheel * cameraScrollSpeed;
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
