using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    //System.Numerics.Vector2 createPos = System.Numerics.Vector2.Zero;   //적이 랜덤으로 생성할 위치

    // 적 AI
    private NavMeshAgent pathfinder;

    // 추적 대상
    private Character targetEntity;

    // 애니메이터 컴포넌트
    private Animator enemyAnimator;

    // 렌더러 컴포넌트 
    private Renderer renderer;

    // 최소 생성 주기
    public float bulletspawnPateMin;

    // 최대 생성 주기
    public float bulletspawnPateMax;

    // 추척대상을 검색하기위한 레이어 마스크
    public LayerMask whatIsTarget;

    //총알 생성 주기
    public float timeBeAttack = 0.5f;

    private float lastAttackTime;


    protected override void Awake()
    {
        base.Awake();

        // 컴포넌트 가져오기
        pathfinder = GetComponent<NavMeshAgent>(); 
        enemyAnimator = GetComponent<Animator>();
        renderer = GetComponentInChildren<Renderer>();
    }
    private void Start()
    {
        // 게임 오브젝트가 활성화하면서 동시에 AI가 player를 추적를 시작합니다.
        StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        // 여기서 애니메이션 관련
        enemyAnimator.SetBool("HasTarget", hasTarget);
    }

    private IEnumerator UpdatePath()
    {
        // 여기서 isDead이 문제가 발생하면 상속에서 문제가 발생할수있습니다.
        while(!isDead)
        {
            if(hasTarget)
            {
                // 추적대상 존재 : 경로를 갱신하고 AI이동을 계속 진행
                pathfinder.isStopped = false;
                pathfinder.SetDestination(targetEntity.transform.position);
            }
            
            else
            {
                // 추적대상이 없음 :AI 이동 중지;
                pathfinder.isStopped = true;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20.0f, whatIsTarget);
                foreach (Collider2D collider in colliders)
                {
                    // 콜라이더로부터 livingEntity 컴포넌트가 가져오기
                    Character character = collider.GetComponent<Character>(); 
                   
                    // 만약 컴포넌트가 존재하면 그리고 살아있다면 이것은아직 캐릭터에 구현이 안됨.
                    if(character != null && !character.isDead)
                    {
                        // 추적 대상 설정 
                        targetEntity = character;

                        // 반복문 탈출
                        break;
                    }

                }

            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private bool hasTarget
    {
        get
        {
            // 추적상태 있고 거리를 알고 살아있을때
            if(targetEntity !=  null && !targetEntity.isDead)
            {
                return true;
            }

            // 그렇지 않으면 false
            return false;
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        //자신이 사망하지 않았다면
        if(!isDead && Time.time >= lastAttackTime + timeBeAttack)
        {
            Character attackTarget = other.GetComponent<Character>();

            // 비어있지 않거나 타켓이 플레이어일때.
            if(attackTarget != null && attackTarget == targetEntity)
            {
                // 최근 공격시간 갱신
                lastAttackTime = Time.time;
                Vector2 hitpoint  = other.ClosestPoint(transform.position);
                Vector2 hitNormal = transform.position -  other.transform.position;
                
                // 이부분으 캐릭터 부분에서 만드는 것이다.
                // attackTarget.OnDamage(hitpoint, hitNormal);
            }
        }
    }
  
    // 만약 이게 오류가 난다며 character에 부분에서 dead가 버추얼로 만들어지지 않았기때문에
    public override void Dead()
    {
        base.Dead();

        // 다른 AI이가 방해 하지 않도록 자신의 콜라이더 비활성화
        Collider2D[] enemyColliders =GetComponents<Collider2D>();
        for(int i = 0; i < enemyColliders.Length; i++) 
        {
            enemyColliders[i].enabled = false; 
        }

        pathfinder.isStopped = true;
        pathfinder.enabled = false;

        // 음악을 넣을 거면 이 뒤에다가.
    }

}