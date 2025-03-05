using UnityEngine;

public class TankControl : MonoBehaviour
{
    [SerializeField] float vitesseMax;
    [SerializeField] float vitesseRotation;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] Transform head;
    [SerializeField] float sensibilite;
    Transform root;
    float input;
    float vitesse = 0f;
    float rotation = 0f;
    float rotationTeteX;
    float rotationTeteY;


    // Start is called before the first frame update
    void Start()
    {
       root = gameObject.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxis("Vertical");
        float vitesseCible = input * vitesseMax;


        if(input != 0)
        {
            vitesse = DeplacerTank(vitesse, vitesseCible, acceleration * Time.deltaTime);
        }else
        {
            vitesse = DeplacerTank(vitesse, 0f, acceleration * Time.deltaTime);
        }

        rotation = Input.GetAxis("Horizontal") * vitesseRotation;

        if (input < 0)
        {
            rotation = -rotation;
        }

        float mouvementX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensibilite;
        float mouvementY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensibilite;

        rotationTeteX -= mouvementX;
        rotationTeteY -= mouvementY;

        rotationTeteY = Mathf.Clamp(rotationTeteY, -15f, 2f);
    }

    void FixedUpdate()
    {
        root.Translate(0, 0, vitesse);
        root.Rotate(0, rotation, 0);

        head.localRotation = Quaternion.Euler(rotationTeteY, -rotationTeteX, 0f);
    }

    float DeplacerTank(float _current, float _cible, float _acceleration)
    {
        if (_current < _cible)
        {
            return Mathf.Min(_current + _acceleration, _cible);
        }else if (_current > _cible)
        {
            return Mathf.Max(_current - _acceleration, _cible);
        }

        return _cible;
    }
}
