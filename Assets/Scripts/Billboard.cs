using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //����ī�޶�� ���� ȸ�� ������ ��ġ��Ų��.
        transform.rotation = Camera.main.transform.rotation;
    }
}