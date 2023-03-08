using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public static bool isJump = false;
    public static bool isDead = false;

    //public static float hpMaxNow = 0.0;
    //public static float hpNow = 0.0;

    //public override bool SetHp(float hp, float hpMax) {
    //    if (hp > hpMax) { hp = hpMax; }
    //    hpNow = hp;
    //    hpMaxNow = hpMax;
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { isJump = false; }
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
