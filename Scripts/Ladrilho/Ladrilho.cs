using UnityEngine;
using System.Collections;

public class Ladrilho : MonoBehaviour {
	//public Transform dispositivo;
	//public Transform[] dispositivosInstalados;
	//public int numDispositivos;
	public GameObject menuDispositivos;
	public Transform[] dispositivos;

	private GameObject dispositivoInstanciado;

	private Renderer rend;
	private Color corInicial;

	private RaycastHit hit;

	private bool _ativarMenu = true;
	private bool _desativarMenu = true;
	private bool ladrilhoVazio = true;
	private bool primeiraVezSelecionado = true;
	private bool primeiraVezDeselecionado = true;

	void Awake(){

	}
	void Start(){
		menuDispositivos = GameObject.Find("_menuladrilhos").GetComponent<_menuladrilhos>().menu_ladrilhos;
		//menuDispositivos.SetActive(false);
		//dispositivosInstalados =  new Transform[numDispositivos];

		rend = GetComponent<Renderer>();

		corInicial = rend.material.color;
	}
	void Update(){

	}
	void Ativado(){
		if(_ativarMenu){
			AtivarMenu();
			_ativarMenu = false;
		}
		_desativarMenu = true;
		rend.material.color = Color.red;
		//menuDispositivos.SetActive (true);
	}
	void Desativado(){
		rend.material.color = corInicial;
		if(_desativarMenu){
			DesativarMenu();
			_desativarMenu = false;
		}
		_ativarMenu = true;
		//menuDispositivos.SetActive (false);
	}
	void Selecionado(){
		if(primeiraVezSelecionado){
			Debug.Log ("selec");
			//rend.material.color = Color.yellow;
			//rend.material.color = Color.Lerp(rend.material.color, Color.yellow, Time.deltaTime * 1);
			primeiraVezSelecionado = false;
			primeiraVezDeselecionado = true;
			StartCoroutine(LerpSelecionado());
		}
	}
	void Deselecionado(){
		if(primeiraVezDeselecionado){
			Debug.Log ("deselec");
			//rend.material.color = Color.white;
			//rend.material.color = Color.Lerp(Color.yellow,corInicial, Time.deltaTime * 1);
			primeiraVezDeselecionado = false;
			primeiraVezSelecionado = true;
			StartCoroutine(LerpDeselecionado());
		}
	}
	void AtivarMenu(){
		Debug.Log ("abriu");
		menuDispositivos.SetActive(true);
		menuDispositivos.GetComponent<Animation>().Play("menu_entrando");
		menuDispositivos.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
	}
	void DesativarMenu(){
		menuDispositivos.GetComponent<Animation>().Play("menu_saindo");
		if(!menuDispositivos.GetComponent<Animation>().IsPlaying("menu_saindo"))
			menuDispositivos.SetActive(false);
	}
	void AdicionaDispositivo(int tipoDispositivo){
		if(ladrilhoVazio){
			if(tipoDispositivo == 1)
				dispositivoInstanciado = Instantiate (dispositivos[0],new Vector3(transform.position.x, transform.position.y+dispositivos[0].localScale.y/2, transform.position.z), Quaternion.identity) as GameObject;
			else if(tipoDispositivo == 2)
				dispositivoInstanciado = Instantiate (dispositivos[1],new Vector3(transform.position.x, transform.position.y+dispositivos[1].localScale.y/2, transform.position.z), Quaternion.identity) as GameObject;
		}
		ladrilhoVazio = false;
	}
	private IEnumerator LerpSelecionado(){
		float tempoSelecao = 0;
		while(true)
		{
			tempoSelecao += Time.deltaTime;
			rend.material.color = Color.Lerp(rend.material.color, Color.yellow, Time.deltaTime * 5f);
			if(tempoSelecao > 0.3f){
				Debug.Log ("parou");
				return false;
			}
			yield return new WaitForEndOfFrame();
		}
	}
	private IEnumerator LerpDeselecionado(){
		float tempoDeselecao = 0;
		while(true)
		{
			rend.material.color = Color.white;
			tempoDeselecao += Time.deltaTime;
			if(tempoDeselecao > 1){
				rend.material.color = Color.white;
				return false;
			}
			yield return new WaitForEndOfFrame();
		}
	}
}

