using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveReached : MonoBehaviour
{
    [SerializeField]
    private Text _wave;

    private int _waveCounter = Waves.currentWave;
    void Start()
    {
        if (_waveCounter != 0)
        {
            _wave.text = _waveCounter.ToString();
        }
        else
        {
            _wave.text = "0, somehow, you suck";
        }
    }
}
