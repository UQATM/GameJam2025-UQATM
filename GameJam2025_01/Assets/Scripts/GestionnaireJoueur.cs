using UnityEngine;

public class GestionnaireJoueur : MonoBehaviour
{
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
}
