using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColors : MonoBehaviour
{
    public SpriteRenderer backgroundSprite;
    private Color[] colors = new Color[]
    {
        new Color(1f, 1f, 1f),        // White
        new Color(1f, 1f, 0.5f),     // Light Yellow
        new Color(0.5f, 1f, 1f),     // Light Cyan
        new Color(0.5f, 1f, 0.5f),   // Light Green
        new Color(1f, 0.5f, 1f),     // Light Magenta
        new Color(1f, 0.5f, 0.5f),   // Light Red
        new Color(0.5f, 0.5f, 1f)   // Light Blue
        
    };

    public float fadeSpeed = 1.0f;

    private int colorIndex = 0;
    private float t = 0.0f;

    void Start()
    {
        backgroundSprite = GetComponent<SpriteRenderer>();
        backgroundSprite.color = colors[colorIndex];
    }

    void Update()
    {
        t += Time.deltaTime * fadeSpeed;
        if (t >= 1.0f)
        {
            t = 0.0f;
            colorIndex = (colorIndex + 1) % colors.Length;
        }

        Color nextColor = Color.Lerp(colors[colorIndex], colors[(colorIndex + 1) % colors.Length], t);
        backgroundSprite.color = nextColor;
    }
}






