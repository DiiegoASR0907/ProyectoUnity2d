using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {
    [Tooltip("esperar x segundos antes de destruir objeto")]
    public float waitBeforeDestroy;
    [HideInInspector]
    public Vector2 mov;

    public float speed;

	void Update ()
    {
		transform.position += new Vector3(mov.x,mov.y,0)*speed*Time.deltaTime;
	}
    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Object")
        {
            yield return new WaitForSeconds(waitBeforeDestroy);
            Destroy(gameObject);
        }else if (col.tag != "Player" && col.tag != "Attack")
        {
            // restar uno de vida si es el enemigo
            if (col.tag == "Enemy") col.SendMessage("Attacked");
            Destroy(gameObject);
        }
    }
}
