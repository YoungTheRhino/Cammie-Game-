using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    public bool grounded;

    float horizontalscale;
    float horizontalaxis;
    float verticalscale;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        horizontalscale = transform.localScale.x;
        verticalscale = transform.localScale.y;
    }
    void Update () {
        Movement();
      //animator.SetBool("Grounded", grounded);
	}
    void Movement()
    {
        horizontalaxis = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalaxis));
        if (horizontalaxis < 0)
            transform.localScale = new Vector2 (-horizontalscale, verticalscale);
            if (horizontalaxis > 0) 
            transform.localScale = new Vector2(horizontalscale, verticalscale);
            
    }
}
