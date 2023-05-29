using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniRobot : MonoBehaviour
{
    public Slider MRsliderHP;
    // - ���� ü��
    int MRcurrentHP;
    // - �ִ� ü��
    public int MRmaxHP;

    public Image MRbar;

    public Transform target2; //�÷��̾�

    public GameObject MiniRobots; // �̴Ϸκ�   

    public GameObject MRBullet; // �̴Ϸκ� �Ѿ�

    public Transform MRFirePos; // �̴Ϸκ� �Ѿ� ���� ����(����1)

    /*private AudioSource MRAudioSource = null; // ����� �� �ʱ�ȭ
    public AudioClip MRfireSfx; // �̴Ϸκ� ���ݼҸ�*/

    public int MRHP
    {
        get { return MRcurrentHP; }
        set
        {
            //�� ����
            MRcurrentHP = value;
            //UI�� ���� ����
            MRsliderHP.value = MRcurrentHP;
        }
    }
    void Start()
    {
    }

        void Update()
    {
        transform.LookAt(target2);
        /*StartCoroutine(MiniRobotFire());*/
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Banana")
        {
            MRcurrentHP--;
            MRbar.fillAmount = MRcurrentHP / 3f;
            Debug.Log("�̴Ϸκ� hp����" + MRcurrentHP);
            if (MRcurrentHP <= 0)
            {
                //���� HP�� 0���϶�� �ı��Ѵ�.
                Destroy(MiniRobots);
                Debug.Log("�̴Ϸκ� ����)");
            }
        }

    }
    
    IEnumerator MiniRobotFire()
    {
            /*MRAudioSource.PlayOneShot(MRfireSfx, 5.0f);*/
            Instantiate(MRBullet, MRFirePos.transform.position, MRFirePos.transform.rotation);
            Debug.Log("�̴Ϸκ� ��ź �߻�(1�ʸ���)");
            yield return new WaitForSeconds(1f);
    }
}
