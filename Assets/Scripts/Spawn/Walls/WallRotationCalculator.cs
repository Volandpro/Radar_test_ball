using Infrastructure;
using Infrastructure.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawn.Walls
{
    public class WallRotationCalculator
    {
        public const float Angle=30;
        public const float MinOffset = 2;
        public const float MaxOffset = 10;
        public Quaternion CalculateRotation(float side, Transform previousWallPiece, Vector3 initialSpawnPoint)
        {
            float minimumAngle = -Angle;
            float maximumAngle = Angle;
            if (previousWallPiece != null)
            {
                minimumAngle = CalculateMinimumAngle(side, previousWallPiece, initialSpawnPoint);
                maximumAngle = CalculateMaximumAngle(side, previousWallPiece, initialSpawnPoint);
            }

            return Quaternion.Euler(new Vector3(0,Random.Range(minimumAngle,maximumAngle),0));
        }

        float CalculateMinimumAngle(float side, Transform previousWallPiece, Vector3 initialSpawnPoint)
        {
            float angle = -Angle;
            float previousWallAngle = CalculateAngleInDegrees(previousWallPiece.rotation);
            float endPositionOfPrevious = previousWallPiece.transform.position.x+CachedParameters.wallsLength * Mathf.Sin(previousWallAngle)/2;
            float maxOffsetPosition = initialSpawnPoint.x + MaxOffset * (side - 0.5f) * 2;
            float deltaPosition = Mathf.Abs(maxOffsetPosition - endPositionOfPrevious);
            
            if (deltaPosition / CachedParameters.wallsLength < 1f)
            {
                angle = Mathf.Acos(deltaPosition / CachedParameters.wallsLength);
                angle = (angle / Mathf.PI) * 180;
                angle -= 90;
               
                if (Mathf.Abs(angle) >Angle)
                    angle = -Angle;
                if (side > 0)
                    angle *= -1;
            }

            return angle;
        }
        float CalculateMaximumAngle(float side, Transform previousWallPiece, Vector3 initialSpawnPoint)
        {
            float angle = Angle;
            float previousWallAngle = CalculateAngleInDegrees(previousWallPiece.rotation);
            float endPositionOfPrevious = previousWallPiece.transform.position.x+CachedParameters.wallsLength * Mathf.Sin(previousWallAngle)/2 ;
            float minOffsetPosition = initialSpawnPoint.x + MinOffset * (side - 0.5f) * 2;
            float deltaPosition = Mathf.Abs(minOffsetPosition - endPositionOfPrevious);

            if (deltaPosition / CachedParameters.wallsLength < 1f)
            {
                angle = Mathf.Asin(deltaPosition / CachedParameters.wallsLength);
                angle = (angle / Mathf.PI) * 180;
                if (Mathf.Abs(angle) >Angle)
                    angle = Angle;
                if (side > 0)
                    angle *= -1;
            }

            return angle;
        }
        
        private float CalculateAngleInDegrees(Quaternion newRotation)
        {
            return (newRotation.eulerAngles.y * Mathf.PI) / 180;
        }
    }
}