using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDatabase", menuName = "TutorialDatabase")]
public class TutorialDB : ScriptableObject
{
    public List<TutorialDataObject> entries = new();

    public TutorialDataObject GetTutorialForScene(string sceneName)
    {
        return entries.FirstOrDefault(entry => entry.sceneName == sceneName);
    }
}
