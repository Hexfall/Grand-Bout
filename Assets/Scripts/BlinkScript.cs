using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    public float rate = 6;
    
    void Update()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Mathf.PingPong(Time.time * rate, 1));
    }
}
