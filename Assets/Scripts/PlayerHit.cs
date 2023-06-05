using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    public GameObject HitImage; // 총알 맞았을때 출력 이펙트
    public GameObject Player; // 플레이어
    public GameObject Bullet; // 적군 총알
    private int PlayerHP = 10; // 플레이어 체력
    public bool PlayerArmor = true; // 플레이어 아머

    public GameObject Canvas; // 메인 UI
    public GameObject GameOverPanel; // 게임종료 패널
    public GameObject GameWinPanel; // 게임승리 패널

    public Text InfoText; // 플레이어 정보 텍스트

    public Image PlayerBar; // 플레이어 체력바 

    void Start()
    {
        HitImage.SetActive(false);
        GameOverPanel.SetActive(false);
        GameWinPanel.SetActive(false);
        //StartCoroutine(Hit());
    }

    void Update()
    {
        PlayerBar.fillAmount = PlayerHP / 10f;
    }

    public IEnumerator Hit()
    {
        HitImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PlayerHP--;
        HitImage.SetActive(false);
    }
    private void OnCollisionEnter(Collision col)
    {
        // 적에게 피격 시
        if (col.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
            Debug.Log("hp감소" + PlayerHP);
            if (PlayerHP <= 0)
            {
                //만약 HP가 0이하라면 파괴한다.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // 열쇠 획득 시
        else if (col.gameObject.tag == "Key")
        {
            Debug.Log("진짜 열쇠");
            Canvas.SetActive(false);
            GameWinPanel.SetActive(true);
            Player.SetActive(false);
        }
        else if (col.gameObject.tag == "NotKey")
        {
            Debug.Log("가짜 열쇠");
            StartCoroutine(Keyinfo());
        }

        // 보스한테 피격 시
        else if (col.gameObject.tag == "BossBullet1")
        {
            StartCoroutine(Hit());
            Debug.Log("보스에 의한 hp감소" + PlayerHP);
            if (PlayerHP == 1)
            {
                //만약 HP가 0이하라면 파괴한다.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // 불꽃에 닿을 시
        else if (col.gameObject.tag == "Fire")
        {
            StartCoroutine(Hit());
            Debug.Log("불꽃에 의한 hp감소" + PlayerHP);
            if (PlayerHP == 1)
            {
                //만약 HP가 0이하라면 파괴한다.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // 낙사 시
        else if (col.gameObject.tag == "Falling")
        {
            PlayerHP = 0;
            GameOverPanel.SetActive(true);
        }

        // 회복 아이템 획득 시
        else if (col.gameObject.tag == "Fruit")
        {
            PlayerHP = 10;
        }

        // 미니 로봇과 부딫힐 경우
        else if (col.gameObject.tag == "MiniRobot")
        {
            PlayerHP--;
        }
    }

    public IEnumerator HPinfo()
    {
        InfoText.text = "캐릭터가 위험합니다! 조심하세요";
        yield return new WaitForSeconds(5.0f);
        InfoText.text = "";
    }

    public IEnumerator Keyinfo()
    {
        InfoText.text = "가짜 열쇠입니다! 진짜 열쇠를 찾으세요";
        yield return new WaitForSeconds(5.0f);
        InfoText.text = "";
    }
}
