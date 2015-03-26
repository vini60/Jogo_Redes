using UnityEngine;
using System.Collections;

public class ControlaParedes : MonoBehaviour {
	private GameObject[] paredes;
	private bool paredesAtivas = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public void MudaEstadoParedes(){
		GameObject[] paredes = GameObject.FindGameObjectsWithTag("parede");
		Debug.Log (paredes);
		if(paredesAtivas){
			foreach(GameObject p in paredes){
				Debug.Log (p);
				p.GetComponent<Parede>().DesativarParede();
			}
			paredesAtivas = false;
		}else{
			foreach(GameObject p in paredes){
				p.GetComponent<Parede>().AtivarParede();
			}
			paredesAtivas = true;
		}
	}
}
