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
