using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveReached : MonoBehaviour
{
    [SerializeField]
    private Text _wave;
    void Start()
    {
        Waves _waveCounter = GetComponent<Waves>();
        if (_waveCounter != null)
        {
            _wave.text = _waveCounter.waveCounterText.ToString();
        }
        else
        {
            _wave.text = "0, somehow, you suck";
        }
    }
}
