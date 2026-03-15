using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount; // 이동 경로 개수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement2D movement2D; // 오브젝트 이동 제어
    public int hp = 3; // 적 유닛 체력
    public GameObject goldPrefab; // 적 유닛 사망 시 드랍하는 골드 프리팹

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0) // 적 사망 처리
        {
            Die();
        }
    }

    public void Setup(Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        // 적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적 위치를 첫 번째 wayPoint로 설정
        transform.position = wayPoints[currentIndex].position;

        // 적 이동/목표지점 설정 코루틴 함수
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        // 다음 이동 방향 설정
        NextMoveTo();

        while (true)
        {
            // 적의 현재위치 <-> 목표위치 거리가 0.02 * movement2D.MoveSpeed보다 작으면 if조건문 실행
            // movement2D.MoveSpeed 곱하는 이유 -> 속도가 빠르면 한 프레임에 0.02보다 많이 움직이기 때문에
            // if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 생길 수 있음
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                // 다음 이동 방향 설정
                NextMoveTo();
            }

            yield return null;
        }
    }

    void NextMoveTo()
    {
        // 이동할 wayPoints가 남아있으면
        if (currentIndex < wayPointCount - 1)
        {
            // 적 위치를 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;

            // 다음 목표 지점(wayPoints) 설정
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized; // 정규화로 방향만 얻음
            movement2D.MoveTo(direction);
        }

        // 마지막 wayPoints이면
        else
        {
            // 적 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Instantiate(goldPrefab, transform.position, Quaternion.identity); // 현재 위치에 골드 드랍
        Destroy(gameObject);
    }
}
