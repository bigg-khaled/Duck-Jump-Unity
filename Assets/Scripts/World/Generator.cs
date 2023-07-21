using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class Generator : MonoBehaviour
{
    public int width, height;
    public int minHeight, maxHeight;
    public GameObject floor;
    public GameObject duck;
    private Rigidbody2D rb;
    public int repeatnum;
    private float currentPos = 0;
    public int chunknum;
    private bool usedChunk;

    void Start()
    {
        currentPos = transform.position.x;
        Generation();
        usedChunk = false;
    }

    // Update is called once per frame
    void Generation()
    {
        
        int repeatvalue = 0;
        for(int x = (int)transform.position.x; x < width+ (int)transform.position.x; x++)
        {
            if (repeatvalue == 0)
            {
                height = Random.Range(minHeight, maxHeight);
                GenFlat(x);
                repeatvalue = repeatnum;
            }
            else
            {
                GenFlat(x);
                repeatvalue--;
            }
        }
    }

    void GenFlat(int x)
    {
        int amountSpawned = 0;
        for (float y = transform.position.y; amountSpawned < height; y+=floor.transform.localScale.y)
        {
            spawnObj(floor, x, y);
            amountSpawned++;
        }
    }
    void spawnObj(GameObject obj, int width, float height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    private void Update()
    {
        
        if(duck.transform.position.x >= currentPos + width/2)
        {
            if (usedChunk)
            {
                Destroy(gameObject);
            }
            else
            {
                GameObject newChunk = new GameObject();
                newChunk.transform.position = new Vector2(transform.position.x + width, transform.position.y);
                Generator newGenerator = newChunk.AddComponent<Generator>();
                newGenerator.width = width;
                newGenerator.height = height;
                newGenerator.minHeight = minHeight;
                newGenerator.maxHeight = maxHeight;
                newGenerator.floor = floor;
                newGenerator.duck = duck;
                newGenerator.repeatnum = repeatnum;
                newGenerator.chunknum = chunknum + 1;
                currentPos += width;
                usedChunk = true;
            }

            //GameObject newChunk = Instantiate(gameObject, new Vector2( transform.position.x + width, transform.position.y), Quaternion.identity);
            //currentPos += width;
        }
    }
}
