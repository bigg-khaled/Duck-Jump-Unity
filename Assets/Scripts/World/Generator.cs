using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [Header("Chunk Generation settings")]
    public int width, height;
    public int minHeight, maxHeight;
    public int chunknum = 0;

    [Header("GameOnjects")]
    public GameObject floor;
    public GameObject duck;
    public GameObject Egg;
    public GameObject challengeDetect;
    public GameObject SeaGull;
    public GameObject ceiling;

    [NonSerialized] public int repeatnum;
    private float currentPos = 0;
    private bool usedChunk;

    private void Awake()
    {
        
    }
    private void Start()
    {
        // duck = GameObject.FindGameObjectWithTag("Player");
        currentPos = transform.position.x;
        usedChunk = false;
        Generation();
    }

    private void Generation()
    {
        // Randomly choose a column for the challenge
        int ColumnChooser = UnityEngine.Random.Range(2, (width / 2 - 1));
        int columnNum = 1;
        int repeatvalue = 0;

        for (float x = transform.position.x; x < (width) * floor.GetComponent<SpriteRenderer>().bounds.size.x + transform.position.x; x += floor.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            if ((ColumnChooser == columnNum) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.MIND_THE_GAP) && (chunknum != 0))
            {
                print("RAND: " + ColumnChooser + " COL: " + columnNum + " CHALLENGE: " + (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.MIND_THE_GAP));
                // For MIND_THE_GAP challenge, set the startGap position and skip this column
                challengeDetect.GetComponent<Challenge>().startGap = x + floor.GetComponent<SpriteRenderer>().bounds.size.x;
                columnNum++;
                continue;
            }
            else if ((ColumnChooser == columnNum) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.HIT_SEAGULL))
            {
                // Spawn a SeaGull at this column
                spawnObj(SeaGull, x, height * floor.GetComponent<SpriteRenderer>().bounds.size.y + transform.position.y + 2);
            }

            if (repeatvalue == 0)
            {
                // Generate a flat column with random height
                height = UnityEngine.Random.Range(minHeight, maxHeight);
                GenFlat(x);
                repeatvalue = repeatnum;
            }
            else
            {
                // Continue generating the same height column
                GenFlat(x);
                repeatvalue--;
            }

            spawnObj(ceiling, x, height * floor.GetComponent<SpriteRenderer>().bounds.size.y + transform.position.y);

            if ((ColumnChooser == columnNum) && (challengeDetect.GetComponent<Challenge>().challengeType == Challenge.ChallengeType.HIT_TARGET) && (chunknum != 0))
            {
                // Spawn an Egg at this column
                spawnObj(Egg, x, height * (floor.GetComponent<SpriteRenderer>().bounds.size.y + 1) + transform.position.y);
            }

            columnNum++;
        }
    }

    private void GenFlat(float x)
    {
        int amountSpawned = 0;
        for (float y = transform.position.y; amountSpawned < height; y += floor.GetComponent<SpriteRenderer>().bounds.size.y)
        {
            // Spawn a floor object
            spawnObj(floor, x, y);
            amountSpawned++;
        }
    }

    private void spawnObj(GameObject obj, float width, float height)
    {
        // Instantiate and parent the object
        GameObject newObj = Instantiate(obj, new Vector2(width, height), Quaternion.identity);
        newObj.transform.parent = transform;
    }

    private void Update()
    {
        if (duck.transform.position.x >= currentPos + (width * floor.GetComponent<SpriteRenderer>().bounds.size.x) / 2)
        {
            if (usedChunk)
            {
                // If usedChunk is true, destroy this chunk when the duck passes halfway through it
                Destroy(gameObject);
            }
            else
            {
                // Create a new chunk when the duck passes halfway through this chunk
                GameObject newChunk = new GameObject();
                newChunk.transform.position = new Vector2((width) * floor.GetComponent<SpriteRenderer>().bounds.size.x + transform.position.x, transform.position.y);
                Generator newGenerator = newChunk.AddComponent<Generator>();

                // Transfer necessary variables to the new chunk
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
                newGenerator.ceiling = ceiling;

                currentPos += width;
                usedChunk = true;
            }
        }
    }
}
