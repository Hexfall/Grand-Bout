using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public Color targetColor;
    public Color invertedColor;
    public float progress = 0;
    public GameObject primary;
    public GameObject secondary;

    void Start()
    {
        float H, S, V;
        Color.RGBToHSV(targetColor, out H, out S, out V);
        H = (H + .5f) % 1;
        invertedColor = Color.HSVToRGB(H, S, V);
    }

    public void SetColor(Color color)
    {
        targetColor = color;
        float H, S, V;
        Color.RGBToHSV(targetColor, out H, out S, out V);
        H = (H + .5f) % 1;
        invertedColor = Color.HSVToRGB(H, S, V);
    }

    public void SetProgress(float value)
    {
        progress = value;
        UpdateColor();
    }

    private void UpdateColor()
    {
        primary.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, targetColor, progress);
        secondary.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.black, invertedColor, progress);
    }
}
