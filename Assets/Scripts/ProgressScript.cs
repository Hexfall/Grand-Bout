using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressScript : MonoBehaviour
{
    public GameObject[] items;
    public float progress = 0;

    void Awake()
    {       
        SetProgress(0f);
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
