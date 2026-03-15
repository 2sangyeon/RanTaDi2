using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public GameObject projectilePrefab;
    public float attackRange = 3f;
    public float attackRate = 1f;
    public int damage = 1; // 공격력
    public float projectileSpeed = 8f; // 총알 속도

    private float attackTimer; // 공격 주기 계산

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer < 1f / attackRate) return;

        Fire(); // 공격 (투사체 발사)
        attackTimer = 0f; // 공격 주기 초기화
    }

    void Fire()
    {
        Enemy target = FindClosestEnemy();

        if (target == null) return;

        Vector2 direction = (target.transform.position - transform.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        proj.GetComponent<Projectile>().Initialize(direction, projectileSpeed, damage);
    }

    Enemy FindClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange);

        float closestDist = Mathf.Infinity;
        Enemy target = null;

        foreach (Collider2D coll in enemies)
        {
            if (coll.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, coll.transform.position);

                if (dist < closestDist)
                {
                    closestDist = dist;
                    target = coll.GetComponent<Enemy>();
                }
            }
        }

        return target;
    }

    private void OnDrawGizmosSelected() // 플레이어 공격 사거리 표시
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
