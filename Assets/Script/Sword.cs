using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    [Tooltip("velocidad del movimiento en unidades de unity")]
    public float speed;
    public int speedr;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target, dir; // almacenar objetivo y direccion
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        // detectar posicion de jugador
        if (player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // si detecta el objeivo hacia la posicion del jugador
		if (target != Vector3.zero)
        {
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.back,
            speedr*Time.deltaTime);
        }
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        // si la espada choca con un jugador o un ataque se borra
        if (col.transform.tag == "Player" || col.transform.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        // si el objeto sale de la pantalla se borra
        Destroy(gameObject);
    }

}
