using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//���(agent=enemy)���� �������� �˷��༭ �������� �̵��ϰ� �Ѵ�.
//���¸� ���� �����ϰ� �ʹ�.
// Idle : Player�� ã�´�, ã������ Run���·� �����ϰ� �ʹ�.
//Run : Ÿ�ٹ������� �̵�(���)
//Attack : ���� �ð����� ����

public class Enemy : MonoBehaviour
{

    //������
    public Transform target;
    //���
    NavMeshAgent agent;

    public Animator anim;

    public GameObject Bullet;
    public Transform FirePos;

    public bool BulletCount = true;
    // - UI
    public Slider sliderHP;
    // - ���� ü��
    int currentHP;
    // - �ִ� ü��
    public int maxHP;

    public Image bar;

    public GameObject enemy;

    //�Ѿ� �߻� ����
    public AudioClip fireSfx;
    //AudioSource ������Ʈ ���� ����
    private AudioSource source = null;

    public GameObject Key;

    public int HP
    {
        get { return currentHP; }
        set
        {
            //�� ����
            currentHP = value;
            //UI�� ���� ����
            sliderHP.value = currentHP;
        }
    }

    //���������� ������ ���°��� ���
    enum State
    {
        Idle,
        Run,
        Shoot
    }
    //���� ó��
    State state;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource ������Ʈ ����, ���� �Ҵ�
        source = GetComponent<AudioSource>();

        //������ ���¸� Idle�� �Ѵ�.
        state = State.Idle;

        //����� �������༭
        agent = GetComponent<NavMeshAgent>();

        //������ ü���� �ִ� ü������ �����
        currentHP = maxHP;
        sliderHP.maxValue = maxHP;
        sliderHP.value = currentHP;

        StartCoroutine(CountAttackDelay());
    }
    
    // Update is called once per frame
    void Update()
    {
        //���� state�� idle�̶��
        if (state == State.Idle)
        {
            UpdateIdle();
            //Debug.Log("idle ����");
        }
        else if (state == State.Run)
        {
            UpdateRun();
            //Debug.Log("������Ʈ��");
        }
        else if (state == State.Shoot)
        {
            UpdateAttack();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Banana")
        {
            currentHP--;
            bar.fillAmount = currentHP / 5f;
            Debug.Log("hp����" + currentHP);
            if (currentHP <= 0)
            {
                //���� HP�� 0���϶�� �ı��Ѵ�.
                Destroy(enemy);
                Instantiate(Key, enemy.transform.position, enemy.transform.rotation);
            }
        }
    }

    private void UpdateAttack()
    {
        //agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 5)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }

    private void UpdateRun()
    {
        agent.speed = 0;
        //���� �Ÿ��� "distance�� ���� �۰ų� ���� ��"���Ͷ�� �����Ѵ�.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 30)
        {
            //Ÿ�� �������� �̵��ϴٰ� 
            agent.speed = 4.5f;

            state = State.Shoot;
            anim.SetTrigger("Shoot");
            //Debug.Log("�߻���");
            //Debug.Log("�߻���");

        }

        //������� �������� �˷��ش�.
        agent.destination = target.transform.position;

    }
    IEnumerator CountAttackDelay()
    {
        while (BulletCount == true)
        {
            source.PlayOneShot(fireSfx, 1.0f);
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
            yield return new WaitForSeconds(1f);
            Debug.Log("�Ѿ� �Һ� - ���� �Ѿ� : " + BulletCount);
        }
    }

    private void UpdateIdle()   
    {
        agent.speed = 0;
        /*//�����ɶ� ������(Player)�� �O�´�.
        target = GameObject.Find("Player").transform;
        //target�� ã���� Run���·� �����ϰ� �ʹ�.
        if (target != null)
        {
            state = State.Run;
            //�̷��� state���� �ٲ�ٰ� animation���� �ٲ��? no! ����ȭ�� ������Ѵ�.
            anim.SetTrigger("Run");
            
        }*/
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 30)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }
}