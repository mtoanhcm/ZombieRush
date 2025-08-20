using UnityEngine;

namespace ZRUtility
{
    public static class ObjectLayer
    {
        public const string PlayerLayerName = "Player";
        public const string EnemyLayerName = "Enemy";
        public const string DeathLayerName = "Death";
        public const string ObstacleLayerName = "Obstacle";
        public const string GroundLayerName = "Ground";

        public static LayerMask ObstacleLayer => LayerMask.GetMask(ObstacleLayerName);
        public static LayerMask GroundLayer => LayerMask.GetMask(GroundLayerName);
        public static LayerMask DeathEnemyLayer => LayerMask.GetMask(DeathLayerName);

        public static LayerMask SolidObjectLayer => LayerMask.GetMask(ObstacleLayerName, GroundLayerName);

        public static LayerMask TargetHitLayer(string seftLayerName) { 
            return seftLayerName switch
            {
                PlayerLayerName => LayerMask.GetMask(EnemyLayerName),
                EnemyLayerName => LayerMask.GetMask(PlayerLayerName),
                _ => 0,
            };
        }

        public static LayerMask NameToLayerMask(string layerName)
        {
            var layerIndex = LayerMask.NameToLayer(layerName);
            if (layerIndex < 0)
            {
                Debug.LogWarning($"Layer '{layerName}' does not exist.");
                return 0;
            }

            return (LayerMask)(1 << layerIndex);
        }

        public static LayerMask IndexToLayer(int layerIndex) {
            return (LayerMask)(1 << layerIndex);
        }
    }
}
