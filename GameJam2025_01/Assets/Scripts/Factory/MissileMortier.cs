using UnityEngine;

public class MissileMortier : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] int damage;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject missileBody;
    [SerializeField] GameObject noSignal;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = rb.velocity.normalized;

        if (direction != Vector3.zero)
        {
            Quaternion rotationCible = Quaternion.LookRotation(direction);
            transform.rotation = rotationCible;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(missileBody);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        Camera cam = GetComponentInChildren<Camera>();
        //cam.gameObject.SetActive(false);
        ExplosionMissile();
        noSignal.SetActive(true);
    }

    void ExplosionMissile()
    {
        Vector3 explosionPosition = transform.position;

        // Find all colliders in the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionPosition, radius, layerMask);

        if (hitColliders.Length > 0)
        {

            foreach (Collider hit in hitColliders)
            {
                Debug.Log(hit.gameObject.name);
                hit.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                hit.gameObject.GetComponent<Boss>().TakeDamage(damage);
            }
        }

        Destroy(gameObject, 5f);
    }
}
