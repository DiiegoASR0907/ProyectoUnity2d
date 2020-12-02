using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour {
    public float visionRadius;
    public float attackRadius;
    public float speed;

    [Tooltip("prefab de la espada")]
    public GameObject swordPrefab;
    [Tooltip("Vleocidad de ataque (tiempo entre repeticion")]
    public float attackSpeed = 2f;
    bool attacking;
    // vida del enemigo
    [Tooltip("puntos de vida")]
    public int maxHP = 3;
    [Tooltip("vida actual")]
    public int hp;
    // Guardar al jugador
    GameObject player;
    //guardar posicion incial
    Vector3 initialPosition, target;
    Animator anim;
    Rigidbody2D rb2d;
	void Start ()
    {
        // detectar el jugador con el tag
        player = GameObject.FindGameObjectWithTag("Player");
        // almacenar posicion incial enemigo
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        // iniccializar la vida del enemigo con la maxima
        hp = maxHP;
	}

    // Update is called once per frame
    void Update()
    {
        // el target siempre sera la posicion inicial
        Vector3 target = initialPosition;
        // comprobación del raycast del enemigo al jugador
        RaycastHit2D hit = Physics2D.Raycast
            (transform.position, player.transform.position - transform.position,
            visionRadius, 1 << LayerMask.NameToLayer("Default"));
        Vector3 forward = transform.TransformDirection(player.transform.position -
            transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
        // si el raycast detecta el jugador cambia la posicion
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                target = player.transform.position;
            }
        }
        // calcular distancia de enemigo a jugador
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;
        // si el enemigo esta en rango de ataque se detiene y lo ataca
        if (target != initialPosition && distance < attackRadius)
        {
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Enemy_Walk", -1,0);// detiene la animacion de caminar
            
            if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        else // si es falso el enemigo se mueve al jjugador
        {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }
        //  evitar errores al retorno de la pocicion inicial dandole un rango de error
        if (target == initialPosition && distance < 0.02f)
        {
            transform.position = initialPosition;
            anim.SetBool("walking", false);
        }
        Debug.DrawLine(transform.position, target, Color.green);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    IEnumerator Attack(float seconds)
    {
        string s = target.ToString() + swordPrefab.ToString();
        Debug.Log(s);
        attacking = true;
        if (target != initialPosition && swordPrefab != null)
        {
            Instantiate(swordPrefab, transform.position, transform.rotation);
            Debug.Log("entrando a creación de espada");
            yield return new WaitForSeconds(seconds);
        }
        attacking = false;
    }
    // por cada ataque resta 1 de vida
    public void Attacked()
    {
        if (--hp <= 0) Destroy(gameObject);
    }
    // mostrar vidas del enemigo en una barra
    void OnGUI()
    {
        // almacenar posicion enemigo respecto a la camara
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(
        new Rect(pos.x - 20, Screen.height - pos.y + 60, 40, 24), hp + "/" + maxHP);
    }

}
