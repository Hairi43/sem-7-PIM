using UnityEngine;
using UnityEngine.SceneManagement;

public class FortuneWheel : MonoBehaviour
{
    public float minSpinForce = 500f;
    public float maxSpinForce = 2000f;
    public float spinTime = 2f;

    private string[] segments = {"1", "2", "1", "2", "1", "2"};
    private string result;

    private float currentSpeed;
    private bool spinning = false;

    void Update()
    {
        if (spinning)
        {
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime / spinTime);

            if (currentSpeed < 5f)
            {
                spinning = false;
                currentSpeed = 0;
                result = GetValue();
                Debug.Log("Wheel stopped at: " + result);
                
                // save scene name for ExpandableMenuItem return button and EndGameButton
                var sceneName = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("lastScene", sceneName);
                PlayerPrefs.Save();
                // switch to rolled game
                switch (result)
                {
                    case "1":
                        SceneManager.LoadScene("PaintCatcher");
                        break;
                    case "2":
                        SceneManager.LoadScene("ContainerStacker");
                        break;
                    default:
                        Debug.Log("Someone stole the scene :fire:");
                        break;
                }
            }
        }
    }

    public void Spin()
    {
        if (spinning) return;

        currentSpeed = Random.Range(minSpinForce, maxSpinForce);
        spinning = true;
    }

    private string GetValue()
    {
        float angle = transform.eulerAngles.z;
        Debug.Log("Angle Before is equal: " + angle);
        angle = (360 - angle) % 360;
        Debug.Log("Angle is equal: " + angle);
        
        float segmentSize = 360f / segments.Length;
        Debug.Log("SegmentSize is equal: " + segmentSize);
        int index = Mathf.FloorToInt(angle / segmentSize);
        Debug.Log("Index is equal: " + index);
        index = Mathf.Clamp(index, 0, segments.Length - 1);

        return segments[index];
    }
}
