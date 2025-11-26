using UnityEngine;

[CreateAssetMenu(fileName = "New tutorial text", menuName = "Tutorial")]
public class TutorialDataObject : ScriptableObject
{
    public string sceneName;
    public string sceneNameHeader;
    [TextArea] public string content;

}
