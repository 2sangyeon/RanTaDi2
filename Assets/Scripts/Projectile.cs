using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector2 direction;
    float speed;
    int damage;

    public float lifeTime = 3f;

    public void Initialize(Vector2 dir, float spd, int dmg)
    {
        direction = dir;
        speed = spd;
        damage = dmg;

        Destroy(gameObject, lifeTime); // lifeTime초 후 projectile 삭제
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject); // 적에게 적중 시 projectile 삭제
        }
    }
}
