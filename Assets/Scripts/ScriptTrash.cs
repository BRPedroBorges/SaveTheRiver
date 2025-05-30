using UnityEngine;

public class ScriptTrash : MonoBehaviour
{
    public float fallSpeed = 0f;

    void Start()
    {
        fallSpeed += GameManager.instance.difficultyMultiplier * (GameManager.instance.score >= 10 ? 50f : (GameManager.instance.score >= 5 ? 20f : 2f));
    }
    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bucket"))
        {
            GameManager.instance.AddPoint();
            Destroy(gameObject);
        }
        else if (other.CompareTag("BottomLimit"))
        {
            Destroy(gameObject);
        }
    }
}