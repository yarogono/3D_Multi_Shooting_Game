using UnityEngine;
using static Define;

public class DropItemController : MonoBehaviour
{
    [SerializeField] private DropItemType _type;
    [SerializeField] private int _value;

    private int _id;

    public int Id 
    { 
        get => _id; 
        set => _id = value; 
    }

    public int Value 
    { 
        get => _value; 
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }
}