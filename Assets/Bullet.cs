using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializedField] private RigidBody2D rb;
    [Header("Attributes")]
    [SerializedField] private float bulletSpeed = 5f;
    public Transform target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public vois SetTarget(Transform _target) {
        target = _target;

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

    }

    private void OnCollitionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
