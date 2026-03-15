using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void OnPickup(PlayerController player)
    {
        Destroy(gameObject);
    }
}
