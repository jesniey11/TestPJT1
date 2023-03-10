using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class tmpSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;

    public static Vector3 spawnPosition;

    private void Spwan() {
        spawnPosition = spawnPoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Spwan();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
