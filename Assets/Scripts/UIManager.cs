using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting.Dependencies.Sqlite;

public class UIManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField]
    private Slider SliderMaxSpeed;
    [SerializeField]
    private Slider SliderMaxSteering;
    [SerializeField]
    private Slider SliderNodeRadius;
    [SerializeField]
    private Button btnReset;
    [SerializeField]
    private Button btnResetAll;

    [SerializeField]
    private EntitySpawner carSpawner;
    [SerializeField]
    private NodesPath path;

    private void Awake()
    {
        if (carSpawner == null) return;
        
        ResetSliders();

        SliderMaxSpeed.onValueChanged.AddListener(OnChangeMaxSpeed);

        SliderMaxSteering.onValueChanged.AddListener(OnChangeMaxSteer);

        SliderNodeRadius.onValueChanged.AddListener(OnChangeRadius);


        btnReset.onClick.AddListener(ResetValues);
        btnResetAll.onClick.AddListener(ResetAll);
    }

    void ResetSliders()
    {
        SliderMaxSpeed?.SetValueWithoutNotify(carSpawner.DefaultSpeed);
        SliderMaxSteering?.SetValueWithoutNotify(carSpawner.DefaultSteer);
    }

    void OnChangeMaxSpeed(float value)
    {
        carSpawner.MaxSpeed= value;
    }

    void OnChangeMaxSteer(float value)
    {
        carSpawner.MaxSteer= value;
    }

    void OnChangeRadius(float value)
    {
        path.RadiusRun = value;
    }

    void ResetValues()
    {
        carSpawner?.SetDefaultValues();
        path?.SetDefaultRadius();
        ResetSliders();
    }
    void ResetAll()
    {
        ResetValues();
        carSpawner.DestroyAllCars();
    }


}
