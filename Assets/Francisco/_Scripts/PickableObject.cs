using UnityEngine;

[CreateAssetMenu(fileName = "PickableObject", menuName = "Scriptable Objects/PickableObject")]
public class PickableObject : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] bool canPick, canRotate;
    [SerializeField] GameObject objToInteract;

    public bool GetPick() {  return canPick; }
    public bool CanRotate() {  return canRotate; }
    public GameObject GetObjToInteract() {  return objToInteract; }
}
