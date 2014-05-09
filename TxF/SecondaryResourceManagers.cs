#region Using directives

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TxF.WindowsApi;

#endregion

namespace TxF
{
    public class SecondaryResourceManagers
    {
        public SecondaryResourceManagers()
        {
            throw new NotImplementedException();

            if (!Transaction.IsSupported) {
                new FileTransactedException("TxF Transactional NTFS not supported in this version operating system.");
            }
        }

        private IntPtr _GetPointDirectory(string pathDirectory)
        {
            IntPtr p = apiwindows.CreateFileW(pathDirectory,
                apiwindows.DesiredAccess.GENERIC_WRITE,
                apiwindows.ShareMode.FILE_SHARE_WRITE,
                new apiwindows.LPSECURITY_ATTRIBUTES(),
                apiwindows.CreationDisposition.OPEN_EXISTING,
                apiwindows.FlagsAndAttributes.FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);

            if (p.ToInt32() == apiwindows.INVALID_HANDLE_VALUE) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return p;
        }

        public void Create(string pathDirectory)
        {
            try {
                IntPtr directory = _GetPointDirectory(pathDirectory);
                int lpBytesReturned = 0;
                var ol = new apiwindows.LPOVERLAPPED();
                int err = apiwindows.DeviceIoControl(directory, apiwindows.FSCTL_TXFS_CREATE_SECONDARY_RM, IntPtr.Zero,
                    0, IntPtr.Zero, 0, ref lpBytesReturned, ol);

                if (err == 0) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            catch (Exception) {
                throw;
            }
            finally {
            }
        }

        public void Start(string pathDirectory)
        {
            try {
                IntPtr directory = _GetPointDirectory(pathDirectory);
                int lpBytesReturned = 0;
                var ol = new apiwindows.LPOVERLAPPED();
                int err = apiwindows.DeviceIoControl(directory, apiwindows.FSCTL_TXFS_START_RM,
                    new apiwindows.TXFS_START_RM_INFORMATION(), 0, IntPtr.Zero, 0, ref lpBytesReturned, ol);

                if (err == 0) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                err = apiwindows.DeviceIoControl(directory, apiwindows.FSCTL_TXFS_ROLLFORWARD_REDO,
                    new apiwindows.TXFS_ROLLFORWARD_REDO_INFORMATION(), 0, IntPtr.Zero, 0, ref lpBytesReturned, ol);
                if (err == 0) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                err = apiwindows.DeviceIoControl(directory, apiwindows.FSCTL_TXFS_ROLLFORWARD_UNDO, IntPtr.Zero, 0,
                    IntPtr.Zero, 0, ref lpBytesReturned, ol);
                if (err == 0) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            catch (Exception) {
                throw;
            }
            finally {
            }
        }

        public void Close(string pathDirectory)
        {
            try {
                IntPtr directory = _GetPointDirectory(pathDirectory);
                int lpBytesReturned = 0;
                var ol = new apiwindows.LPOVERLAPPED();
                int err = apiwindows.DeviceIoControl(directory, apiwindows.FSCTL_TXFS_SHUTDOWN_RM, IntPtr.Zero, 0,
                    IntPtr.Zero, 0, ref lpBytesReturned, ol);

                if (err == 0) {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            catch (Exception) {
                throw;
            }
            finally {
            }
        }
    }
}