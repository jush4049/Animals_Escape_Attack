using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동하게 한다.
//상태를 만들어서 제어하고 싶다.
// Idle : Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
//Run : 타겟방향으로 이동(요원)
//Attack : 일정 시간마다 공격

public class Enemy : MonoBehaviour
{

    //목적지
    public Transform target;
    //요원
    NavMeshAgent agent;

    public Animator anim;

    public GameObject Bullet;
    public Transform FirePos;

    public bool BulletCount = true;
    // - UI
    public Slider sliderHP;
    // - 현재 체력
    int currentHP;
    // - 최대 체력
    public int maxHP;

    public Image bar;

    public GameObject enemy;

    //총알 발사 사운드
    public AudioClip fireSfx;
    //AudioSource 컴포넌트 저장 변수
    private AudioSource source = null;

    public GameObject Key;

    public int HP
    {
        get { return currentHP; }
        set
        {
            //값 변경
            currentHP = value;
            //UI에 변경 적용
            sliderHP.value = currentHP;
        }
    }

    //열거형으로 정해진 상태값을 사용
    enum State
    {
        Idle,
        Run,
        Shoot
    }
    //상태 처리
    State state;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource 컴포넌트 추출, 변수 할당
        source = GetComponent<AudioSource>();

        //생성시 상태를 Idle로 한다.
        state = State.Idle;

        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();

        //생성시 체력을 최대 체력으로 만든다
        currentHP = maxHP;
        sliderHP.maxValue = maxHP;
        sliderHP.value = currentHP;

        StartCoroutine(CountAttackDelay());
    }
    
    // Update is called once per frame
    void Update()
    {
        //만약 state가 idle이라면
        if (state == State.Idle)
        {
            UpdateIdle();
            //Debug.Log("idle 상태");
        }
        else if (state == State.Run)
        {
            UpdateRun();
            //Debug.Log("업데이트런");
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
            Debug.Log("hp감소" + currentHP);
            if (currentHP <= 0)
            {
                //만약 HP가 0이하라면 파괴한다.
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
        //남은 거리가 "distance값 보다 작거나 같은 값"미터라면 공격한다.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 30)
        {
            //타겟 방향으로 이동하다가 
            agent.speed = 4.5f;

            state = State.Shoot;
            anim.SetTrigger("Shoot");
            //Debug.Log("발사중");
            //Debug.Log("발사중");

        }

        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;

    }
    IEnumerator CountAttackDelay()
    {
        while (BulletCount == true)
        {
            source.PlayOneShot(fireSfx, 1.0f);
            Instantiate(Bullet, FirePos.transform.position, FirePos.transform.rotation);
            yield return new WaitForSeconds(1f);
            Debug.Log("총알 소비 - 현재 총알 : " + BulletCount);
        }
    }

    private void UpdateIdle()   
    {
        agent.speed = 0;
        /*//생성될때 목적지(Player)를 찿는다.
        target = GameObject.Find("Player").transform;
        //target을 찾으면 Run상태로 전이하고 싶다.
        if (target != null)
        {
            state = State.Run;
            //이렇게 state값을 바꿨다고 animation까지 바뀔까? no! 동기화를 해줘야한다.
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