using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class BallBounce : MonoBehaviour
{

    public float m_ForcePower = 10;
    private Rigidbody m_RigidBody;

	// Use this for initialization
	void Awake ()
	{
	    m_RigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown()
    {
        Debug.Log("CLICKED!");
        m_RigidBody.AddForce(new Vector3(Random.Range(-1f, 1f),1,0) * m_ForcePower);
    }
}
