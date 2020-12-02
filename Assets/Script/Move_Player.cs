using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Player : MonoBehaviour {
    public float speed = 4f;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;
    public GameObject slashprefab;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Movements();
        Animations();
        
        //Vector3 mov = new Vector3
        //    (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
        //transform.position = Vector3.MoveTowards
        //    (transform.position, transform.position + mov, speed * Time.deltaTime);

    }
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime); 
    }
    void Movements()
    {
        mov = new Vector2
        (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void Animations()
    {
        if (mov != Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }
    void SlashAttack()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool loading = stateInfo.IsName("Player_Slash");

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("cargando ataque");
            anim.SetTrigger("loading"); }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Debug.Log("atacando");
            anim.SetTrigger("attacking");
            // ROTACION A PARTIR DE UN VECTOR
            float angle = Mathf.Atan2(anim.GetFloat("movY"), anim.GetFloat("movX"))
                * Mathf.Rad2Deg;
            //Instanciamos el objeto
            GameObject slashObj = Instantiate(
                slashprefab, transform.position, Quaternion.AngleAxis
                (angle, Vector3.forward));
            // MOVIMIENTO INICIAL
            Slash slash = slashObj.GetComponent<Slash>();
            slash.mov.x = anim.GetFloat("movX");
            slash.mov.y = anim.GetFloat("movY");
        }
    }
}
