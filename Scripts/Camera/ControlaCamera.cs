using UnityEngine;
using System.Collections;

public class ControlaCamera : MonoBehaviour {
	public Transform posOrto;
	public Transform posPers;

	private bool perspectiva = true;

	void Start(){
		Camera.main.orthographicSize = 50;
		transform.position = posPers.position;
		transform.rotation = posPers.rotation;
	}
	public void Ortografica(){
		if(perspectiva){
			StartCoroutine(LerpOrtografica());
			/*transform.position = posOrto.position;
			transform.rotation = posOrto.rotation;*/
		}
		perspectiva = false;
	}
	public void Perspectiva(){
		if(!perspectiva){
			Camera.main.orthographic = false;
			StartCoroutine(LerpPerspectiva());
			/*transform.position = posPers.position;
			transform.rotation = posPers.rotation;*/
		}
		perspectiva = true;
	}

	IEnumerator LerpPerspectiva()
	{
		float t = 0;
		while(t < 1)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position,posPers.position,  t/1.0f);
			transform.rotation = Quaternion.Lerp (transform.rotation,posPers.rotation,  t/1.0f);
			yield return null;
		}
	}

	IEnumerator LerpOrtografica()
	{
		float t = 0;
		while(t < 0.5)
		{
			t += Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position,posOrto.position,  t/1.0f);
			transform.rotation = Quaternion.Lerp (transform.rotation,posOrto.rotation,  t/1.0f);
			yield return null;
		}
		Camera.main.orthographic = true;
	}
}
