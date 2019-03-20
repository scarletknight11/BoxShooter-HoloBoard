using UnityEngine;
using TesseractBasic;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Canvas UI Interactible. Attach this script to any UI based GameObject along with a Box Collider and ARInteractible to simulate a button click
/// initiated by a controller.
/// </summary>
public class CanvasUIInteractible : MonoBehaviour {

	public Image TargetImage;

	#region colortransition
	public Color NormalColor = new Color (1, 1, 1, 1);
	public Color HighlightedColor = new Color (0.95f,0.95f,0.95f,1);
	public Color PressedColor = new Color (0.9f, 0.9f, 0.9f, 1);
	#endregion

	/// <summary>
	/// The click button.
	/// </summary>
	public CONTROLLER_BUTTON ClickButton = CONTROLLER_BUTTON.APP_BTN;

	/// <summary>
	/// The event that will be invoked upon clicking this button.
	/// </summary>
	public UnityEvent OnClick;

	private ARInteractible mARInteractible;

	// Use this for initialization
	void Start ()
	{

		if (TargetImage != null)
			TargetImage.color = NormalColor;

		mARInteractible = GetComponent<ARInteractible> ();

		//If no interactible exists, disable this script
		if (mARInteractible == null) {
			enabled = false;
			return;
		}
	}

	private void OnEnable ()
	{
		//Subscribing to events
		mARInteractible = GetComponent<ARInteractible> ();
		if (mARInteractible != null) {
			mARInteractible.OnEnter += MARInteractible_OnEnter;
			mARInteractible.OnExit += MARInteractible_OnExit;
			mARInteractible.OnButtonPressStart += MARInteractible_OnButtonPressStart;
			mARInteractible.OnButtonPressEnd += MARInteractible_OnButtonPressEnd;
		}
	}

	private void OnDisable ()
	{
		//Unsubscribing to the events
		if (mARInteractible != null) {
			mARInteractible.OnEnter -= MARInteractible_OnEnter;
			mARInteractible.OnExit -= MARInteractible_OnExit;
			mARInteractible.OnButtonPressStart -= MARInteractible_OnButtonPressStart;
			mARInteractible.OnButtonPressEnd -= MARInteractible_OnButtonPressEnd;
		}
	}

	void MARInteractible_OnEnter ()
	{
		if (TargetImage != null)
			TargetImage.color = HighlightedColor;
	}

	void MARInteractible_OnExit ()
	{
		if (TargetImage != null)
			TargetImage.color = NormalColor;
	}

	void MARInteractible_OnButtonPressStart (CONTROLLER_BUTTON obj)
	{
		if (ClickButton == obj) {
			if (TargetImage != null)
				TargetImage.color = PressedColor;
		}
	}

	void MARInteractible_OnButtonPressEnd (CONTROLLER_BUTTON obj)
	{
		if (ClickButton == obj) {
			if (TargetImage != null)
				TargetImage.color = NormalColor;

			if (OnClick != null)
				OnClick.Invoke ();
		}
	}
}