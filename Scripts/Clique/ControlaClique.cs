using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlaClique : MonoBehaviour {
	public int numRouters = 0;

	private RaycastHit hit;

	private Transform objetoClicado;
	private Transform objetoClicadoAnteriormente;
	private Transform objetoSelecionado;
	private Transform objetoSelecionadoAnteriormente;
	public Transform paredeSelecionada;
	public Transform paredeSelecionadaAnteriormente;
	public Transform objetoAtrasParede;
	public Transform objetoAtrasParedeAnteriormente;

	private int tipoClique = 0;

	private float tempoHint = 0;

	public LayerMask clicavel;

	void Update () {
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(raio, out hit, Mathf.Infinity, clicavel)){
			Debug.Log (hit.transform);
			if(hit.transform.GetComponent<Collider>().CompareTag("clicavel")){
				objetoSelecionado = hit.transform;
				if(Input.GetMouseButtonDown (0)){
					tipoClique = 1;
					objetoClicado = objetoSelecionado;
				}
				if(!objetoSelecionado.GetComponent<Movel>().movelVazio){
					tempoHint += Time.deltaTime;
				}else{
					tempoHint = 0;
				}
			}else{
				tempoHint = 0;
			}
		}else{
			tempoHint = 0;
			if(Input.GetMouseButtonDown (0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)){
				if(objetoClicado)
					objetoClicado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
				objetoClicado = null;
			}
			if(objetoSelecionado)
				objetoSelecionado.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
			objetoSelecionado = null;
		}
		if(Input.GetMouseButtonDown (1)){
			if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1)){
				objetoClicado.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);
				objetoClicado = null;
			}
		}

		if(Physics.Raycast(raio, out hit)){
			if(hit.transform.GetComponent<Collider>().CompareTag("parede")){
				paredeSelecionada = hit.transform;
			}
		}

		if(paredeSelecionada){
			if(paredeSelecionadaAnteriormente != paredeSelecionada){
				if(paredeSelecionadaAnteriormente)
					paredeSelecionadaAnteriormente.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
				paredeSelecionadaAnteriormente = paredeSelecionada;
			}
			paredeSelecionada.SendMessage("Selecionado", SendMessageOptions.DontRequireReceiver);
			paredeSelecionadaAnteriormente = paredeSelecionada;
		}else{
			if(paredeSelecionadaAnteriormente)
				paredeSelecionadaAnteriormente.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
		}


		if(objetoSelecionado){
			if(objetoSelecionadoAnteriormente != objetoSelecionado){
				if(objetoSelecionadoAnteriormente)
					objetoSelecionadoAnteriormente.SendMessage("Deselecionado", SendMessageOptions.DontRequireReceiver);
				objetoSelecionadoAnteriormente = objetoSelecionado;
			}
			objetoSelecionado.SendMessage("Selecionado", SendMessageOptions.DontRequireReceiver);
		}
		if(objetoClicado){
			if(objetoClicadoAnteriormente != objetoClicado){
				if(objetoClicadoAnteriormente){
					objetoClicadoAnteriormente.SendMessage("Desativado", SendMessageOptions.DontRequireReceiver);

				}
				objetoClicadoAnteriormente = objetoClicado;
			}
			objetoClicado.SendMessage("Ativado", tipoClique, SendMessageOptions.DontRequireReceiver);
		}
		if(tempoHint > 1){
			//objetoSelecionado
		}
	}
	public void RetornaObjetoClicado(int tipoClique){
		if(objetoClicado)
			objetoClicado.SendMessage("AdicionaDispositivo", tipoClique , SendMessageOptions.DontRequireReceiver);
	}

}