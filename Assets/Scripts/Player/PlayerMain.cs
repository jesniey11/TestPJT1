using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private Animator animator;

    //public static bool isMove = false;
    public static bool isJump = false;
    public static bool isDead = false;

    public float hpmax = 100f;
    public float hp;

    //public override bool SetHp(float hp, float hpMax) {
    //    if (hp > hpMax) { hp = hpMax; }
    //    hpNow = hp;
    //    hpMaxNow = hpMax;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { isJump = false; }
        if (collision.gameObject.CompareTag("Enemy")) { Debug.Log("Collision"); HpDown(); }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���߿� switch case ������ �ٲٱ�
        if (other.gameObject.CompareTag("DeadSpace")) { Debug.Log("Trigger Dead"); isDead = true; Debug.Log("isDead : " + isDead); }
        if (other.gameObject.CompareTag("Enemy")) { Debug.Log("Trigger"); HpDown(); }
        //if (other.gameObject.CompareTag("Attack"))
    }

    private void HpDown()
    {
        Debug.Log("now hp is " + hp);
        hp--;
        //�ϴ� hp--; ���� ���ⵥ���� * @ �ʿ�.... �� ���⵩*�Ÿ���ʵ� ���...
        if (hp <= 0) { hp = 0; isDead = true; Debug.Log("isDead : " + isDead); }
    }

    private void HpUp() {
        // hp�� 0�϶� �ٷ� ���ó�� �ϱ� ���ؼ� 0�϶� ȸ�� �Ұ�
        if (hp <= 0) { return; }
        hp++;
        if (hp < hpmax) { hp = hpmax; }        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Set Default hp
        hp = hpmax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    
}
