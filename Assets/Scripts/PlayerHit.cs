using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHit : MonoBehaviour
{
    public GameObject HitImage; // �Ѿ� �¾����� ��� ����Ʈ
    public GameObject Player; // �÷��̾�
    public GameObject Bullet; // ���� �Ѿ�
    private int PlayerHP = 10; // �÷��̾� ü��
    public bool PlayerArmor = true; // �÷��̾� �Ƹ�

    public GameObject Canvas; // ���� UI
    public GameObject GameOverPanel; // �������� �г�
    public GameObject GameWinPanel; // ���ӽ¸� �г�

    public Text InfoText; // �÷��̾� ���� �ؽ�Ʈ

    public Image PlayerBar; // �÷��̾� ü�¹� 

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
        // ������ �ǰ� ��
        if (col.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
            Debug.Log("hp����" + PlayerHP);
            if (PlayerHP <= 0)
            {
                //���� HP�� 0���϶�� �ı��Ѵ�.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // ���� ȹ�� ��
        else if (col.gameObject.tag == "Key")
        {
            Debug.Log("��¥ ����");
            Canvas.SetActive(false);
            GameWinPanel.SetActive(true);
            Player.SetActive(false);
        }
        else if (col.gameObject.tag == "NotKey")
        {
            Debug.Log("��¥ ����");
            StartCoroutine(Keyinfo());
        }

        // �������� �ǰ� ��
        else if (col.gameObject.tag == "BossBullet1")
        {
            StartCoroutine(Hit());
            Debug.Log("������ ���� hp����" + PlayerHP);
            if (PlayerHP == 1)
            {
                //���� HP�� 0���϶�� �ı��Ѵ�.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // �Ҳɿ� ���� ��
        else if (col.gameObject.tag == "Fire")
        {
            StartCoroutine(Hit());
            Debug.Log("�Ҳɿ� ���� hp����" + PlayerHP);
            if (PlayerHP == 1)
            {
                //���� HP�� 0���϶�� �ı��Ѵ�.
                Canvas.SetActive(false);
                GameOverPanel.SetActive(true);
                Player.SetActive(false);
            }
            else if (PlayerHP == 2)
            {
                StartCoroutine(HPinfo());
            }
        }

        // ���� ��
        else if (col.gameObject.tag == "Falling")
        {
            PlayerHP = 0;
            GameOverPanel.SetActive(true);
        }

        // ȸ�� ������ ȹ�� ��
        else if (col.gameObject.tag == "Fruit")
        {
            PlayerHP = 10;
        }

        // �̴� �κ��� �΋H�� ���
        else if (col.gameObject.tag == "MiniRobot")
        {
            PlayerHP--;
        }
    }

    public IEnumerator HPinfo()
    {
        InfoText.text = "ĳ���Ͱ� �����մϴ�! �����ϼ���";
        yield return new WaitForSeconds(5.0f);
        InfoText.text = "";
    }

    public IEnumerator Keyinfo()
    {
        InfoText.text = "��¥ �����Դϴ�! ��¥ ���踦 ã������";
        yield return new WaitForSeconds(5.0f);
        InfoText.text = "";
    }
}
