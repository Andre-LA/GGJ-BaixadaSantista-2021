using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgCatraca : MonoBehaviour
{
    public GameObject locked, unlocked;

    public void SetLock(bool isLocked) {
        locked.SetActive(isLocked);
        unlocked.SetActive(!isLocked);
    }
}
