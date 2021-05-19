using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControl : MonoBehaviour
{
    float x;
    float y = -4.125f;
    float z = 0;
    Vector3 pos;

    void Start()
    {
        RandomPosition();
    }

    public void RandomPosition()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        x = Random.Range(0, 8);
        pos = new Vector3(x, y, z);
        transform.position = pos;
    }
}
