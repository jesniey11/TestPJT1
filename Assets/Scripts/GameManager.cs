using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMain player;
    public int hp = 3;
    public int hpMax = 5;

    private void hpUp(){
        // tmp ȸ�� - ������ ȸ������ŭ �� ¥�� ��
        if (hp < hpMax) {
            hp++;
        }

    }
    private void hpDown() {
        // tmp ���� - �� ���ط���ŭ �� 
        hp--;
        
        if (hp < 1) { }
        if (hp < 0) { }
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
