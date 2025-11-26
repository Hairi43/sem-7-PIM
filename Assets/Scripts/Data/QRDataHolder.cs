using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class QRDataHolder : MonoBehaviour
{
    public static QRDataHolder Instance;
    public Vector3 prefabOffset = new Vector3(0.5f, 1.3f, 1.5f); // default offset
    public Vector3 monsterOffset = new Vector3(0f, 0f, 0f);
    public string gameId;

    public ARAnchor qrAnchor;
    public ARAnchor qrAnchorMonster;
    public Pose qrPose;
    public Vector3 savedCameraPosition;
    public Quaternion savedCameraRotation;

    public bool spawnNoteAfterGame = false;

    void Awake()
    {
        // if (Instance != null && Instance != this)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

    }
}
