using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceGame : MonoBehaviour
{
    private void Awake()
    {
        if (!AudioManager.Instance)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialze(audioSource);
         
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
