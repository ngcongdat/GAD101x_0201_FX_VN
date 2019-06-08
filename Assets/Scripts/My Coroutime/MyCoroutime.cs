using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyCoroutime {

	public static IEnumerator WaitForSecondsRealtime (float time) {

		float start = Time.realtimeSinceStartup;

		while (Time.realtimeSinceStartup < (start + time)) {
			yield return null;
		}

	}

}
