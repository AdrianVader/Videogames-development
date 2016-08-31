using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool _SpaceDown;
    private bool _RightDown;
    private bool _LeftDown;

    private bool _RightKill;
    private bool _LeftKill;

    private int _JumpCount;

    private const float JUMP_FORCE = 450.0f;
    private const float SIDE_FORCE = 20.0f;
    private const int MAX_JUMPS = 1;

    // Use this for initialization
    void Start () {
        _SpaceDown = false;
        _RightDown = false;
        _LeftDown = false;

        _RightKill = false;
        _LeftKill = false;

        _JumpCount = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space") && _JumpCount < MAX_JUMPS)
        {
            _SpaceDown = true;
            _JumpCount++;
        }
        else {
            _SpaceDown = false;
        }

        _RightDown = Input.GetKey("right");

        _LeftDown = Input.GetKey("left");

    }

    void FixedUpdate() {

        if (_SpaceDown) {
            GetComponent<Rigidbody>().AddExplosionForce(JUMP_FORCE, transform.position, 0.0f);
            _SpaceDown = false;
        }

        if (_RightDown && ! _RightKill)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * SIDE_FORCE);
        }

        if (_LeftDown && !_LeftKill)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * SIDE_FORCE);
        }

    }

    void OnCollisionStay(Collision collisionInfo)
    {
        _RightKill = false;
        _LeftKill = false;

        for (int i = 0; i < collisionInfo.contacts.Length; i++)
        {
            if (collisionInfo.contacts[i].normal.y >= 0.1f)
            {
                _JumpCount = 0;
            }

            if (collisionInfo.contacts[i].normal.x < -0.1f)
            {
                _RightKill = true;
            }

            if (collisionInfo.contacts[i].normal.x > 0.1f)
            {
                _LeftKill = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Application.LoadLevel("Platforms");
    }
}
