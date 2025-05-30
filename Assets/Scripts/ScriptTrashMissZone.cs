using UnityEngine;

public class ScriptTrashMissZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Destroy(other.gameObject);
            GameManager.instance.AddError();
        }
    }
}
