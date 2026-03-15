using UnityEngine;

public class GoldItem : Item
{
    public int goldAmount = 1; // 적 사망시 드랍하는 골드량

    public override void OnPickup(PlayerController player)
    {
        player.AddGold(goldAmount);
        Destroy(gameObject);
    }
}
