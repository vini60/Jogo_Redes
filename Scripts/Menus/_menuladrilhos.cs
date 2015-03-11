using UnityEngine;
using System.Collections;

public class _menuladrilhos : MonoBehaviour {
	public GameObject menu_ladrilhos;

	void Start () {
		menu_ladrilhos = GameObject.Find("Menu_Ladrilhos");
		menu_ladrilhos.GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
		menu_ladrilhos.SetActive (false);
	}

}
