using UnityEngine;
using System.Collections;

public class DeathZoneTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		StartCoroutine(load());
    }

	private IEnumerator load(){
		yield return new WaitForSeconds(1);
		Application.LoadLevel(Application.loadedLevel);
	}
}
