using UnityEngine;

public class detector : MonoBehaviour
{
    public score score;
    void OnTriggerEnter(Collider other)
    {
        score.OnDetect(this);
    }
}
