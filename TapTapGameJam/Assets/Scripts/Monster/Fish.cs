using UnityEngine;

public class Fish : BaseMonster
{
    public float moveSpeed = 2f;
    private bool movingRight = false; // �⺻ ������ ����

    protected override void Start()
    {
        base.Start();
        FlipObject(movingRight); // ���� �� ���� ����
    }

    private void Update()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹 �ÿ��� BaseMonster���� ó����, Fish������ ����
        if (collision.gameObject.CompareTag("Player")) return;

        // ������ ������Ʈ��� �浹�ϸ� ���� ����
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
