using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour {

	public static GameManager1 instance;

	public GameObject[] troncos;
	public List<GameObject> listaTroncos;

	//variaveis da arvore
	private float alturaTronco = 2.43f;
	private float posicaoInicailY = -2.38f;
	private int maxTrocos = 6;
	private bool TroncoSemGalho = false;

	public Text pontuacao;
	private int pontos = 0;

	public Image barraTempo;
	public float larguraBarra = 188f;
	private float tempoJogo = 10f;
	private float tempoExtra = 0.115f;		
	private float tempoAtual;

	public bool gameOver = false;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		InicializaTroncos ();
		tempoAtual = tempoJogo;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameOver) {			
			DiminuiBarra ();
			/*if (Input.GetButtonDown ("Left") || Input.GetButtonDown ("Right")) {				
				CortaTronco ();
				ReposicionaTronco ();
				SomaPontos ();
				somaTempo ();
			}*/

		}
	}

	void CriarTroncos(int posicao){
		GameObject tronco = Instantiate(TroncoSemGalho ? troncos[Random.Range(0,3)] : troncos [0]);
		tronco.transform.localPosition = new Vector3(0f, posicaoInicailY+posicao*alturaTronco, 0f);
		listaTroncos.Add(tronco);
		TroncoSemGalho = !TroncoSemGalho;
	}

	void InicializaTroncos(){
		for (int posicao = 0; posicao <= maxTrocos; posicao++) {
			CriarTroncos (posicao);
		}
	}

	void CortaTronco(){
		SoundManger.instance.PlayFx (SoundManger.instance.fxCorte);	
		Destroy (listaTroncos [0]);
		listaTroncos.RemoveAt (0);

	}

	void ReposicionaTronco(){
		for (int posicao = 0; posicao < listaTroncos.Count; posicao++) {
			listaTroncos [posicao].transform.localPosition = new Vector3 (0f, posicaoInicailY + posicao * alturaTronco, 0f);
		}

		CriarTroncos (maxTrocos);
			
	}
	void somaTempo(){
		if (tempoAtual + tempoExtra < tempoJogo) {
			tempoAtual += tempoExtra;
		}
	
	}

	void SomaPontos(){
		pontos++;
		pontuacao.text = pontos.ToString ();
	}

	void DiminuiBarra(){
		tempoAtual -= Time.deltaTime;
		float tempo = tempoAtual / tempoJogo;
		float pos = larguraBarra - (tempo * larguraBarra);
		barraTempo.transform.localPosition = new Vector2 (-pos, barraTempo.transform.localPosition.y);
		if (tempoAtual <= 0) {
			gameOver = true;
			SalvaPontuacao ();

		}
	}

	public void SalvaPontuacao(){
		if (PlayerPrefs.GetInt ("Best") < pontos) {
			PlayerPrefs.SetInt ("Best", pontos);
		}
		PlayerPrefs.SetInt ("score",pontos);
		SoundManger.instance.PlayFx (SoundManger.instance.fxMorte);
		Invoke ("ChamaCenaGameOver", 2f);
	}
	public void ChamaCenaGameOver(){
		SceneManager.LoadScene ("GameOver");
	}

	public void Toque(){
		if (!gameOver) {								
				CortaTronco ();
				ReposicionaTronco ();
				SomaPontos ();
				somaTempo ();
		}
	}
}
