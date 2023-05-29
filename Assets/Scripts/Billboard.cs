using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //메인카메라와 나의 회전 방향을 일치시킨다.
        transform.rotation = Camera.main.transform.rotation;
    }
}