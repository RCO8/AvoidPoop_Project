using UnityEngine;

public class Character : MonoBehaviour
{
    protected Rigidbody2D rgb2D;
    protected Vector2 characterMovement;
    protected Vector2 targetRotation = Vector2.right;

    protected CharacterStatsHandler statsHandler;

    protected HealthSystem healthSystem;

    public bool IsDead {  get; protected set; }

    protected AudioSource audioSource;
    [SerializeField] protected AudioClip deathClip;
    [SerializeField] protected AudioClip shootClip;

    protected virtual void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        statsHandler = GetComponent<CharacterStatsHandler>();
        healthSystem = GetComponent<HealthSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {
         healthSystem.OnDeath += Dead;
    }

    protected virtual void FixedUpdate()
    {
        Moving(characterMovement);
        ViewTarget(targetRotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("상대 bullet")) IsDead = true;
        //Debug.Log("총알 맞음 : "+ collision.tag);
    }

    protected virtual void Shooting(AttackSO attackSO)
    {
        //발사하는 총알 생성 (오브젝트 풀링)
        //if(isShot) 총알발사
    }
    protected void Moving(Vector2 move)
    {
        //상위클래스는 move값을 charcterMovement에 담기만 하고
        //실제 이동은 하위 클래스에서.
        //이동방향 * speed;
        characterMovement = move;
    }
    protected void ViewTarget(Vector2 target)
    {
        targetRotation = target;
    }

    protected virtual void Dead()
    {
        //공통적으로 파괴를 하면 좋을 것 같은데
        //연출차이로?
    }
}
