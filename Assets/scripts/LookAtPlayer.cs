using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerT;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(playerT);
        
    }
}
