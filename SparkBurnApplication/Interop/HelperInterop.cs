using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SparkBurnApplication.Interop
{
    internal class HelperInterop
    {
        public class ManagedIStream : IStream
        {
            private Stream _stream;

            public ManagedIStream(Stream stream)
            {
                _stream = stream;
            }

            public void Read(byte[] pv, int cb, IntPtr pcbRead)
            {
                int bytesRead = _stream.Read(pv, 0, cb);
                if (pcbRead != IntPtr.Zero)
                {
                    Marshal.WriteInt32(pcbRead, bytesRead);
                }
            }

            public void Write(byte[] pv, int cb, IntPtr pcbWritten)
            {
                _stream.Write(pv, 0, cb);
                if (pcbWritten != IntPtr.Zero)
                {
                    Marshal.WriteInt32(pcbWritten, cb);
                }
            }

            public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
            {
                long newPos = _stream.Seek(dlibMove, (SeekOrigin)dwOrigin);
                if (plibNewPosition != IntPtr.Zero)
                {
                    Marshal.WriteInt64(plibNewPosition, newPos);
                }
            }

            public void SetSize(long libNewSize)
            {
                _stream.SetLength(libNewSize);
            }

            public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
            {
                byte[] buffer = new byte[81920];
                long written = 0;
                while (written < cb)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    pstm.Write(buffer, bytesRead, IntPtr.Zero);
                    written += bytesRead;
                }

                if (pcbRead != IntPtr.Zero)
                    Marshal.WriteInt64(pcbRead, written);

                if (pcbWritten != IntPtr.Zero)
                    Marshal.WriteInt64(pcbWritten, written);
            }

            public void Commit(int grfCommitFlags)
            {
                _stream.Flush();
            }

            public void Revert()
            {
                throw new NotSupportedException();
            }

            public void LockRegion(long libOffset, long cb, int dwLockType)
            {
                throw new NotSupportedException();
            }

            public void UnlockRegion(long libOffset, long cb, int dwLockType)
            {
                throw new NotSupportedException();
            }

            public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
            {
                pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG();
                pstatstg.cbSize = _stream.Length;
                pstatstg.type = 2; // stream type
                pstatstg.grfMode = _stream.CanRead ? 0x00000000 : 0; // read mode
            }

            public void Clone(out IStream ppstm)
            {
                throw new NotSupportedException();
            }
        }
    }
}
