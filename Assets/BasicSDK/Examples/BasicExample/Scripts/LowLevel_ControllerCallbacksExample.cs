using UnityEngine;
using TesseractBasic;

/// <summary>
/// Low level controller callbacks example. This example shows how to use the Low-Level Callbacks from the controller.
/// </summary>
public class LowLevel_ControllerCallbacksExample : MonoBehaviour {

	/// <summary>
	/// The button used for recentering, as an example
	/// </summary>
	public CONTROLLER_BUTTON RecenterButton = CONTROLLER_BUTTON.VOL_UP;

   	void Start () {
		//Subscribing to controller events
        ControllerCallbacks.OnTrackpadTouchBegin += OnTrackpadTouchStart;
        ControllerCallbacks.OnTrackpadTouchEnd += OnTrackpadTouchEnd;
        ControllerCallbacks.OnControllerStateChanged += ControllerStateChange;

		ControllerCallbacks.OnButtonSingleClickConfirm+= ControllerCallbacks_OnButtonClickConfirm;
		ControllerCallbacks.OnButtonSingleClickUp += ControllerCallbacks_OnButtonClickUp;
		ControllerCallbacks.OnButtonDoubleClick += ControllerCallbacks_OnButtonDoubleClick;
		ControllerCallbacks.OnButtonLongPressEnd+= ControllerCallbacks_OnButtonLongPressEnd;
		ControllerCallbacks.OnButtonLongPressStart += ControllerCallbacks_OnButtonLongPressStart;
        ControllerCallbacks.OnSwipeHorizontal += OnSwipeHorizontal;
        ControllerCallbacks.OnSwipeVertical += OnSwipeVertical;
		ControllerCallbacks.OnDoubleClickCancelled += ControllerCallbacks_OnDoubleClickCancelled;
    }

    void Destroy()
    {
		//Unsubscribing to controller events
		ControllerCallbacks.OnButtonSingleClickConfirm -= ControllerCallbacks_OnButtonClickConfirm;
		ControllerCallbacks.OnButtonSingleClickUp -= ControllerCallbacks_OnButtonClickUp;
		ControllerCallbacks.OnTrackpadTouchBegin -= OnTrackpadTouchStart;
        ControllerCallbacks.OnTrackpadTouchEnd -= OnTrackpadTouchEnd;
        ControllerCallbacks.OnControllerStateChanged -= ControllerStateChange;
        ControllerCallbacks.OnSwipeHorizontal -= OnSwipeHorizontal;
        ControllerCallbacks.OnSwipeVertical -= OnSwipeVertical;
		ControllerCallbacks.OnButtonDoubleClick -= ControllerCallbacks_OnButtonDoubleClick;
		ControllerCallbacks.OnButtonLongPressEnd -= ControllerCallbacks_OnButtonLongPressEnd;
		ControllerCallbacks.OnButtonLongPressStart -= ControllerCallbacks_OnButtonLongPressStart;
    }

    void OnTrackpadTouchStart()
    {
        Debug.LogError("Touch start");
    }

    void OnTrackpadTouchEnd()
    {
        Debug.LogError("Touch end");
    }

    void ControllerStateChange(CONTROLLER_STATE state)
    {
        Debug.LogError("new state " + state);
    }

    void OnSwipeHorizontal(float factor)
    {
        Debug.LogError("horizontal " + factor);
    }

    void OnSwipeVertical(float factor)
    {
        Debug.LogError("vertical " + factor);
    }

	void ControllerCallbacks_OnButtonDoubleClick (CONTROLLER_BUTTON obj)
	{
		Debug.LogError ("Double click " +obj);
	}

	void ControllerCallbacks_OnButtonLongPressEnd (CONTROLLER_BUTTON obj)
	{
		Debug.LogError ("Long Press End " +obj);
	}

	void ControllerCallbacks_OnButtonLongPressStart (CONTROLLER_BUTTON obj)
	{
		Debug.LogError ("Long Press Start " +obj);
	}

	void ControllerCallbacks_OnDoubleClickCancelled ()
	{
		Debug.LogError ("Double Click Cancelled");
	}

	void ControllerCallbacks_OnButtonClickConfirm (CONTROLLER_BUTTON obj)
	{
		//Calling Recenter to make the camera face along the direction of the recenter point
		if (obj == RecenterButton)
			ARCamera.Instance.Recenter ();
		Debug.LogError ("On button click confirm " + obj);
	}

	void ControllerCallbacks_OnButtonClickUp(CONTROLLER_BUTTON obj){
		Debug.LogError ("On button click up " + obj);
	}

	//private void Update ()
	//{

	//}
}
