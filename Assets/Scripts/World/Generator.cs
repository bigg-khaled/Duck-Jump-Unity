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
    private Color[] colors;
    private int currentIndex = 0;

    void Start()
    {
        colors = new Color[]
        {
            new Color(1f, 1f, 0f),       // Yellow
            new Color(0f, 1f, 1f),       // Cyan
            new Color(0f, 1f, 0f),       // Green
            new Color(1f, 0f, 1f),       // Magenta
            new Color(1f, 0f, 0f),       // Red
            new Color(0f, 0f, 0.5f),     // Dark Blue
            new Color(1f, 1f, 1f)        // White
        };
        currentPos = transform.position.x;
        Generation();
        usedChunk = false;
    }

    // Update is called once per frame
    void Generation()
    {
        
        int repeatvalue = 0;
        for(float x = transform.position.x; x < width*0.99f + transform.position.x; x+=0.99f)
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

    void GenFlat(float x)
    {
        int amountSpawned = 0;
        Color color = ColorPick();
        for (float y = transform.position.y; amountSpawned < height; y+=floor.transform.localScale.y)
        {
            spawnObj(floor, x, y);
            amountSpawned++;
        }
    }
    void spawnObj(GameObject obj, float width, float height)
    {
        obj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        obj.transform.parent = this.transform;
        obj.GetComponent<Renderer>().material.color = Color.black;
       
    }

    private void Update()
    {
        
        if(duck.transform.position.x >= currentPos + (width*0.99f)/2)
        {
            if (usedChunk)
            {
                Destroy(gameObject);
            }
            else
            {
                GameObject newChunk = new GameObject();
                newChunk.transform.position = new Vector2(transform.position.x + (width*0.99f), transform.position.y);
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
        }
    }

    Color ColorPick()
    {
        Color nextColor = colors[currentIndex];
        currentIndex = (currentIndex + 1) % colors.Length;
        return nextColor;
    }
}
