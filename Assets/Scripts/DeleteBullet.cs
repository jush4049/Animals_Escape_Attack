using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteBullet : MonoBehaviour
{
    public GameObject obj;
    public float lifetime;

    void Awake()
    {
        Destroy(obj, lifetime);
    }
}
