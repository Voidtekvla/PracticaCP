using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ZombieController : MonoBehaviour
{
    public float speedWalking = 5.0F;
    public float speedRunning = 10.0f;
    public float rotateSpeed = 3.0F;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        float curSpeed = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeed *= speedRunning;
        }
        else
        {
            curSpeed *= speedWalking;
        }
        controller.SimpleMove(forward * curSpeed);

        anim.SetFloat("speed", curSpeed);
    }
}
