using Infrastructure;
using Infrastructure.Misc;
using UnityEngine;

namespace Spawn.Walls
{
    public class WallPositionCalculator
    {
        private const int StartWallsOffset = 10;

        public Vector3 CalculatePosition(Vector3 startPoint, Quaternion newRotation, Transform previousWallPiece,
            int side)
        {
            if (previousWallPiece == null)
                return startPoint+Vector3.right*StartWallsOffset*(side-0.5f);
            else
            {
                float lengthOfPiece = CachedParameters.wallsLength;
                Vector3 returningPosition = new Vector3(CalculateX(previousWallPiece,lengthOfPiece,newRotation),
                    startPoint.y,CalculateZ(previousWallPiece,lengthOfPiece,newRotation));

                return returningPosition;
            }
        }

        float CalculateX(Transform previousWallPiece, float length, Quaternion newRotation)
        {
            float previousWallAngle = CalculateAngleInDegrees(previousWallPiece.rotation);
            float newWallAngle = CalculateAngleInDegrees(newRotation);
            return previousWallPiece.transform.position.x + length* (Mathf.Sin(previousWallAngle)+Mathf.Sin(newWallAngle))/2;
        }

        float CalculateZ(Transform  previousWallPiece, float length, Quaternion newRotation)
        {
            float newWallAngle = CalculateAngleInDegrees(newRotation);
            float previousWallAngle = CalculateAngleInDegrees(previousWallPiece.rotation);
            return previousWallPiece.transform.position.z + length* (Mathf.Cos(previousWallAngle)+Mathf.Cos(newWallAngle))/2;
        }

        private float CalculateAngleInDegrees(Quaternion newRotation)
        {
            return (newRotation.eulerAngles.y * Mathf.PI) / 180;
        }
    }
}