using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform birdRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f; //Bullet por segundo


    private Transform target;
    private float timeUntilFire;

     private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target=null;
        }
        else {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot() {

        Debug.Log("Shoot");

        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        

    }

    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        //birdRotationPoint.rotation = targetRotation;
        birdRotationPoint.rotation = Quaternion.RotateTowards(birdRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}
