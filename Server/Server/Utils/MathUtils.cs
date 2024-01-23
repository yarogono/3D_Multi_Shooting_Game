using Google.Protobuf.Protocol;
using System.Numerics;

namespace Server.Utils
{
    public static class MathUtils
    {
        public static float Vector3Distance(Vec3 positionOne, Vec3 positionTwo)
        {
            Vector3 posOne = new Vector3() { X = positionOne.X, Y = positionOne.X, Z = positionOne.Z };
            Vector3 posTwo = new Vector3() { X = positionTwo.X, Y = positionTwo.Y, Z = positionTwo.Z };

            return Vector3.Distance(posOne, posTwo);
        }
    }
}
