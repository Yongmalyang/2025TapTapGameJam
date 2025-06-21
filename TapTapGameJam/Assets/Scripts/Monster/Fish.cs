using UnityEngine;

public class Fish : BaseMonster
{
    public float moveSpeed = 2f;
    private bool movingRight = true; // �⺻ ������ ������

    protected override void Start()
    {
        base.Start();
        FlipObject(movingRight); // ���� �� ���� ����
    }

    private void Update()
    {
        float dir = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Fish Hit!!!!");
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
