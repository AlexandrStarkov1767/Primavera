using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct Playerinput : IComponent {
    public LayerMask ground;
    public RaycastHit hit;
    public Ray ray;
    public bool isHit;

    public float cameraRotationSpeed;
    public bool isHold;
    public float timeHold;
}