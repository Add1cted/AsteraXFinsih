using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileFireButton : MonoBehaviour
{
	//hi
	bool registeredWithPauseChanged = false;

	Image img;

	void Start ()
	{
			img = GetComponent<Image>();
			img.raycastTarget = false;

			if(Application.isMobilePlatform)
			{
				RegisterWithPauseChanged();
				PauseChangedCallback();
			}
			else
			{
				#if UNITY_EDITOR
					StartCoroutine(CheckForUnityRemote(1));
				#else
					gameObject.SetActive(false);
				#endif
			}
	}

	IEnumerator CheckForUnityRemote(float delay)
	{
		while(!registeredWithPauseChanged){
			if(UnityEditor.EditorApplication.isRemoteConnected)
			{
				RegisterWithPauseChanged();
				PauseChangedCallback();
			}
			else
			{
				yield return new WaitForSeconds(delay);
			}
		}
	}

	void RegisterWithPauseChanged()
	{
			if(registeredWithPauseChanged) return;

			AsteraX.PAUSED_CHANGE_DELEGATE -= PauseChangedCallback;
			AsteraX.PAUSED_CHANGE_DELEGATE += PauseChangedCallback;

			registeredWithPauseChanged = true;

	}

	void PauseChangedCallback()
	{
		img.raycastTarget = !AsteraX.PAUSED;
	}
}
