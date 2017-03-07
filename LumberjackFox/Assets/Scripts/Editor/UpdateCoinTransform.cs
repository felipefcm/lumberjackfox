using UnityEngine;
using UnityEditor;

public class UpdateCoinTransform
{
	[MenuItem("4Ever_Alone/UpdateCoinTransform")]
	private static void UpdateCoinTransformItem()
	{
		GameObject[] objs = Selection.gameObjects;

		foreach(GameObject obj in objs)
		{
			obj.transform.localRotation = Quaternion.Euler(52.2f, -54.0f, 0f);
		}
	}
}
