using UnityEngine;
using System.Collections;

public class Movel : MonoBehaviour {
	public GameObject[] dispositivos;
	public GameObject dispositivoInstanciado;
	public Transform listaEmissores;
	public Transform listaEmissores_Receptores;
	public Transform listaComputadores;

	public GameObject menuUsado;
	public GameObject menuDispositivos;
	public GameObject menuEspec;
	
	public Transform cuboSelecao;
	public Renderer rendCuboSelecao;

	public bool movelVazio = true;

	public Vector2 posLadrilho;

	private RaycastHit hit;
	
	private bool primeiraVezAtivado = true;
	private bool primeiraVezDesativado = true;
	private bool primeiraVezSelecionado = true;
	private bool primeiraVezDeselecionado = true;
	private bool primeiraVezMenuEspecificacoes = true;

	private Renderer rend;
	private Color corInicial;

	private Color corSelecionado;
	private Color corAtivo;

	private int tipoClique = 0;

	private ControlaClique scriptControlaClique;

	void Awake(){

	}
	void Start(){
		listaEmissores = GameObject.Find("emissores").transform;
		listaEmissores_Receptores = GameObject.Find ("emissores/receptores").transform;
		listaComputadores = GameObject.Find("computadores").transform;

		menuDispositivos = GameObject.Find ("Menu_Ladrilhos");
		menuEspec = GameObject.Find ("Menu_Especificacoes");

		scriptControlaClique = Camera.main.GetComponent<ControlaClique>();

		corSelecionado = new Vector4(1, 0, 0, 0.05f);
		corAtivo = new Vector4(0, 0, 1, 0.2f);

		rendCuboSelecao = cuboSelecao.GetComponent<Renderer>();

		rend = GetComponent<Renderer>();

		corInicial = rend.material.color;
		EnviaMensagem();
	}

	void Ativado(int tipoClique){
		if(primeiraVezAtivado){
			if(tipoClique == 1){
				menuUsado = menuDispositivos;
			}else if(tipoClique == 2){
				menuUsado = menuEspec;
			}
			AtivarMenu();
		}
		cuboSelecao.gameObject.SetActive(true);
		rendCuboSelecao.material.color = corAtivo;
		primeiraVezAtivado = false;
		primeiraVezDesativado = true;
	}
	void Desativado(){
		if(primeiraVezDesativado){
			DesativarMenu();
		}
		cuboSelecao.gameObject.SetActive(false);
		rendCuboSelecao.material.color = corSelecionado;
		primeiraVezAtivado = true;
		primeiraVezDesativado = false;
	}
	void Selecionado(){
		if(primeiraVezSelecionado){
			cuboSelecao.gameObject.SetActive(true);
			rendCuboSelecao.material.color = corSelecionado;
			primeiraVezSelecionado = false;
			primeiraVezDeselecionado = true;
		}
	}
	void Deselecionado(){
		if(primeiraVezDeselecionado){
			cuboSelecao.gameObject.SetActive(false);
			primeiraVezDeselecionado = false;
			primeiraVezSelecionado = true;
		}
	}
	void AtivarMenu(){
		menuUsado.SetActive(true);
		menuUsado.GetComponent<Animation>().Play("menu_entrando");
		menuUsado.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
	}
	void DesativarMenu(){
		menuUsado.GetComponent<Animation>().Play("menu_saindo");
		if(!menuUsado.GetComponent<Animation>().IsPlaying("menu_saindo"))
			menuUsado.SetActive(false);
	}
	
	void AdicionaDispositivo(int tipoDispositivo){
		if(movelVazio){
			if(tipoDispositivo == 1){
				dispositivoInstanciado = Instantiate (dispositivos[0],new Vector3(transform.position.x, transform.position.y+dispositivos[0].transform.localScale.y, transform.position.z), Quaternion.identity) as GameObject;
			}else if(tipoDispositivo == 2){
				if(scriptControlaClique.numRouters == 0){
					dispositivoInstanciado = Instantiate (dispositivos[1],new Vector3(transform.position.x, transform.position.y+dispositivos[1].transform.localScale.y, transform.position.z), Quaternion.identity) as GameObject;
					dispositivoInstanciado.transform.parent = listaEmissores.transform;
					scriptControlaClique.numRouters = 1;
				}
			}else if(tipoDispositivo == 3){
				dispositivoInstanciado = Instantiate (dispositivos[2],new Vector3(transform.position.x, transform.position.y+dispositivos[2].transform.localScale.y, transform.position.z), Quaternion.LookRotation(-Vector3.forward)) as GameObject;
				dispositivoInstanciado.transform.parent = listaEmissores_Receptores.transform;
			}
			EnviaMensagem();
		}
		movelVazio = false;
	}

	void EnviaMensagem(){
		foreach(Transform child in listaEmissores){
			child.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}
		foreach(Transform child in listaEmissores_Receptores){
			child.transform.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}
		foreach(Transform child in listaComputadores){
			child.transform.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}

		foreach(Transform child in listaEmissores){
			child.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}
		foreach(Transform child in listaEmissores_Receptores){
			child.transform.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}
		foreach(Transform child in listaComputadores){
			child.transform.SendMessage("BuscaMelhorSinal", SendMessageOptions.DontRequireReceiver);
		}
	}
}

