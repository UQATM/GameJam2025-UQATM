using UnityEngine;

public class MissileMortier : MonoBehaviour
{
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
        Destroy(this.gameObject);
    }
}
