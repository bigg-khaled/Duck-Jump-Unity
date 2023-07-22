using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreak : MonoBehaviour
{ 
    public GameObject brokenEggPiecePrefab; // Prefab of a single broken egg piece
    public int numberOfPieces = 4; // Number of pieces the egg will break into

    public void eggBroken()
    {
        // Disable the original egg sprite
        GetComponent<SpriteRenderer>().enabled = false;

        // Get the bounds of the egg sprite
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;

        // Calculate the size of each broken piece
        Vector2 pieceSize = bounds.size / numberOfPieces;

        // Calculate the starting position for splitting the egg
        Vector2 startPos = bounds.min;

        // Create the broken egg pieces
        for (int i = 0; i < numberOfPieces; i++)
        {
            // Calculate the center position for the current piece
            Vector2 centerPos = startPos + pieceSize * 0.5f;

            // Instantiate a broken egg piece
            GameObject brokenPiece = Instantiate(brokenEggPiecePrefab, centerPos, Quaternion.identity);

            // Set the size of the broken piece to match the calculated piece size
            brokenPiece.transform.localScale = pieceSize;

            // Adjust the starting position for the next piece
            startPos.x += pieceSize.x;
            if (startPos.x > bounds.max.x)
            {
                startPos.x = bounds.min.x;
                startPos.y += pieceSize.y;
            }

            // Add random force to each broken piece
            Rigidbody2D rb = brokenPiece.GetComponent<Rigidbody2D>();
            Vector2 randomForce = Random.insideUnitCircle * 2.0f;
            rb.AddForce(randomForce, ForceMode2D.Impulse);

            // Destroy the broken pieces after some time (you can adjust this value to control the lifetime)
            float destroyTime = Random.Range(2.0f, 4.0f);
            Destroy(brokenPiece, destroyTime);
        }

        // Destroy the original egg object after a short delay (you can adjust this value to control the delay)
        Destroy(gameObject, 0.2f);
    }
}

