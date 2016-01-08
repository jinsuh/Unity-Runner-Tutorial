using UnityEngine;
using System;

public class RunnerScript : MonoBehaviour {

    public event Action LandedOnPlatform = delegate{};
    public event Action FellIntoTheVoid = delegate { };

    public Vector3 Acceleration, JumpVelocity;

    private bool _IsTouchingPlatform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_IsTouchingPlatform && Input.GetButtonDown("Jump"))
        {
            this.GetComponent<Rigidbody>().AddForce(JumpVelocity, ForceMode.VelocityChange);
        }

        if (this.transform.position.y < -100)
        {
            FellIntoTheVoid.Invoke();
        }
	}

    void FixedUpdate()
    {
        if (_IsTouchingPlatform)
        {
            var rigidbody = this.GetComponent<Rigidbody>();
            rigidbody.AddForce(Acceleration, ForceMode.Acceleration);
        }
    }

    void OnCollisionEnter()
    {
        _IsTouchingPlatform = true;
        LandedOnPlatform.Invoke();
    }

    void OnCollisionExit()
    {
        _IsTouchingPlatform = false;
    }
}
