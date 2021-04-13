using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BowlingBall b = other.GetComponent<BowlingBall>();
        if (b != null)
        {
            b.Deactivate();
        }
    }
}
