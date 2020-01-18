using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
	#region Properties

	public static Vector3 ScreenCenter { get; } = Vector3.zero;

	/// <summary>
	/// Gets the left edge of the screen in world coordinates
	/// </summary>
	/// <value>left edge of the screen</value>
	public static Vector3 ScreenLeft { get; private set; }

	/// <summary>
	/// Gets the right edge of the screen in world coordinates
	/// </summary>
	/// <value>right edge of the screen</value>
	public static Vector3 ScreenRight { get; private set; }

	/// <summary>
	/// Gets the top edge of the screen in world coordinates
	/// </summary>
	/// <value>top edge of the screen</value>
	public static Vector3 ScreenTop { get; private set; }

	/// <summary>
	/// Gets the bottom edge of the screen in world coordinates
	/// </summary>
	/// <value>bottom edge of the screen</value>
	public static Vector3 ScreenBottom { get; private set; }

	#endregion

	#region Methods

	/// <summary>
	/// Initializes the screen utilities
	/// </summary>
	public static void Initialize()
    {
		// save screen edges in world coordinates
		float screenZ = -Camera.main.transform.position.z;
		Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
		Vector3 upperRightCornerScreen = new Vector3(
			Screen.width, Screen.height, screenZ);
		Vector3 lowerLeftCornerWorld = 
			Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
		Vector3 upperRightCornerWorld = 
			Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
		ScreenLeft = new Vector3(lowerLeftCornerWorld.x, 0, 0);
		ScreenRight = new Vector3(upperRightCornerWorld.x, 0, 0);
		ScreenTop = new Vector3(0, upperRightCornerWorld.y, 0);
		ScreenBottom = new Vector3(0, lowerLeftCornerWorld.y, 0);
	}

	#endregion
}
