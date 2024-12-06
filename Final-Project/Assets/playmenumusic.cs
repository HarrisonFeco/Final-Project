using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playmenumusic : MonoBehaviour
{
    public AudioManager AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayRandomMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
