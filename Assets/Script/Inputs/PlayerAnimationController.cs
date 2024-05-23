using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private static readonly int isInvincibillity = Animator.StringToHash("isInvincibillity");

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnInvincibillity()
    {
        animator.SetBool(isInvincibillity, true);
    }

    public void OffInvincibillity()
    {
        animator.SetBool(isInvincibillity, false);
    }
}