using System.Collections;
using UnityEngine;

public class TankCannon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private GameObject _cannonEnd;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private int cooldownTime = 3;

    public bool isCooldown = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isCooldown)
        {
            GameObject b = Instantiate(_bullet, _cannonEnd.transform.position, _cannonEnd.transform.rotation);
            b.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward) * 25f, ForceMode.Impulse);
            isCooldown = true;
            if (isCooldown == true)
            {
                StartCoroutine(CoolDownhappening());
            }
            Destroy(b, 5f);
        }

        Vector3 mouse = Input.mousePosition;
        mouse.x = Mathf.Clamp(mouse.x, 0, Screen.width);
        mouse.y = Mathf.Clamp(mouse.y, 25, (Screen.height - 250));
        Vector3 mouseWorld = cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, _cannonEnd.transform.position.y + 90));

        transform.LookAt(mouseWorld);
    }

    public IEnumerator CoolDownhappening()
    {
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

}
