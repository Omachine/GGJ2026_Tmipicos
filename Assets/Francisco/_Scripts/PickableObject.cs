using UnityEngine;

[CreateAssetMenu(fileName = "PickableObject", menuName = "Scriptable Objects/PickableObject")]
public class PickableObject : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] bool canPick, canRotate, isClock;
    [SerializeField] GameObject objToInteract;

    public bool GetPick() {  return canPick; }
    public bool CanRotate() {  return canRotate; }
    public bool IsClock() { return isClock; }
    public string GetName() { return name; }
    public GameObject GetObjToInteract() {  return objToInteract; }
}
