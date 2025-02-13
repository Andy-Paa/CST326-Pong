using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float speedMultiplier = 1.1f; // 碰到pad时的加速倍率
    public AudioClip bounceSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Rigidbody rb = GetComponent<Rigidbody>();
        // rb.linearVelocity = new Vector3(initialSpeed, 0f, initialSpeed * 0.2f); // 设置初始速度
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 up = new Vector3(0f, 1f, 0f);
        // Quaternion posRotation = Quaternion.Euler(45f, 0f, 0f);
        // Quaternion negRotation = Quaternion.Euler(-45f, 0f, 0f);

        // Vector3 posVector = posRotation * up;
        // Vector3 negVector = negRotation * up;

        // Debug.DrawRay(transform.position, posVector, Color.red);
        // Debug.DrawRay(transform.position, negVector, Color.blue);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"made contact with {collision.gameObject.name}");
        Rigidbody rb = GetComponent<Rigidbody>();
        float speed = rb.linearVelocity.magnitude;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = bounceSound;
        audioSource.pitch = Mathf.Clamp(speed / initialSpeed, 0.5f, 2f);
        audioSource.Play();

        // if (collision.gameObject.CompareTag("pad"))
        // {
        //     Vector3 normal = collision.contacts[0].normal;
        //     Vector3 reflectDirection = Vector3.Reflect(rb.linearVelocity, normal);
        //     reflectDirection = new Vector3(reflectDirection.x, 0f, reflectDirection.z); // 保持水平反弹
        //     rb.linearVelocity = reflectDirection * rb.linearVelocity.magnitude * speedMultiplier; // 加速
        // }
        // else */
        if (collision.gameObject.CompareTag("acc"))
        {
            rb.linearVelocity *= 1.2f;
        }
        else if (collision.gameObject.CompareTag("dec"))
        {
            rb.linearVelocity *= 0.8f;
        }
        // else
        // {
        //     Vector3 normal = collision.contacts[0].normal;
        //     Vector3 reflectDirection = Vector3.Reflect(rb.linearVelocity, normal);
        //     rb.linearVelocity = reflectDirection * rb.linearVelocity.magnitude;
        // }
    }
}
