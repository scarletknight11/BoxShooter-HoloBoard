

/*
 * Uncomment after adding Vuforia Support
 * 
using UnityEngine;
using Vuforia;
using TesseractBasic;

/// <summary>
/// Vuforia holoboard trackable event handler. This is a custom script made from Vuforia <see cref=" Vuforia.DefaultTrackableEventHandler"/>.
/// Attach this script to all Vuforia Targets present in the scene
/// </summary>
public class VuforiaHoloboardTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

	/// <summary>
	/// When Holoboard-Vuforia Extended tracking is ON, objects are not hidden when the Vuforia trackable is lost.
	/// Set this to true if you want the child objects to be hidden if Vuforia trackable is lost even with Extended tracking.
	/// </summary>
	public bool ForceHideOnLost = false;

	/// <summary>
	/// The trackable behaviour attached to this gameobject
	/// </summary>
	private TrackableBehaviour mTrackableBehaviour;

	/// <summary>
	/// The Vuforia Trackables tracker
	/// </summary>
	private VuforiaTrackablesTracker mVuforiaTrackablesTracker;

	void Start ()
	{
		//Get the TrackableBehaviour Attached to this gameobject
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour) {
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
		}

		//If Hybrid tracking is enabled, get the vuforia trackables tracker to add trackables
		if (ARCamera.Instance.IsHybridTrackingEnabled ()) {

			mVuforiaTrackablesTracker = VuforiaTrackablesTracker.Instance;

			if (mVuforiaTrackablesTracker == null) {
				Debug.LogError ("Holoboard AR Camera configuration not valid! Please refer documentation!");
				ReplaceWithDefaultTrackableEventHandler ();
				HideChildrenRenderersAndColliders ();
				return;
			}

			mVuforiaTrackablesTracker.AddTrackable (mTrackableBehaviour);
		} else
			ForceHideOnLost = true;

		//Hide all renderers and colliders at the beginnig
		HideChildrenRenderersAndColliders ();
	}

	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged (
									TrackableBehaviour.Status previousStatus,
									TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			OnTrackingFound ();
		} else {
			OnTrackingLost ();
		}
	}

	/// <summary>
	/// On tracking found.
	/// </summary>
	private void OnTrackingFound ()
	{
		Renderer [] rendererComponents = GetComponentsInChildren<Renderer> (true);
		Collider [] colliderComponents = GetComponentsInChildren<Collider> (true);

		// Enable rendering:
		foreach (Renderer component in rendererComponents) {
			component.enabled = true;
		}

		// Enable colliders:
		foreach (Collider component in colliderComponents) {
			component.enabled = true;
		}

		if (mVuforiaTrackablesTracker)
			mVuforiaTrackablesTracker.UpdateTrackableState (mTrackableBehaviour, true);

		Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
	}

	/// <summary>
	/// On tracking lost.
	/// </summary>
	private void OnTrackingLost ()
	{
		if (ForceHideOnLost) {
			HideChildrenRenderersAndColliders ();
		}

		Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");

		if (mVuforiaTrackablesTracker)
			mVuforiaTrackablesTracker.UpdateTrackableState (mTrackableBehaviour, false);
	}

	/// <summary>
	/// Hides the children renderers and colliders.
	/// </summary>
	private void HideChildrenRenderersAndColliders(){
		Renderer [] rendererComponents = GetComponentsInChildren<Renderer> (true);
		Collider [] colliderComponents = GetComponentsInChildren<Collider> (true);

		// Disable rendering:
		foreach (Renderer component in rendererComponents) {
			component.enabled = false;
		}

		// Disable colliders:
		foreach (Collider component in colliderComponents) {
			component.enabled = false;
		}
	}

	/// <summary>
	/// Replaces with the default trackable event handler in case some configuration mismatch
	/// </summary>
	private void ReplaceWithDefaultTrackableEventHandler(){
		gameObject.AddComponent<DefaultTrackableEventHandler> ();
		mTrackableBehaviour.UnregisterTrackableEventHandler (this);
		enabled = false;
	}
}
*/