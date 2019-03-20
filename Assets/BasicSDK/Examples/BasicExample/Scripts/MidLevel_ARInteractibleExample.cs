using UnityEngine;
using TesseractBasic;

/// <summary>
/// Middle level AR Interactible example. This example shows how to use the Mid Level Interaction API
/// </summary>
[RequireComponent(typeof(ARInteractible))]
public class MidLevel_ARInteractibleExample: MonoBehaviour {

	private ARInteractible mARInteractible;

	private void OnEnable ()
	{
		if (mARInteractible == null)
			mARInteractible = GetComponent<ARInteractible> ();

		//Subscribing to ARInteractible Events
		mARInteractible.OnEnter += MARInteractible_OnEnter;
		mARInteractible.OnExit += MARInteractible_OnExit;

		mARInteractible.OnButtonPressStart += MARInteractible_OnButtonPressStart;
		mARInteractible.OnButtonPressEnd += MARInteractible_OnButtonPressEnd;
		mARInteractible.OnTrackpadTouchBegin += MARInteractible_OnTrackpadTouchBegin;
		mARInteractible.OnTrackpadTouchEnd += MARInteractible_OnTrackpadTouchEnd;

		mARInteractible.OnSingleClickUp += MARInteractible_OnSingleClickUp;
		mARInteractible.OnSingleClickConfirm += MARInteractible_OnSingleClickConfirm;
		mARInteractible.OnDoubleClick += MARInteractible_OnDoubleClick;
		mARInteractible.OnDoubleClickCancelled += MARInteractible_OnDoubleClickCancelled;
		mARInteractible.OnLongPressStart += MARInteractible_OnLongPressStart;
		mARInteractible.OnLongPressEnd += MARInteractible_OnLongPressEnd;
		mARInteractible.OnSwipeVertical += MARInteractible_OnSwipeVertical;
		mARInteractible.OnSwipeHorizontal += MARInteractible_OnSwipeHorizontal;
	}

	private void OnDisable ()
	{
		//Unsubscribing to ARInteractible Events
		mARInteractible.OnEnter -= MARInteractible_OnEnter;
		mARInteractible.OnExit -= MARInteractible_OnExit;

		mARInteractible.OnButtonPressStart -= MARInteractible_OnButtonPressStart;
		mARInteractible.OnButtonPressEnd -= MARInteractible_OnButtonPressEnd;
		mARInteractible.OnTrackpadTouchBegin -= MARInteractible_OnTrackpadTouchBegin;
		mARInteractible.OnTrackpadTouchEnd -= MARInteractible_OnTrackpadTouchEnd;

		mARInteractible.OnSingleClickUp -= MARInteractible_OnSingleClickUp;
		mARInteractible.OnSingleClickConfirm -= MARInteractible_OnSingleClickConfirm;
		mARInteractible.OnDoubleClick -= MARInteractible_OnDoubleClick;
		mARInteractible.OnDoubleClickCancelled -= MARInteractible_OnDoubleClickCancelled;
		mARInteractible.OnLongPressStart -= MARInteractible_OnLongPressStart;
		mARInteractible.OnLongPressEnd -= MARInteractible_OnLongPressEnd;
		mARInteractible.OnSwipeVertical -= MARInteractible_OnSwipeVertical;
		mARInteractible.OnSwipeHorizontal -= MARInteractible_OnSwipeHorizontal;
	}

	void MARInteractible_OnEnter ()
	{
		Debug.Log ("Gaze has entered the " + gameObject.name);
	}

	void MARInteractible_OnExit ()
	{
		Debug.Log ("Gaze has exited the " + gameObject.name);
	}

	void MARInteractible_OnButtonPressEnd (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Button " + obj + " press has ended while gazing on " + gameObject.name);
	}

	void MARInteractible_OnButtonPressStart (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Button " + obj + " press has started while gazing on " + gameObject.name);
	}

	void MARInteractible_OnTrackpadTouchBegin ()
	{
		Debug.Log ("Trackpad Touch Begin on " + gameObject.name);
	}

	void MARInteractible_OnTrackpadTouchEnd ()
	{
		Debug.Log ("Trackpad Touch End on " + gameObject.name);
	}

	void MARInteractible_OnSingleClickUp (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Single Click " + obj + " up on " + gameObject.name);
	}

	void MARInteractible_OnSingleClickConfirm (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Single Click Confirm " + obj + " on " + gameObject.name);
	}

	void MARInteractible_OnDoubleClick (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Double Click on " + gameObject.name);
	}

	void MARInteractible_OnDoubleClickCancelled ()
	{
		Debug.Log ("Double Click Cancelled on " + gameObject.name);
	}

	void MARInteractible_OnLongPressStart (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Long Press Start on " + gameObject.name);
	}

	void MARInteractible_OnLongPressEnd (CONTROLLER_BUTTON obj)
	{
		Debug.Log ("Long press end on " + gameObject.name);
	}

	void MARInteractible_OnSwipeVertical (float obj)
	{
		Debug.Log ("Swipe Vertical on " + gameObject.name);
	}

	void MARInteractible_OnSwipeHorizontal (float obj)
	{
		Debug.Log ("Swipe Horizontal on " + gameObject.name);
	}

}
