using UnityEngine;
using System.Collections;

public class Parede : MonoBehaviour {
	private Transform filho;

	private bool primeiraVezSelecionado = true;
	private bool primeiraVezDeselecionado = true;

	private bool paredeDesativada = false;

	private Renderer rendParede;
	private BoxCollider colParede;

	private Vector3 tamanhoOriginal;
	public Vector3 posicaoOriginal;
	// Use this for initialization
	void Awake () {
		filho = transform.Find("parede");
		Debug.Log (filho);

		rendParede = filho.transform.GetComponent <Renderer>();
		colParede = filho.transform.GetComponent<BoxCollider>();
		tamanhoOriginal = filho.transform.localScale;
		posicaoOriginal = new Vector3(filho.transform.localPosition.x, filho.transform.localPosition.y - filho.transform.localScale.y/2, filho.transform.localPosition.z);

	}

	void Selecionado(){
		if(primeiraVezSelecionado && !paredeDesativada){
			/*rendParede.material.color = new Color(1f,1f,1f,0.2f);*/
			StartCoroutine(LerpSelecionado ());
			primeiraVezSelecionado = false;
			primeiraVezDeselecionado = true;
		}
	}
	void Deselecionado(){
		if(primeiraVezDeselecionado && !paredeDesativada){
			//rendParede.material.color = new Color(1f,1f,1f,1f);
			StartCoroutine(LerpDeselecionado());
			primeiraVezSelecionado = true;
			primeiraVezDeselecionado = false;
		}
	}
	public void DesativarParede(){
		Selecionado();
		paredeDesativada = true;
	}
	public void AtivarParede(){
		paredeDesativada = false;
		Deselecionado();
	}

	IEnumerator LerpSelecionado()
	{
		float t = 0;
		float metadeTamanho = filho.transform.localScale.y/2;;

		while(t < 0.5)
		{
			t += Time.deltaTime;

			filho.transform.localScale = Vector3.Lerp (tamanhoOriginal,new Vector3(filho.transform.localScale.x,tamanhoOriginal.y*0.1f,filho.transform.localScale.z),  t/0.2f);
			metadeTamanho = filho.transform.localScale.y/2;

			filho.transform.localPosition = Vector3.Lerp (posicaoOriginal,new Vector3(posicaoOriginal.x,posicaoOriginal.y + metadeTamanho/2f ,posicaoOriginal.z),  t/0.2f);
			yield return null;
		}
	}
	
	IEnumerator LerpDeselecionado()
	{
		float t = 0;
		float metadeTamanho = filho.transform.localScale.y/2;;

		while(t < 0.5)
		{
			t += Time.deltaTime;
			
			filho.transform.localScale = Vector3.Lerp (new Vector3(filho.transform.localScale.x,tamanhoOriginal.y*0.1f,filho.transform.localScale.z),tamanhoOriginal,  t/0.2f);
			metadeTamanho = filho.transform.localScale.y/2;

			filho.transform.localPosition = Vector3.Lerp (new Vector3(posicaoOriginal.x,posicaoOriginal.y + (tamanhoOriginal.y*0.1f)/2f ,posicaoOriginal.z) , new Vector3(posicaoOriginal.x,posicaoOriginal.y + tamanhoOriginal.y/2 ,posicaoOriginal.z),  t/0.2f);
			yield return null;
		}

	}
}
