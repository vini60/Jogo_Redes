using UnityEngine;
using System.Collections;

public class Computador : MonoBehaviour {
	private float tempoEspera;
	
	public float maiorSinal;
	
	private int contListaEmissores;
	private int contListaEmissores_Receptores;
	private int contListaEmissores_Anterior;
	private int contListaEmissores_Receptores_Anterior;
	
	private Transform listaEmissores;
	private Transform listaEmissores_Receptores;
	
	private RaycastHit hit;

	private Renderer rend;
	
	// Use this for initialization
	void Awake(){
		rend = this.GetComponent<Renderer>();
		maiorSinal = 0;
		contListaEmissores = 0;
		contListaEmissores_Receptores = 0;
		listaEmissores = GameObject.Find("emissores").transform;
		listaEmissores_Receptores = GameObject.Find ("emissores/receptores").transform;
	}
	void Start () {
		contListaEmissores_Anterior = 0;
		contListaEmissores_Receptores_Anterior = 0;
	}
	
	void Update () {
		tempoEspera += Time.deltaTime;
		contListaEmissores = 0;
		contListaEmissores_Receptores = 0;
		if(tempoEspera >= 1){
			/*foreach(Transform child in listaEmissores){
				contListaEmissores++;
			}
			foreach(Transform child in listaEmissores_Receptores){
				contListaEmissores_Receptores++;
			}
			if(contListaEmissores_Anterior !=  contListaEmissores || contListaEmissores_Receptores_Anterior !=  contListaEmissores_Receptores){
				BuscaMelhorSinal();
			}*/
			tempoEspera = 0;
			MudarCor();
		}
	}
	private void MudarCor(){
		Color cor = new Color(0f, maiorSinal, 0F, 1F);
		rend.material.color = cor;
	}
	
	private void BuscaMelhorSinal(){
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
			}
		}
		
		contListaEmissores_Anterior = contListaEmissores;
		contListaEmissores_Receptores_Anterior = contListaEmissores_Receptores;
	}
}