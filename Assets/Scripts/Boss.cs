using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject Boss1; // 보스 오브젝트
    public Slider BossSliderHP; // UI 보스 HP
    public int BossCurrentHP; // 보스 현재 HP
    public int BossmaxHP; // 보스 최대 HP
    public Image BossBar; // UI상 보스 HP 체력바
    public Transform target; // 플레이어

    public GameObject BossBullet1; // 보스 공격(포탄)
    public GameObject BossBullet2; // 보스 공격(포탄)
    public Transform BossFirePos; // 포탄 공격 지점(패턴1)
    public Text InfoText; // 정보 텍스트


    public Transform BossFirePos21; // 포탄 공격 지점(패턴2)
    public Transform BossFirePos22; // 포탄 공격 지점(패턴2)
    public GameObject WinPannel;

    public AudioClip BossfireSfx; // 패턴1 공격 소리
    public AudioClip Bossfire2Sfx; // 패턴2 공격 소리
    private AudioSource BossAudioSource = null; // 오디오 값 초기화

    public GameObject MiniRobot1; // 미니로봇 오브젝트
    public GameObject MiniRobot2;
    public GameObject MiniRobot3;
    public GameObject MiniRobot4;
    public int BossHP; 
    {
        get { return BossCurrentHP; }
        set
        {
            BossHP = value;
            BossSliderHP.value = BossCurrentHP;
        }
    }
    void Start()
    {
        BossCurrentHP = BossmaxHP;
        BossSliderHP.maxValue = BossmaxHP;
        BossSliderHP.value = BossCurrentHP;
        BossAudioSource = GetComponent<AudioSource>();

        StartCoroutine(BossStartinfo());

        StartCoroutine(BossFire1());

        MiniRobot1.SetActive(false);
        MiniRobot2.SetActive(false);
        MiniRobot3.SetActive(false);
        MiniRobot4.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Banana")
        {
            BossCurrentHP--;
            Debug.Log("hp감소" + BossCurrentHP);
            BossBar.fillAmount = BossCurrentHP / 200f;
            if (BossCurrentHP <= 0)
            {
                Destroy(Boss1);
                WinPannel.SetActive(true);
            }
            else if (BossCurrentHP <= 100)
            {
                StartCoroutine(BossFire2());
                Debug.Log("거대 로봇 공격 강화");
                StartCoroutine(BossStartinfo2());
            }
            else if (BossCurrentHP <= 150)
            {
                MiniRobot1.SetActive(true);
                MiniRobot2.SetActive(true);
                MiniRobot3.SetActive(true);
                MiniRobot4.SetActive(true);
                Debug.Log("미니로봇 소환");
                StartCoroutine(BossStartinfo3());
            }
        }
    }
    public IEnumerator BossStartinfo()
    {
        InfoText.text = "거대로봇이 공격을 시작합니다";
        yield return new WaitForSeconds(3.0f);
        InfoText.text = "";
    }
    public IEnumerator BossStartinfo2()
    {
        if (BossCurrentHP == 100)
        {
            InfoText.text = "거대로봇이 공격을 강화합니다";
            yield return new WaitForSeconds(3.0f);
            InfoText.text = "";
        }
    }
    public IEnumerator BossStartinfo3()
    {
        if (BossCurrentHP == 150)
        {
            InfoText.text = "거대로봇이 근처에 미니로봇을 소환합니다";
            yield return new WaitForSeconds(3.0f);
            InfoText.text = "";
        }
    }
    IEnumerator BossFire1()
    {
        // 보스 기본 공격 패턴
        while (BossCurrentHP <= 200)
        {
            BossAudioSource.PlayOneShot(BossfireSfx, 2.0f);
            Instantiate(BossBullet1, BossFirePos.transform.position, BossFirePos.transform.rotation);
            Debug.Log("포탄 발사(2초마다)");
            yield return new WaitForSeconds(2f);
        }
    }
    IEnumerator BossFire2()
    {
        // 보스 HP 100 이하 시 공격 패턴
        while (BossCurrentHP <= 100)
        {
            BossAudioSource.PlayOneShot(Bossfire2Sfx, 3.0f);
            Instantiate(BossBullet2, BossFirePos21.transform.position, BossFirePos21.transform.rotation);
            Instantiate(BossBullet2, BossFirePos22.transform.position, BossFirePos22.transform.rotation);
            Debug.Log("포탄 추가 발사(5초마다)");
            yield return new WaitForSeconds(3f);
        }
    }
}
