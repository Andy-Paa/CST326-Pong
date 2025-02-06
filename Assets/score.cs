using UnityEngine;

public class score : MonoBehaviour
{
    public Transform ball;
    public float speed = 1f;
    public detector detectorL;
    public detector detectorR;
    public int scoreL = 0;
    public int scoreR = 0;
    Vector3 ballPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballPos = ball.position;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(speed, 0f, speed * 0.2f)*speed; // 设置初始速度
    }

    public void OnDetect(detector detector)
    {
        if (detector == detectorL)
        {
            scoreR++;
            if (scoreR == 11)
            {
                Debug.Log("Player R wins!");
                scoreL = 0;
                scoreR = 0;
            }
            respawn(-1f);
        }
        else if (detector == detectorR)
        {
            scoreL++;
            if (scoreL == 11)
            {
                Debug.Log("Player L wins!");
                scoreL = 0;
                scoreR = 0;
            }
            respawn(1f);
        }
        Debug.Log($"scoreL = {scoreL}, scoreR = {scoreR}");
    }

    void respawn(float dir)
    {
        ball.position = ballPos;
        dir = Mathf.Sign(dir);
        Vector3 v = new Vector3(dir, 0f, 0f)*speed*1.8f;
        v = Quaternion.Euler(0f, Random.Range(-15f, 15f), 0f) * v;

        var rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = v;
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
