using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2.0f;
    // 충돌 위한 리지드바디
    private Rigidbody bulletRigdbody;

    private void Start()
    {
        // 블릿에 대한 컴포넌트를 가져옵니다.
        bulletRigdbody = GetComponent<Rigidbody>();

        // 리지드바디의 속도 = 앞쪽 방향 * 이동 속도
        bulletRigdbody.velocity = transform.forward * speed;

        // 3초에 뒤에 파괴되는 것을 세팅합니다.
        Destroy(gameObject, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어태그 맞는 체크하는 조건문.
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();

            if (playerController != null)
            {
                //여기에 플레이어 컨트롤에서 죽는 함수가 있어야합니다. 
                //여기에
            }
        }

    }
}
