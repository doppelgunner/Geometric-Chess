using UnityEngine;
using System.Collections;

public class SimpleFPSCounter : MonoBehaviour
{

    public bool active = false;

    private float elapsedTime;
    private float numFrames;
    private float fps;

    void Update()
    {
        if (!active) return;
        
        numFrames++;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1f)
        {
            fps = numFrames / elapsedTime;
            numFrames = 0;
            elapsedTime = 0;
        }
        print(Mathf.RoundToInt(fps));
    }
}
