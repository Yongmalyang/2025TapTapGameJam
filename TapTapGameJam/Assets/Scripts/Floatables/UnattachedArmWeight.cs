using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnattachedArmWeight : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
