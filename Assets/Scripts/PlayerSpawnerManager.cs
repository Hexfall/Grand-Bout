using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnerManager : MonoBehaviour
{
    PlayerInputManager pim;

    // Start is called before the first frame update
    void Start()
    {
        pim = GetComponent<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (pim.joiningEnabled)
                pim.DisableJoining();
            else
                pim.EnableJoining();
        }
    }
}
