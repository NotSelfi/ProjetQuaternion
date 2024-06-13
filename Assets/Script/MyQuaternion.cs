using UnityEngine;

public class MyQuaternion
{
    public float w, x, y, z;

    public MyQuaternion(float w, float x, float y, float z)
    {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Norme du quaternion
    public float Norm()
    {
        return Mathf.Sqrt(w * w + x * x + y * y + z * z);
    }

    // Conjugaison du quaternion
    public MyQuaternion Conjugate()
    {
        return new MyQuaternion(w, -x, -y, -z);
    }

    // Multiplication de quaternions
    public static MyQuaternion operator *(MyQuaternion q1, MyQuaternion q2)
    {
        return new MyQuaternion(
            q1.w * q2.w - q1.x * q2.x - q1.y * q2.y - q1.z * q2.z,
            q1.w * q2.x + q1.x * q2.w + q1.y * q2.z - q1.z * q2.y,
            q1.w * q2.y - q1.x * q2.z + q1.y * q2.w + q1.z * q2.x,
            q1.w * q2.z + q1.x * q2.y - q1.y * q2.x + q1.z * q2.w
        );
    }

    // Normalisation du quaternion
    public void Normalize()
    {
        float norm = Norm();
        if (norm > 0.0001f)
        {
            w /= norm;
            x /= norm;
            y /= norm;
            z /= norm;
        }
    }

    // Conversion d'un quaternion en matrice de rotation
    public Matrix4x4 ToMatrix()
    {
        float xx = x * x, yy = y * y, zz = z * z;
        float xy = x * y, xz = x * z, yz = y * z;
        float wx = w * x, wy = w * y, wz = w * z;

        Matrix4x4 matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(1 - 2 * (yy + zz), 2 * (xy - wz), 2 * (xz + wy), 0));
        matrix.SetRow(1, new Vector4(2 * (xy + wz), 1 - 2 * (xx + zz), 2 * (yz - wx), 0));
        matrix.SetRow(2, new Vector4(2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (xx + yy), 0));
        matrix.SetRow(3, new Vector4(0, 0, 0, 1));

        return matrix;
    }

    // Conversion d'une matrice de rotation en quaternion
    public static MyQuaternion FromMatrix(Matrix4x4 matrix)
    {
        float trace = matrix.m00 + matrix.m11 + matrix.m22;
        if (trace > 0)
        {
            float s = 0.5f / Mathf.Sqrt(trace + 1.0f);
            return new MyQuaternion(
                0.25f / s,
                (matrix.m21 - matrix.m12) * s,
                (matrix.m02 - matrix.m20) * s,
                (matrix.m10 - matrix.m01) * s
            );
        }
        else
        {
            if (matrix.m00 > matrix.m11 && matrix.m00 > matrix.m22)
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix.m00 - matrix.m11 - matrix.m22);
                return new MyQuaternion(
                    (matrix.m21 - matrix.m12) / s,
                    0.25f * s,
                    (matrix.m01 + matrix.m10) / s,
                    (matrix.m02 + matrix.m20) / s
                );
            }
            else if (matrix.m11 > matrix.m22)
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix.m11 - matrix.m00 - matrix.m22);
                return new MyQuaternion(
                    (matrix.m02 - matrix.m20) / s,
                    (matrix.m01 + matrix.m10) / s,
                    0.25f * s,
                    (matrix.m12 + matrix.m21) / s
                );
            }
            else
            {
                float s = 2.0f * Mathf.Sqrt(1.0f + matrix.m22 - matrix.m00 - matrix.m11);
                return new MyQuaternion(
                    (matrix.m10 - matrix.m01) / s,
                    (matrix.m02 + matrix.m20) / s,
                    (matrix.m12 + matrix.m21) / s,
                    0.25f * s
                );
            }
        }
    }
}
