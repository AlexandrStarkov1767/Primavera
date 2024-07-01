using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputSystem))]
public sealed class PlayerInputSystem : UpdateSystem {
    Filter cameraFilter;
    Filter Player;
    public override void OnAwake() {
        cameraFilter = World.Filter.With<PlayerCamera>().Build();
        Player = World.Filter.With<PlayerMarker>()
            .With<UnitTransform>()
            .With<Playerinput>()
            .With<NMAgent>()
            .Build();
    }

    public override void OnUpdate(float deltaTime) {
        ref var playerCamera = ref cameraFilter.First().GetComponent<PlayerCamera>();
        ref var playerInput = ref Player.First().GetComponent<Playerinput>();
        ref var playerNavMeshAgent = ref Player.First().GetComponent<NMAgent>();

        playerInput.ray = playerCamera.playerCamera.ScreenPointToRay(Input.mousePosition);
        playerInput.isHit = Physics.Raycast(playerInput.ray, out playerInput.hit, 100);

        if(playerInput.isHit )
        {
            if(playerInput.hit.collider.includeLayers == playerInput.ground)
            {
                Debug.DrawRay(playerInput.ray.origin, playerInput.ray.direction * 100, Color.green);
                if(Input.GetMouseButtonUp(0) && !playerInput.isHold)
                {
                    playerNavMeshAgent.destination = playerInput.hit.point;
                }
            }
            else
            {
                Debug.DrawRay(playerInput.ray.origin, playerInput.ray.direction * 100, Color.red);
            }
        }
    }
}