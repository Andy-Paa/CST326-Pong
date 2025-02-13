using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public Transform ball;
    public float speed = 1f;
    public detector detectorL;
    public detector detectorR;
    public int scoreL = 0;
    public int scoreR = 0;
    Vector3 ballPos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winnerText;
    public GameObject bg; // 背景板

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballPos = ball.position;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(speed, 0f, speed * 0.2f) * speed; // 设置初始速度
        UpdateScoreText();
    }

    public void OnDetect(detector detector)
    {
        if (detector == detectorL)
        {
            scoreR++;
            if (scoreR == 11)
            {
                Debug.Log("Player R wins!");
                winnerText.text = "Player R wins!";
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
                winnerText.text = "Player L wins!";
                scoreL = 0;
                scoreR = 0;
            }
            respawn(1f);
        }
        UpdateScoreText();
        Debug.Log($"scoreL = {scoreL}, scoreR = {scoreR}");
    }

    void respawn(float dir)
    {
        ball.position = ballPos;
        dir = Mathf.Sign(dir);
        Vector3 v = new Vector3(dir, 0f, 0f) * speed * 1.8f;
        v = Quaternion.Euler(0f, Random.Range(-15f, 15f), 0f) * v;

        var rb = ball.GetComponent<Rigidbody>();
        rb.linearVelocity = v;
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
    }

    void UpdateScoreText()
    {
        scoreText.text = $"Player L: {scoreL} - Player R: {scoreR}";

        if (scoreL > scoreR)
        {
            scoreText.color = new Color(137f / 255f, 128f / 255f, 225f / 255f);
            bg.GetComponent<Renderer>().material.color = new Color(135f / 255f, 135f / 255f, 77f / 255f);
        }
        else if (scoreR > scoreL)
        {
            scoreText.color = new Color(201f / 255f, 102f / 255f, 102f / 255f);
            bg.GetComponent<Renderer>().material.color = new Color(52f / 255f, 106f / 255f, 63f / 255f);
        }
        else
        {
            scoreText.color = Color.white;
            bg.GetComponent<Renderer>().material.color = new Color(50f / 255f, 65f / 255f, 67f / 255f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
