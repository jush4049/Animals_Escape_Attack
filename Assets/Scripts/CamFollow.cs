using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform following_object;

    private void FixedUpdate()
    {
        {
            Vector3 pos = this.transform.position;
            this.transform.position = Vector3.Lerp(pos, following_object.position, 10.0f);
        }
    }
}
