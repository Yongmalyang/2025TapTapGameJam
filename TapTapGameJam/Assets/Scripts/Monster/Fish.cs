using UnityEngine;

public class Fish : BaseMonster
{
    public float moveSpeed = 2f;
    private bool movingRight = false; // 기본 방향은 왼쪽

    protected override void Start()
    {
        base.Start();
        FlipObject(movingRight); // 시작 시 방향 설정
    }

    private void Update()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어와 충돌 시에는 BaseMonster에서 처리됨, Fish에서는 무시
        if (collision.gameObject.CompareTag("Player")) return;

        // 나머지 오브젝트들과 충돌하면 방향 반전
        ReverseDirection();
    }

    private void ReverseDirection()
    {
        movingRight = !movingRight;
        FlipObject(movingRight);
    }

    private void FlipObject(bool faceRight)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
        transform.localScale = scale;
    }
}
