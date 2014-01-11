using UnityEngine;
using System.Collections;

public class DeathZoneTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		StartCoroutine(load());
    }

	private IEnumerator load(){
		yield return new WaitForSeconds(3);
		Application.LoadLevel(Application.loadedLevel);
	}
}
