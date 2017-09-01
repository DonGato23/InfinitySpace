using UnityEngine;
using System.Collections;

public class FinalDelPool : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		ObjectPool.Instance.PoolAgain (col.gameObject);
	}
}
