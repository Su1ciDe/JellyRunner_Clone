using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 0)]
public class LevelsSO : ScriptableObject
{
	public List<SceneAsset> Scenes = new List<SceneAsset>();
}