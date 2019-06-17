using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public SpriteRenderer sprite;
	private Animator anim;
	private string ladoAtual = "E";

	// Use this for initialization
	void Start () {
		//sprite = GetComponents<SpriteRenderer> ();
		anim = GetComponent<Animator> ();		
	}


	// Update is called once per frame
	void Update () {

		if (GameManager1.instance.gameOver) {
			sprite.flipX = false;
			anim.Play ("Die");		
		}
	}

	void TrocaLado(string novaPosicao){
		if (ladoAtual != novaPosicao) {
			ladoAtual = novaPosicao;
			transform.position = new Vector3 (-transform.position.x, transform.position.y,transform.position.z);
			sprite.flipX = !sprite.flipX;
		}
	}

	void OnTriggerEnter2D(Collider2D other){		
		GameManager1.instance.gameOver = true;
		GameManager1.instance.SalvaPontuacao();
	}

	public void Toque(string ladoToque){
		if (!GameManager1.instance.gameOver) {
			TrocaLado (ladoToque);
			anim.Play ("cut");
		}
	}

}
