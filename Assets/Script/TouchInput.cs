using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TouchInput
{
	// "Core" Variables.

	private static Vector2[] tapPositions = new Vector2[2];
	private static Vector2[] swipePositions = new Vector2[2];

	// "Offset" Variables.

	private static float offsetTap = 15.0F;
	private static float offsetSwipe = 40.0F;

	// "Flag" Variables.

	private static bool fTapAllowed = false;
	private static bool fSwipeAllowed = false;

	// "Other" Variables.

	private static float tempX = 0.0F;
	private static float tempY = 0.0F;

	// "Core" Methods.

	public static void ProcessTouches()
	{
		if (Input.touchCount > 0) // Check If User Is Touching The Screen.
		{
			Touch touch = Input.touches[0];

			if (touch.phase == TouchPhase.Began) // Begin Phase.
			{
				TouchInput.tapPositions[0] = touch.position;
				TouchInput.swipePositions[0] = touch.position;
			}
			else if (touch.phase == TouchPhase.Canceled) // Canceled Phase.
			{
				TouchInput.ResetPositions();
			}
			else if (touch.phase == TouchPhase.Ended) // Ended Phase.
			{
				TouchInput.tapPositions[1] = touch.position;
				TouchInput.swipePositions[1] = touch.position;

				TouchInput.fTapAllowed = true;
				TouchInput.fSwipeAllowed = true;
			}
			else if (touch.phase == TouchPhase.Moved) // Moved Phase.
			{
				// NO CODE ATM FOR MOVED
			}
			else if (touch.phase == TouchPhase.Stationary) // Stationary Phase.
			{
				// NO CODE ATM FOR STATIONARY
			}
		}

	}

	private static void ResetPositions()
	{
		TouchInput.tapPositions = new Vector2[2];

		TouchInput.fTapAllowed = false;
		TouchInput.fSwipeAllowed = false;
	}

	// "Controls" Methods.

	public static bool Tap()
	{
		bool result = false;

		if (TouchInput.fTapAllowed)
		{
			TouchInput.tempX = Mathf.Abs(TouchInput.tapPositions[0].x - TouchInput.tapPositions[1].x);
			TouchInput.tempY = Mathf.Abs(TouchInput.tapPositions[0].y - TouchInput.tapPositions[1].y);

			if (tempX <= TouchInput.offsetTap && tempY <= TouchInput.offsetTap)
			{
				result = true;
			}

			TouchInput.tapPositions = new Vector2[2];
			TouchInput.fTapAllowed = false;
		}

		return result;
	}

	public static bool SwipeLeft()
	{
		bool result = false;

		if (TouchInput.fSwipeAllowed)
		{
			TouchInput.tempX = TouchInput.swipePositions[0].x - TouchInput.swipePositions[1].x;

			if (tempX >= TouchInput.offsetSwipe)
			{
				TouchInput.swipePositions = new Vector2[2];
				TouchInput.fSwipeAllowed = false;
				result = true;
			}
		}

		return result;
	}

	public static bool SwipeRight()
	{
		bool result = false;

		if (TouchInput.fSwipeAllowed)
		{
			TouchInput.tempX = TouchInput.swipePositions[1].x - TouchInput.swipePositions[0].x;

			if (tempX >= TouchInput.offsetSwipe)
			{
				TouchInput.swipePositions = new Vector2[2];
				TouchInput.fSwipeAllowed = false;
				result = true;
			}
		}

		return result;
	}

	public static bool SwipeUp()
	{
		bool result = false;

		if (TouchInput.fSwipeAllowed)
		{
			TouchInput.tempY = TouchInput.swipePositions[1].y - TouchInput.swipePositions[0].y;

			if (tempY >= TouchInput.offsetSwipe)
			{
				TouchInput.swipePositions = new Vector2[2];
				TouchInput.fSwipeAllowed = false;
				result = true;
			}
		}

		return result;
	}

	public static bool SwipeDown()
	{
		bool result = false;

		if (TouchInput.fSwipeAllowed)
		{
			TouchInput.tempY = TouchInput.swipePositions[0].y - TouchInput.swipePositions[1].y;

			if (tempY >= TouchInput.offsetSwipe)
			{
				TouchInput.swipePositions = new Vector2[2];
				TouchInput.fSwipeAllowed = false;
				result = true;
			}
		}

		return result;
	}
}