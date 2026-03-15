using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f; // 플레이어 이동 속도

    [Header("Item Pickup")]
    public float pickupRange = 2f; // 아이템 습득 범위
    public int gold; // 현재 수집 골드량

    private void Update()
    {
        Move();
        PickUpItem();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(x, y).normalized;
        transform.position += (Vector3)move * moveSpeed * Time.deltaTime;
    }

    void PickUpItem()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach( Collider2D coll in items)
        {
            Item item = coll.GetComponent<Item>();

            if (item != null)
            {
                Debug.Log("PickUp");
                item.OnPickup(this);
            }
        }
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold : " + gold);
    }
}
