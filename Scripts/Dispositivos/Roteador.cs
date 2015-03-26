using UnityEngine;
using System.Collections;

public class Roteador : MonoBehaviour {
	[Range(0.0f, 50.0f)]
	public int abrangenciaSinal;
	private Transform alcance;

	private float tempoEspera = 5f;

	private bool primeiraVez = true;

	//Centro da conexao, apenas um pode ser instanciado.

	void Awake () {
		alcance = transform.Find("Alcance");
		alcance.gameObject.SetActive(true);
		StartCoroutine(AumentaCirculo());
	}
	
	// Update is called once per frame
	void Update () {
		tempoEspera += Time.deltaTime;
		if(tempoEspera >= 5){
			tempoEspera = 0;
		}else{

		}
	}

	public float RetornaIntensidade(Transform receptor){
		int numParedes = 0;
		float intensidadeSinal;
		float distanciaEmissor = Vector3.Distance (transform.position,receptor.position);
		RaycastHit[] objetosAtingidos = Physics.RaycastAll(transform.position, (receptor.position - transform.position));
		foreach(RaycastHit objs in objetosAtingidos){
			if(Vector3.Distance (transform.position , objs.transform.position) < distanciaEmissor){
				if(objs.transform.CompareTag ("parede")){
					numParedes++;
				}
			}
		}

		intensidadeSinal = ((abrangenciaSinal) - (numParedes*2))/abrangenciaSinal;
		if(intensidadeSinal > 0)
			return intensidadeSinal;
		return 0.0f;
	}

	IEnumerator AumentaCirculo()
	{
		float t = 0;
		Vector3 tamInicial = new Vector3(0.0f,0.0f,0.0f);
		Vector3 tamFinal = new Vector3(abrangenciaSinal*2.0f,abrangenciaSinal*2.0f,abrangenciaSinal*2.0f);
		while(t < 2)
		{
			Debug.Log ("frame");
			t += Time.deltaTime;
			alcance.localScale = Vector3.Lerp (tamInicial,tamFinal,  t/2.0f);
			yield return null;
		}
		alcance.localScale = tamFinal;
	}
}
