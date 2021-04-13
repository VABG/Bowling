using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    Rigidbody rb;
    Vector3 ballStartPos;
    Quaternion ballStartRot;
    bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = transform.position;
        ballStartRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 500;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (rb.IsSleeping()) active = false;
        }
    }

    public void Shoot()
    {
        rb.AddForce(transform.forward * 5, ForceMode.VelocityChange);
        rb.AddRelativeTorque(new Vector3(5, 0, 0), ForceMode.VelocityChange);
        active = true;
        ShowArrow(false);
    }

    public bool CheckIfActive()
    {        
        return active;
    }

    public void ShowArrow(bool show)
    {
        arrow.SetActive(show);
    }

    public void ResetPosition()
    {
        transform.position = ballStartPos;
        transform.rotation = ballStartRot;
        ShowArrow(true);
    }

    public void Deactivate()
    {
        active = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
