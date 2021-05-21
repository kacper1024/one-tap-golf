using UnityEngine;

public class HoleControl : MonoBehaviour
{
    const float y = -4.125f;
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

        transform.position = new Vector2(bufferRand[0], y);
    }
}
