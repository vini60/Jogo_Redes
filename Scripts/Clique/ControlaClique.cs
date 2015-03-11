using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlaClique : MonoBehaviour {
	private RaycastHit hit;
	private Transform objetoClicado;
	private Transform objetoSelecionado;
	private Transform objetoSelecionadoAtual;
	private Transform objetoAtual;

	void Start () {
		objetoClicado = null;
	}

	/*void Update () {
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Input.GetMouseButtonDown (0)){
			if(Physics.Raycast(raio, out hit) && (Input.GetMouseButtonDown (0))){ // SE APERTAR O BOTAO ESQUERDO DO MOUSE
				if (hit.transform.GetComponent<Collider>().CompareTag("clicavel")){ // CHECAR SE O CLIQUE INTERCEPTOU UM OBJETO CLICAVEL
					if(objetoSelecionado != hit.transform){ // CHECAR SE O OBJETO CLICADO E DIFERENTE DO OBJETO ATUAL
						if(objetoSelecionado) //SE UM OBJETO FOI SELECIONADO ANTERIORMENTE, DESATIVAR ELE
							objetoSelecionado.SendMessage("Desativado",SendMessageOptions.DontRequireReceiver);
						objetoSelecionado = hit.transform; // SE SIM, ALTERAR O OBJETO SELECIONADO PARA AQUELE QUE FOI CLICADO
					}
					objetoSelecionado.SendMessage("Ativado", 1, SendMessageOptions.DontRequireReceiver);
				}else{
					if(objetoSelecionado != null){ // HA DE HAVER UM OBJETO SELECIONADO PARA ELE SER DESATIVADO
						objetoSelecionado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
						objetoSelecionado = null;
					}
				}
			}
		}else if(Input.GetMouseButtonDown (1)){
			if(objetoSelecionado != null){ // HA DE HAVER UM OBJETO SELECIONADO PARA ELE SER DESATIVADO
				objetoSelecionado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
			}
		}
	}*/
	void Update () {
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(raio, out hit)){ // SE MOUSE ESTIVER EM CIMA DE UM OBJETO CLICAVEL
			if (hit.transform.GetComponent<Collider>().CompareTag("clicavel")){
				if(Input.GetMouseButtonDown (0)){
					if(objetoClicado){
						if(objetoClicado != hit.transform){
							objetoClicado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
						}
					}
					objetoClicado = hit.transform;
				}
				objetoSelecionadoAtual = hit.transform;
				if(objetoSelecionadoAtual != objetoClicado){
					objetoSelecionadoAtual.SendMessage("Selecionado", SendMessageOptions.DontRequireReceiver);
				}
				if(objetoSelecionado){
					if(objetoSelecionado != hit.transform){
						objetoSelecionado.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
					}
				}
				objetoSelecionado = hit.transform;
			}else{
				if(objetoSelecionado)
					objetoSelecionado.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
			}
		}else{
			if(objetoSelecionado)
				objetoSelecionado.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
		}
		if(objetoClicado){
			objetoClicado.SendMessage("Ativado", SendMessageOptions.DontRequireReceiver);
		}

		if(Input.GetMouseButtonDown (1)){
			if(objetoClicado)
				objetoClicado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
			objetoClicado = null;
		}
	}
	public void RetornaObjetoClicado(int tipoClique){
		Debug.Log ("CLICOU");
		if(objetoClicado)
			objetoClicado.SendMessage("AdicionaDispositivo", tipoClique , SendMessageOptions.DontRequireReceiver);
	}


}