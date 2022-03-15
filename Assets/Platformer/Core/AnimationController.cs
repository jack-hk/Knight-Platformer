using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class AnimationController : MonoBehaviour
{
    void Start()
    {
        
    }

    public void Play(Animation animation)
    {
        if (animation == null)
        {
            Debug.LogError("Animation " + name + " could not be loaded");
            return;
        }

        animation.Play();
    }

    public void Stop(Animation animation)
    {
        if (animation == null)
        {
            Debug.LogError("Animation " + name + " could not be loaded");
            return;
        }

        animation.Stop();
    }
}
