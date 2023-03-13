using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMain player;
    public int hp = 3;
    public int hpMax = 5;

    private void hpUp(){
        // tmp 회복 - 아이템 회복량만큼 식 짜야 함
        if (hp < hpMax) {
            hp++;
        }

    }
    private void hpDown() {
        // tmp 피해 - 적 피해량만큼 식 
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
