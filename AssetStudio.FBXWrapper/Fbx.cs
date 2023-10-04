﻿using AssetStudio.FbxInterop;
using AssetStudio.PInvoke;
using System.IO;

namespace AssetStudio
{
    public static partial class Fbx
    {

        static Fbx()
        {
            DllLoader.PreloadDll(FbxDll.DllName);
        }

        public static Vector3 QuaternionToEuler(Quaternion q)
        {
            AsUtilQuaternionToEuler(q.X, q.Y, q.Z, q.W, out var x, out var y, out var z);
            return new Vector3(x, y, z);
        }

        public static Quaternion EulerToQuaternion(Vector3 v)
        {
            AsUtilEulerToQuaternion(v.X, v.Y, v.Z, out var x, out var y, out var z, out var w);
            return new Quaternion(x, y, z, w);
        }

        public static class Exporter
        {

            public static void Export(string path, IImported imported)
            {
                var file = new FileInfo(path);
                var dir = file.Directory;

                if (!dir.Exists)
                {
                    dir.Create();
                }

                var currentDir = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(dir.FullName);

                var name = Path.GetFileName(path);

                using (var exporter = new FbxExporter(name, imported))
                {
                    exporter.Initialize();
                    exporter.ExportAll();
                }

                Directory.SetCurrentDirectory(currentDir);
            }

        }

    }
}
