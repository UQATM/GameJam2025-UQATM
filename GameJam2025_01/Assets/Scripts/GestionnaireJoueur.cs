using UnityEngine;

public class GestionnaireJoueur : MonoBehaviour
{
    [SerializeField] KeyCode choisirCadran;
    [SerializeField] KeyCode reduire; //Retour au niveau supérieur
    [SerializeField] KeyCode agrandir; //Contrôler tank

    public struct Keybinds
    {
        public KeyCode ChoisirCadran;
        public KeyCode Reduire;
        public KeyCode Agrandir;
    }

    public enum State
    {
        factory,
        towerDefence,
        tank
    }

    State activeState;

    // Start is called before the first frame update
    void Start()
    {
        activeState = State.factory;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public State getActiveState()
    {
        return activeState;
    }

    public void setActiveState(State _newState)
    {
        activeState = _newState;
    }

    public Keybinds getKeybinds()
    {
        return new Keybinds { ChoisirCadran = choisirCadran, Reduire = reduire, Agrandir = agrandir };
    }

}
