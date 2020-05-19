using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KO.TBLCryptoEditor.Utils
{
    public static class Utility
    {
        public static string TimeStamp => DateTime.Now.ToString("T");

        public static void InvokeSafe(this ISynchronizeInvoke caller, Action method)
        {
            if (caller.InvokeRequired)
                caller.Invoke(method, null);
            else
                method.Invoke();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static void MemSet(this byte[] source, byte value)
        {
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = value;
            }
        }

        public static int Read(this MemoryStream source, byte[] buffer, int offset, int count, int fromPosition)
        {
            source.Position += fromPosition;
            return source.Read(buffer, offset, count);
        }

        public static T ReadStructure<T>(this Stream stream) where T : struct
        {
            if (stream == null)
                throw new ArgumentNullException();

            int size = Marshal.SizeOf(typeof(T));
            byte[] bytes = new byte[size];
            if (stream.Read(bytes, 0, size) != size)
                throw new EndOfStreamException();

            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally {
                handle.Free();
            }
        }

        public static void CopyFilesRecursively(this DirectoryInfo src, DirectoryInfo dest)
        {
            foreach (DirectoryInfo di in src.GetDirectories())
                CopyFilesRecursively(di, dest.CreateSubdirectory(di.Name));

            foreach (FileInfo fi in src.GetFiles())
                fi.CopyTo(Path.Combine(dest.FullName, fi.Name));
        }

        public static bool IsFileLocked(string filePath)
        {
            List<Process> lockers = FileUtil.WhoIsLocking(filePath);
            return lockers.Any();
        }

        public static string WhoIsFileLocking(string filePath)
        {
            List<Process> lockers = FileUtil.WhoIsLocking(filePath);
            if (!lockers.Any())
                return String.Empty;

            string procs = String.Empty;
            lockers.ForEach(p => procs += $"{p.ProcessName}.exe, ");
            return procs.Substring(0, procs.Length - 2);
        }
    }
}
