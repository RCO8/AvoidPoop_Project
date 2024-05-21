using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidBody;
    private InputController controller;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidBody = GetComponent<Rigidbody2D>();
        controller = GetComponent<InputController>();
    }

    private void Start()
    {
        healthSystem.OnDeath += Death;
    }

    private void Death()
    {
        rigidBody.velocity = Vector2.zero;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;

            color = Color.gray;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
            behaviour.enabled = false;

        controller.DestroyObject();
    }
}