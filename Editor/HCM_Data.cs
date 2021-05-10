using UnityEngine;
using System.Collections.Generic;

namespace Ferdi{
[System.Serializable]
public class HCM_Data : ScriptableObject {
	[System.Serializable]
	public class TagColor {
		public string Tag;
		public Color Color;
	}
	public List<TagColor> TagColors = new List<TagColor>(1);
}
}
