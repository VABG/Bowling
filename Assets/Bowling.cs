using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    //Total 10
    List<GameObject> pins;
    Camera cam;
    [SerializeField] GameObject bowlingPin;
    [SerializeField] BowlingBall ball;
    [SerializeField] float offset;

    bool hasShot = false;
    int pinsLeft = 0;
    int shot = 0;    

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        MakePins();
    }

    void MakePins()
    {
        pins = new List<GameObject>(10);
        // Place bowling pins
        for (int z = 0; z < 4; z++)
        {
            for (int x = 0; x < z + 1; x++)
            {
                float xOffset = -((float)z / 2) * offset;
                GameObject p = Instantiate(bowlingPin, new Vector3(x * offset + xOffset, .33f, z * offset), transform.rotation, transform);
                pins.Add(p);
            }
        }
        pinsLeft = pins.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasShot)
        {
            DoInput();
        }

        else if (!IsDone())
        {
            int pinsHit  = CheckPinsDown();
            pinsLeft -= pinsHit;
            Debug.Log(pinsHit);
            ball.ResetPosition();
            ClearFallenPins();
            hasShot = false;
            shot++;
            // Send message to screen!
        }

        if (pinsLeft < 1 || shot > 2)
        {
            Reset();
            //Send message to screen!
        }
    }

    void Reset()
    {
        ClearAllPins();
        MakePins();
        shot = 0;
    }

    void DoInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.Shoot();
            hasShot = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ball.transform.Rotate(Vector3.up, -Time.deltaTime*25);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ball.transform.Rotate(Vector3.up, Time.deltaTime*25);
        }
    }

    bool IsDone()
    {
        if (ball.CheckIfActive()) return true;
        return false;
    }

    int CheckPinsDown()
    {
        int count = 0;
        for (int i = 0; i < pins.Count; i++)
        {
            if (IsStanding(pins[i])) count++;
        }        
        return pins.Count-count;
    }

    void ClearFallenPins()
    {
        for (int i = pins.Count-1; i > -1; i--)
        {
            if (!IsStanding(pins[i]))
            {
                Destroy(pins[i].gameObject);
                pins.RemoveAt(i);
            }
        }
    }

    void ClearAllPins()
    {
        for (int i = pins.Count - 1; i > -1; i--)
        {
            Destroy(pins[i].gameObject);
            pins.RemoveAt(i);
        }
    }

    bool IsStanding (GameObject o)
    {
        float dir = Vector3.Dot(Vector3.up, o.transform.up);
        if (dir < .6) return false;
        return true;
    }
}
