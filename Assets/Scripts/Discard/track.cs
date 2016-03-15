using UnityEngine;
using System.Collections;

public class track : MonoBehaviour {

	float avance =0.1f;
	int i=0;
	float giro = 0.3f;
	int limiteBloque = 100;
	float angulo = 10f;
	Vector3 posicion;
	Vector3 rotacion;
	
	private void Update () {
		if(Input.GetButtonDown("Fire1")){
			int trigger;
			float var = Random.value;
			if (var >= 0.75f)
				trigger = 3;
			else if (var >= 0.50f)
				trigger = 2;
			else
				trigger = 1;
			trigger = 2;
			recta(trigger);
			destroy();
		}
	}
	
	void recta(int valor) {
		switch (valor)
		{
		case 1:
			giro = 0;
			angulo = 0;
			break;
		case 2:
			giro = 0;
			avance = 0.05f;
			//angulo normal
			break;
		case 3:
			giro = 0.8f;
			angulo = 0;
			break;
		}
		string name = "Cube"+i;
		GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube1.transform.position = new Vector3(posicion.x+avance, posicion.y, posicion.z+giro);
		if (rotacion.y != 0f)
			angulo =1f;
		cube1.transform.rotation = Quaternion.Euler (rotacion.x,rotacion.y+angulo,rotacion.z);
		posicion = cube1.transform.position;
		rotacion = cube1.transform.rotation.eulerAngles;
		cube1.transform.localScale = new Vector3(avance,0.2f,4f);
		cube1.name = name;
		i++;
	}

	void destroy() {
		if (i>=limiteBloque) {
			int k = i - limiteBloque;
			string DestroyThatName = "Cube"+k;
			Destroy(GameObject.Find(DestroyThatName));
		}

	}
}
