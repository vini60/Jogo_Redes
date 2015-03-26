using UnityEngine;
using System.Collections;

public class AccessPoint : MonoBehaviour {
	private float abrangencia = 40f;

	private float tempoEspera= 1.5f;

	public float maiorSinal = 0;

	private Transform listaEmissores;
	private Transform listaEmissores_Receptores;

	private LayerMask parede;
	private RaycastHit hit;

	private Transform alcance;
	private Renderer rendAlcance;


	// Use this for initialization
	void Awake(){
		alcance = transform.Find("Alcance");
		rendAlcance = alcance.GetComponent<Renderer>();

		listaEmissores = GameObject.Find("emissores").transform;
		listaEmissores_Receptores = GameObject.Find ("emissores/receptores").transform;
	}

	void Start(){
		BuscaMelhorSinal();
	}
	
	private void BuscaMelhorSinal(){
		float sinalAnterior = maiorSinal;
		maiorSinal = 0;
		foreach(Transform child in listaEmissores){
			float intensidadeSinal = 0;
			if(child.CompareTag("roteador")){
				intensidadeSinal = child.GetComponent<Roteador>().RetornaIntensidade(this.transform);
			}
			if(intensidadeSinal > maiorSinal){
				maiorSinal = intensidadeSinal;
			}
		}
		foreach(Transform child in listaEmissores_Receptores){	
			if(child != this.transform){
				float intensidadeSinal = 0;
				if(child.CompareTag("access_point")){
					intensidadeSinal = child.GetComponent<AccessPoint>().RetornaIntensidade(this.transform);
				}/*if(child.CompareTag("repetidor")){
					intensidadeSinal = child.GetComponent<Repetidor>().RetornaIntensidade(this.transform);
				}*/
				if(intensidadeSinal > maiorSinal){
					maiorSinal = intensidadeSinal;
				}
				Debug.Log ("ACCHOU ACESSPOINT" + intensidadeSinal);
			}
		}
		Vector3 tamInicial = alcance.localScale;
		Vector3 tamFinal = new Vector3(abrangencia*2.0f,abrangencia*2.0f,abrangencia*2.0f);

		Color corInicial = rendAlcance.material.color;
		Color corFinal = new Color(0f,maiorSinal*5,0f,0.1f);

		if(maiorSinal > sinalAnterior){ //SINAL AUMENTOU
			StartCoroutine(AumentaCirculo(tamInicial,tamFinal,corInicial,corFinal));
		}else if(maiorSinal < sinalAnterior){ //SINAL DIMINUIU
			StartCoroutine(AumentaCirculo(tamFinal,tamInicial,corFinal,corInicial));
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
		if(distanciaEmissor < abrangencia){
			intensidadeSinal = ((maiorSinal) - (numParedes*0.1f));
			Debug.Log (receptor+ "numparedes" + numParedes + "maiosinal" + intensidadeSinal);
			if(intensidadeSinal > 0)
				return intensidadeSinal;
		}
		return 0.0f;
	}

	IEnumerator AumentaCirculo(Vector3 tamInicial,Vector3 tamFinal, Color corInicial, Color corFinal)
	{
		float t = 0;
		while(t < 2)
		{
			t += Time.deltaTime;
			alcance.localScale = Vector3.Lerp (tamInicial,tamFinal,  t/2.0f);
			rendAlcance.material.color = Color.Lerp (corInicial,corFinal,  t/2.0f);
			yield return null;
		}
		alcance.localScale = tamFinal;
	}

	public void TirarConexao(){
		maiorSinal = 0;
	}
}
