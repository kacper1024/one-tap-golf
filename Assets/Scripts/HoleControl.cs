using UnityEngine;

public class HoleControl : MonoBehaviour
{
    float y = -4.125f;
    Vector2 pos;
    public int[] bufferRand = new int[] { 0, 0 };

    void Start()
    {
        RandomPosition();
    }

    public void RandomPosition()
    {
        while(true)
        {
            bufferRand[0] = Random.Range(0, 8);
            if (bufferRand[0] == bufferRand[1])
            {
                continue;
            }
            else
                break;
        }
        
        pos = new Vector2(bufferRand[0], y);
        transform.position = pos;
    }
}
