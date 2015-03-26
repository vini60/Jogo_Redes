using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public GameObject ladrilho;
	public int comprimento = 30;
	public int altura = 30;

	public GameObject [,] grid = new GameObject[30,30];
	// Use this for initialization
	void Awake (){
		GerarGrid();
	}
	void GerarGrid () {
		/*for(int z = 0; z < altura; z++){
			for(int x = 0; x < comprimento; x++){	
				Debug.Log (x);
				GameObject planoGrid = (GameObject)Instantiate (ladrilho,transform.position,Quaternion.identity);
				planoGrid.transform.position = new Vector3(planoGrid.transform.position.x + x + 0.5f,
				                                           planoGrid.transform.position.y,
				                                           planoGrid.transform.position.z + z + 0.5f);
				grid[x,z] = planoGrid;
				planoGrid.GetComponent<Ladrilho>().posLadrilho = new Vector2(x,z);
				planoGrid.transform.parent = this.transform; 
			}
		}*/
	}
}
