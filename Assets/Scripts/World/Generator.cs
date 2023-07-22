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
    public GameObject Egg;
    public GameObject challengeDetect;
    public GameObject SeaGull;
    private Rigidbody2D rb;
    public int repeatnum;
    private float currentPos = 0;
    public int chunknum = 0;
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
        float ColumnChooser = transform.position.x + Random.Range(1, (width/2 - 1)) * 0.99f;
        int repeatvalue = 0;
        for(float x = transform.position.x; x < width*0.99f + transform.position.x; x+=0.99f)
        {
            if (((int)x == (int)ColumnChooser) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.MIND_THE_GAP))
            {
                challengeDetect.GetComponent<Challenge>().startGap = x + floor.transform.localScale.x * 0.99f;
                continue;
            }
            else if (((int)x == (int)ColumnChooser) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.HIT_SEAGULL))
            {
                spawnObj(SeaGull, x, height * floor.transform.localScale.y + transform.position.y + 2);
            }
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
            if(((int)x == (int)ColumnChooser) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.HIT_TARGET) && (chunknum != 0)) spawnObj(Egg, x, height* floor.transform.localScale.y + transform.position.y);
            
        }
    }

    void GenFlat(float x)
    {
        int amountSpawned = 0;
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
                newGenerator.Egg = Egg;
                newGenerator.challengeDetect = challengeDetect;
                newGenerator.SeaGull = SeaGull;
                currentPos += width;
                usedChunk = true;
            }
        }
    }

}
