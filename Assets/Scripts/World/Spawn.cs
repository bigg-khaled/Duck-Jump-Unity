using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float maxTime = 1;
    public float timer = 0;
    public GameObject Floor;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime)
        {
            GameObject newFloor = Instantiate(Floor);
            newFloor.transform.position = transform.position + new Vector3 (0, Random.Range(-height, height), 0);
            timer = 0;
            Destroy(newFloor, 15);
        }
        timer += Time.deltaTime;
        
    }
}
