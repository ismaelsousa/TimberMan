using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void iniciaJogo(){
		SoundManger.instance.PlayFx (SoundManger.instance.fxPlay);
		SceneManager.LoadScene("Game");
	}
}
