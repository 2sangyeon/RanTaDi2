using UnityEngine;

public class GoldItem : Item // Item 스크립트 상속
{
    public int goldAmount = 1; // 적 사망시 드랍하는 골드량

    public override void OnPickup(PlayerController player)
    {
        player.AddGold(goldAmount);
        Destroy(gameObject);
    }
}
