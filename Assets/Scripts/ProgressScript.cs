using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour
{
    public GameObject[] items;
    public GameObject[] blinking;
    [Range(0,1)]
    public float blinkThreshold = 0.75f;
    [Range(0,1)]
    public float progress = 0;

    void Awake()
    {       
        SetProgress(0f);
    }

    private void Update()
    {
        if (progress >= blinkThreshold)
            Blink();
    }

    private void Blink()
    {
        foreach (var item in blinking)
            item.GetComponent<SpriteRenderer>().color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, Mathf.PingPong(Time.time * 3, 1));
    }

    public void SetColor(Color color)
    {
        foreach (var item in items)
            item.GetComponent<ColorScript>().SetColor(color);
    }

    public void SetProgress(float value)
    {
        progress = value;
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<ColorScript>().SetProgress(
                Mathf.Min(
                    1f,
                    Mathf.Max(
                        0f,
                        progress * (float) items.Length - i
                    )
                )
            );
        }
    }
}
