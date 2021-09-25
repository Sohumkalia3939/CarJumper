
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{

    public Text Score;
    public CarController carController;
    public Transform car;
    // Start is called before the first frame update
    void Start()
    {
       carController=FindObjectOfType<CarController>();
        Score.text = "Score:" + car.transform.position.z.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput.ProcessTouches();

        if (TouchInput.Tap())
        {
            SceneManager.LoadScene(0);
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            SceneManager.LoadScene(0);

        }
    }
    }
