using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(UnitMovementSystem))]
public sealed class UnitMovementSystem : UpdateSystem {
    Filter units;
    public override void OnAwake() {
        units = World.Filter.With<UnitMarker>().With<NMAgent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var unit in units)
        {
            var navMeshAgent = unit.GetComponent<NMAgent>();
            if (navMeshAgent.destination != Vector3.zero)
            {
                navMeshAgent.navMeshAgent.destination = navMeshAgent.destination;
            }
        }
    }
}