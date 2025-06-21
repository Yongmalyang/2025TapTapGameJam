using System.Collections;
using UnityEngine;

public class FishGroup : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float pauseDuration = 1f;

    public float minBound;
    public float maxBound;

    private Vector3 moveDir = Vector3.right;
    private bool isPaused = false;

    private float offset = 3f;

    void Start()
    {
        moveDir = (transform.localScale.x > 0) ? Vector3.left : Vector3.right; 
        minBound = GameManager.Instance.minBounds.x - offset;
        maxBound = GameManager.Instance.maxBounds.x + offset;
    }

    void Update()
    {
        if (isPaused) return;

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);

        if (transform.position.x <= minBound || transform.position.x >= maxBound)
        {
            ReverseDirection();
        }
    }

    private void ReverseDirection()
    {
        moveDir *= -1;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public Vector3 GetDirection() => moveDir;

    public void PauseAfterCollision()
    {
        if (!isPaused)
            StartCoroutine(PauseCoroutine());
    }

    private IEnumerator PauseCoroutine()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }
}
