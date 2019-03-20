using UnityEngine;
using TesseractBasic;

/// <summary>
/// High level AR Interactible example. This example shows how to create a High Level Interaction System using Mid-Level Interactible API
/// </summary>
[RequireComponent(typeof(ARInteractible))]
public class HighLevel_ARInteractibleExample : MonoBehaviour {

	/// <summary>
	/// The interact button.
	/// </summary>
	public CONTROLLER_BUTTON InteractButton = CONTROLLER_BUTTON.APP_BTN;

	/// <summary>
	/// The rotation trigger button.
	/// </summary>
	public CONTROLLER_BUTTON RotationButton = CONTROLLER_BUTTON.HOME_BTN;

	/// <summary>
	/// The grab trigger button.
	/// </summary>
	public CONTROLLER_BUTTON GrabButton = CONTROLLER_BUTTON.APP_BTN;

	/// <summary>
	/// The scale increment factor
	/// </summary>
	public float ScaleIncrementFactor = 1.2f;

	/// <summary>
	/// The rotation increment factor.
	/// </summary>
	public float RotationIncrementFactor = 0.1f;

	#region private
	private float releaseDist = 1f;
	private ARInteractible interactible;
	private ARCamera arCamera;
	private HoloboardController controller;
	private bool rotating = false;
	private bool grabbed = false;
	private Vector3 angles;
	private Ray ray;
	private Quaternion pickedRotation = new Quaternion();
	private Camera cam;

	//Variables to avoid scaling down the GameObject too small or it away too far that its not longer visible
	private float ScreenRatioForSmall = (float)(Screen.height / 8);
	private float smallRatioThreshold = 0.13f;

	//Variable to keep track whether the GameObject is colliding with the headset, in order to avoid scaling the GameObject too big or
	// bringing it too close to the user.
	private bool colliding = false;
#endregion

	public void Start ()
	{

		arCamera = ARCamera.Instance;
		controller = HoloboardController.Instance;

		ray = arCamera.GetRayFromGaze ();
		interactible = GetComponent<ARInteractible> ();
	}

	private void OnEnable ()
	{
		if(interactible==null)
			interactible = GetComponent<ARInteractible> ();

		//Subscribing to interactible events
		interactible.OnSingleClickConfirm+=Interactible_OnSingleClick;
		interactible.OnSwipeVertical += Interactible_OnSwipeVertical;
		interactible.OnSwipeHorizontal += Interactible_OnSwipeHorizontal;
		interactible.OnDoubleClick += Interactible_OnDoubleClick;
		interactible.OnLongPressStart+=Interactible_OnLongPressStart;
		interactible.OnLongPressEnd += Interactible_OnLongPressEnd;
	}

	private void OnDisable ()
	{
		if (interactible == null)
			interactible = GetComponent<ARInteractible> ();

		//Unsubscribing interactibe events
		interactible.OnSingleClickConfirm -= Interactible_OnSingleClick;
		interactible.OnSwipeVertical -= Interactible_OnSwipeVertical;
		interactible.OnSwipeHorizontal -= Interactible_OnSwipeHorizontal;
		interactible.OnDoubleClick -= Interactible_OnDoubleClick;
		interactible.OnLongPressStart -= Interactible_OnLongPressStart;
		interactible.OnLongPressEnd -= Interactible_OnLongPressEnd;
	}

	public void Update ()
	{
		//Rotation
		if(rotating){
			angles = controller.ControllerOrientation.eulerAngles;

			if (angles.z > 180)
				angles.z -= 360.0f;

			transform.Rotate (new Vector3 (0, 1, 0), angles.z*RotationIncrementFactor, Space.Self);
		}

		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			if (controller.ControllerButtonActive == CONTROLLER_BUTTON.NONE) {
				grabbed = false;
				rotating = false;
			}
		}
	}

	private void LateUpdate ()
	{
		//Grabbing
		if (grabbed) {
			ray = arCamera.GetRayFromGaze ();
			transform.position = ray.GetPoint (releaseDist);

			//Rotating the gameObject so that it always faces the user while grabbing the object.
			transform.rotation = GetObjectRotationWRTCamera (pickedRotation);
		}
	}

void Interactible_OnSingleClick (CONTROLLER_BUTTON obj)
	{
		//Place Holder for Interact Functionality
		if (obj == InteractButton) {
			Debug.Log ("Single Click on " + name);
		}
	}

	//Moving Along the ray
void Interactible_OnSwipeVertical (float fact)
	{
		fact = fact * 1.3f;

		if (fact < 0) {
			if (colliding)
				return;
		} else {
			if (SmallerThanRequired ()) {
				return;
			}
		}

		ray = arCamera.GetRayFromGaze ();
		transform.position += ray.direction * fact;
	}

	//Scaling 
void Interactible_OnSwipeHorizontal (float scale)
	{
	
		scale = scale + 1;

		if (scale > 1) {
			if (colliding)
				return;
		} else {
			if (SmallerThanRequired ()) {
				//Debug.LogError ("smaller than thresh");
				return;
			}
		}

		//have to add min max range
		transform.localScale = transform.localScale * scale;
	}

	//Delete
void Interactible_OnDoubleClick (CONTROLLER_BUTTON obj)
	{
		if(obj == InteractButton)
			Destroy (gameObject);
	}

	//Grab Start / Rotation Start
void Interactible_OnLongPressStart (CONTROLLER_BUTTON obj)
	{
		if (obj == RotationButton) {
			rotating = true;
			controller.ResetControllerOrientation ();
		}

		if (obj == GrabButton) {
			grabbed = true;
			releaseDist = (arCamera.GetHeadTransform ().position - transform.position).magnitude;
			pickedRotation = GetRotationBetweenObjectAndCamera (gameObject);
		}
	}

	//Grab End/ Rotation End
void Interactible_OnLongPressEnd (CONTROLLER_BUTTON obj)
	{
		if (obj == RotationButton)
			rotating = false;

		if (obj == GrabButton) {
			grabbed = false;
			release ();
		}
	}


	/// <summary>
	/// Releasing any grabbed object.
	/// </summary>
	private void release(){
		ray = arCamera.GetRayFromGaze ();
		transform.position = ray.GetPoint (releaseDist);
	}

	/// <summary>
	/// Gets the relative rotation between object and camera.
	/// </summary>
	/// <returns>The rotation between object and camera.</returns>
	/// <param name="go">GameObject whose rotation with respect to the camera is to be found.</param>
	public Quaternion GetRotationBetweenObjectAndCamera (GameObject go)
	{
		Vector3 cameraForwardOnPlane = Vector3.ProjectOnPlane (arCamera.GetHeadTransform().forward, Vector3.up);
		return Quaternion.FromToRotation (cameraForwardOnPlane, go.transform.forward);
	}

	/// <summary>
	/// Gets the object rotation with respect to the camera.
	/// </summary>
	/// <returns>Quaternion representing the object rotation with respect to the  camera.</returns>
	/// <param name="angleBetweenObjectAndCamera">Angle between object and camera.</param>
	public Quaternion GetObjectRotationWRTCamera (Quaternion angleBetweenObjectAndCamera)
	{
		Vector3 cameraForwardOnPlane = Vector3.ProjectOnPlane (arCamera.GetHeadTransform ().forward, Vector3.up);
		Vector3 objectForward = angleBetweenObjectAndCamera * cameraForwardOnPlane;
		return Quaternion.LookRotation (objectForward, Vector3.up);
	}

	/// <summary>
	/// This functions checks whether the gameObject is below a certain visible size
	/// </summary>
	/// <returns><c>true</c>, if smaller than required, <c>false</c> otherwise.</returns>
	private bool SmallerThanRequired ()
	{
		Bounds bounds = new Bounds ();
		foreach (var r in transform.GetComponentsInChildren<Renderer> ()) {
			bounds.Encapsulate (r.bounds);
		}

		if (cam == null)
			cam = arCamera.GetRayCastCamera();
		Vector3 a = cam.WorldToScreenPoint (transform.position);
		Vector3 b = new Vector3 (a.x, a.y + ScreenRatioForSmall, a.z);
		float ratio = (cam.ScreenToWorldPoint (a) - cam.ScreenToWorldPoint (b)).magnitude / bounds.size.magnitude;

		if (ratio >= smallRatioThreshold && ratio < 100)
			return true;

		return false;
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.GetComponent<ARCamera>()!=null)
			colliding = true;
	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.GetComponent<ARCamera> () != null)
			colliding = true;
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.GetComponent<ARCamera> () != null)
			colliding = false;
	}
}
