using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.TP.Service
{
    public static class ObjectExtension
    {
        public static byte[] SerializeToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static byte[] SerializeToByteArray(this object[] objs)
        {
            if (objs == null)
            {
                return null;
            }
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, objs);
                return ms.ToArray();
            }
        }

        //public static T Deserialize<T>(this byte[] byteArray) where T : class
        //{
        //    if (byteArray == null)
        //    {
        //        return null;
        //    }
        //    using (var memStream = new MemoryStream())
        //    {
        //        var binForm = new BinaryFormatter();
        //        memStream.Write(byteArray, 0, byteArray.Length);
        //        memStream.Seek(0, SeekOrigin.Begin);
        //        var obj = (T)binForm.Deserialize(memStream);
        //        return obj;
        //    }
        //}

        public static T Deserialize<T>(this byte[] byteArray)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
