using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using Scellecs.Morpeh;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CameraFollowSystem))]
public sealed class CameraFollowSystem : UpdateSystem {
    Filter player;
    Filter playerCameraFilter;
    public override void OnAwake() {
        player = World.Filter.With<PlayerMarker>()
            .With<UnitTransform>()
            .With<Playerinput>()
            .Build();
        playerCameraFilter = World.Filter.With<PlayerCamera>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        var playerCamera = playerCameraFilter.First().GetComponent<PlayerCamera>();
        var playerTransform = player.First().GetComponent<UnitTransform>();
        ref var playerInput = ref player.First().GetComponent<Playerinput>();

        playerCamera.cameraTransform.position = playerTransform.unitTransform.position;

        if (Input.GetMouseButton(0))
        {
            playerCamera.cameraTransform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * playerInput.cameraRotationSpeed, 0);
            if (!playerInput.isHold)
            {
                playerInput.timeHold += Time.deltaTime;
                if(playerInput.timeHold > 0.5f)
                {
                    playerInput.timeHold = 0;
                    playerInput.isHold = true;
                }
            }
        }
        if(Input.GetMouseButtonUp(0) && playerInput.isHold)
        {
            playerInput.isHold = false;
        }
    }
}