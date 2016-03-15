using UnityEngine;
using System.Collections;

public class Generate : MonoBehaviour {

	GameObject[] segmentos;
	float newX;
	float newZ;
	int segm;
	public GameObject block;
	public Material izq;
	public Material der;
	public Material rect;
	bool varNotReady = false;
	float rotation = 0f;
	float oldRan = 0f;

	private void Awake(){
		newX = newZ = 0f;
		segm = 0;
		segmentos = new GameObject[GlobalVariables.MaxSegments];
	}
		
	private void Start(){
		StartCoroutine(Generator());
	}

	private IEnumerator Generator(){
		float oldX = newX;
		float oldZ = newZ;
		float oldRot = rotation;
		if (segmentos[segm] != null) {
			Destroy(segmentos[segm].gameObject);
		}
		GameObject plane = Instantiate(block) as GameObject;
		NewDirection();
		if (oldX<newX){
			/*if (oldRot!=-90 && segmentos[segm-1] != null)
				segmentos[segm-1].gameObject.GetComponent<Renderer>().material = der;
			plane.GetComponent<Renderer>().material = rect;*/
			rotation=-90f;
		} else if (oldX>newX) {
			/*if (oldRot!=90 && segmentos[segm-1] != null)
				segmentos[segm-1].gameObject.GetComponent<Renderer>().material = izq;
			plane.GetComponent<Renderer>().material = rect;*/
			rotation= 90f;
		} else if (oldZ>newZ) {
			//plane.GetComponent<Renderer>().material = rect;
			rotation =0f;
		} else if (oldZ<newZ) {
			//plane.GetComponent<Renderer>().material = rect;
			rotation=180f;
		}
		plane.transform.position = new Vector3(newX,0,newZ);
		plane.transform.Rotate(0,rotation,0);
		plane.name = "cube "+segm;
		segmentos[segm] = plane;
		segm++;
		if (segm == segmentos.Length-1){
			segm = 0;
		}
		yield return new WaitForSeconds(.5f);
		StartCoroutine(Generator());
	}

	private void NewDirection(){
		do {
			float ran = Random.value;
			if (Random.value <=0.75f)
				ran = oldRan;			
			if (ran <= 0.25f)		newX += 10f;
			else if (ran <= 0.5f)	newX -= 10f;
			else if (ran <= 0.75f)	newZ += 10f;
			else if (ran <= 1f)		newZ -= 10f;
			NotReady (ran);
			oldRan = ran;
		} while (varNotReady);
	}

	private void NotReady(float ran){
		Vector3 pos = new Vector3(newX, 0, newZ);
		for (int i = 0; i < segmentos.Length; i++) {
			if (segmentos[i] != null){
				if (segmentos[i].transform.position == pos){
					Debug.Log("Choque");
					if (ran <=0.25f)		newX-=10f;
					else if (ran <=0.5f)	newX+=10f;
					else if (ran <= 0.75f)	newZ-=10f;
					else if (ran <= 1f)		newZ+=10f;
					varNotReady = true;
				}else{
					varNotReady = false;
				}
			}else{
				varNotReady = false;
			}
		}
	}
}
