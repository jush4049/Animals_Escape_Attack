using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniRobot : MonoBehaviour
{
    public Slider MRsliderHP;
    // - 현재 체력
    int MRcurrentHP;
    // - 최대 체력
    public int MRmaxHP;

    public Image MRbar;

    public Transform target2; //플레이어

    public GameObject MiniRobots; // 미니로봇   

    public GameObject MRBullet; // 미니로봇 총알

    public Transform MRFirePos; // 미니로봇 총알 공격 지점(패턴1)

    /*private AudioSource MRAudioSource = null; // 오디오 값 초기화
    public AudioClip MRfireSfx; // 미니로봇 공격소리*/

    public int MRHP
    {
        get { return MRcurrentHP; }
        set
        {
            //값 변경
            MRcurrentHP = value;
            //UI에 변경 적용
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
            Debug.Log("미니로봇 hp감소" + MRcurrentHP);
            if (MRcurrentHP <= 0)
            {
                //만약 HP가 0이하라면 파괴한다.
                Destroy(MiniRobots);
                Debug.Log("미니로봇 제거)");
            }
        }

    }
    
    IEnumerator MiniRobotFire()
    {
            /*MRAudioSource.PlayOneShot(MRfireSfx, 5.0f);*/
            Instantiate(MRBullet, MRFirePos.transform.position, MRFirePos.transform.rotation);
            Debug.Log("미니로봇 포탄 발사(1초마다)");
            yield return new WaitForSeconds(1f);
    }
}
