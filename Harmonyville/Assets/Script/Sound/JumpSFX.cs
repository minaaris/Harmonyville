using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSFX : MonoBehaviour
{
public GameObject Jump;
// Start is called before the first frame update
void Start()
{
Jump.SetActive(false);
}
// Update is called once per frame
void Update()
{
    if(Input.GetKey("space"))
    {
        jump();
    }

    if(Input.GetKeyUp("space"))
    {
        stopjump();
    }
}

void jump()
{
    Jump.SetActive(true);
}

 void stopjump()
{
    Jump.SetActive(false);
}
}