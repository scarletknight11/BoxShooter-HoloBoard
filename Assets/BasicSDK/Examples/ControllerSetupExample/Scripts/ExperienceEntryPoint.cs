using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Experience entry point, used with the Controller Setup script. Check ControllerSetup example scene
/// </summary>
public class ExperienceEntryPoint : MonoBehaviour {

	public string SceneToLoad = "";

	public void LoadScene(){
		SceneManager.LoadScene (SceneToLoad);
	}
}