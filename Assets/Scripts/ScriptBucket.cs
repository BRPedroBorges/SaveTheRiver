using UnityEngine;

public class ScriptBucket : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(touchPosition.x, transform.position.y, 0);
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = new Vector3(touchPosition.x, transform.position.y, 0);
        }
#endif
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            //GameManager.instance.AddPoint();
        }
        else if (other.CompareTag("Fish"))
        {
            Destroy(other.gameObject);
            GameManager.instance.GameOver();
        }
    }
}
