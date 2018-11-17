using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour 
{
    [SerializeField, Range(2, 100)]
    int resolution = 2;

    public int Resolution
    {
        get
        {
            return resolution;
        }
    }
}