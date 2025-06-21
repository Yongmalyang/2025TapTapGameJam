using UnityEngine;

public class Fish : BaseMonster
{
    public float moveSpeed = 2f;
    public float moveRange = 3f;

    private Vector3 startPos;
    private bool movingRight = true;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    private void Update()
    {
        float dir = movingRight ? 1 : -1;
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * dir);

        if (Mathf.Abs(transform.position.x - startPos.x) >= moveRange)
        {
            movingRight = !movingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }


}