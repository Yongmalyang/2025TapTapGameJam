using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public List<string> tagsToIgnore = new List<string> { "LeftArm", "RightArm", "LeftLeg", "RightLeg" };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tagsToIgnore.Contains(collision.gameObject.tag))
        {
            Collider2D myCollider = GetComponent<Collider2D>();
            Collider2D otherCollider = collision.collider;

            Physics2D.IgnoreCollision(myCollider, otherCollider, true);
        }
    }
}
