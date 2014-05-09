#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace TxF.WindowsApi
{

    #region HandlePoint Class -Dismissed-

    //class HandlePoint : IDisposable
    //{
    //    private GCHandle _h;
    //    public HandlePoint(object o)
    //    {
    //        _h = GCHandle.Alloc(o, GCHandleType.Pinned);
    //        try
    //        {
    //            IntPtr oIntPtr = GCHandle.ToIntPtr(_h);
    //            this.ObjPoiter = oIntPtr;
    //        }
    //        catch (Exception)
    //        {
    //            this.Dispose();
    //            throw;
    //        }
    //    }

    //    public IntPtr ObjPoiter
    //    { get; private set; }

    //    public void Dispose()
    //    {
    //        if(_h!=null )
    //            _h.Free();
    //    }
    //}

    #endregion

    internal static class apiwindows
    {
        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int LPPROGRESS_ROUTINE(
            LARGE_INTEGER TotalFileSize,
            LARGE_INTEGER TotalBytesTransferred,
            LARGE_INTEGER StreamSize,
            LARGE_INTEGER StreamBytesTransferred,
            int dwStreamNumber,
            int dwCallbackReason,
            IntPtr hSourceFile,
            IntPtr hDestinationFile,
            IntPtr lpData);

        #endregion

        #region CopyFile

        public enum COPY_FLAGS
        {
            COPY_FILE_COPY_ND = 0,
            COPY_FILE_COPY_SYMLINK = 0x800,
            COPY_FILE_FAIL_IF_EXISTS = 0x1,
            COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x4,
            COPY_FILE_RESTARTABLE = 0x2
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CopyFileTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpExistingFileName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpNewFileName,
            LPPROGRESS_ROUTINE lpProgressRoutine,
            IntPtr lpData,
            ref int pbCancel,
            COPY_FLAGS dwCopyFlags,
            IntPtr hTransaction);

        #endregion

        #region CreateFileTransacted

        #region CreationDisposition enum

        public enum CreationDisposition
        {
            CREATE_ALWAYS = 0x2,
            CREATE_NEW = 0x1,
            OPEN_ALWAYS = 0x4,
            OPEN_EXISTING = 0x3,
            TRUNCATE_EXISTING = 0x5
        }

        #endregion

        #region DesiredAccess enum

        public enum DesiredAccess
        {
            GENERIC_READ = unchecked((int) 0x80000000),
            GENERIC_WRITE = 0x40000000
        }

        #endregion

        #region FlagsAndAttributes enum

        public enum FlagsAndAttributes
        {
            FILE_ATTRIBUTE_ARCHIVE = 0x20,
            FILE_ATTRIBUTE_ENCRYPTED = 0x4000,
            FILE_ATTRIBUTE_HIDDEN = 0x2,
            FILE_ATTRIBUTE_NORMAL = 0x80,
            FILE_ATTRIBUTE_OFFLINE = 0x1000,
            FILE_ATTRIBUTE_READONLY = 0x1,
            FILE_ATTRIBUTE_SYSTEM = 0x4,
            FILE_ATTRIBUTE_TEMPORARY = 0x100,
            FILE_FLAG_BACKUP_SEMANTICS = 0x2000000,
            FILE_FLAG_DELETE_ON_CLOSE = 0x4000000,
            FILE_FLAG_NO_BUFFERING = 0x20000000,
            FILE_FLAG_OPEN_NO_RECALL = 0x100000,
            FILE_FLAG_OPEN_REPARSE_POINT = 0x200000,
            FILE_FLAG_OVERLAPPED = 0x40000000,
            FILE_FLAG_POSIX_SEMANTICS = 0x1000000,
            FILE_FLAG_RANDOM_ACCESS = 0x10000000,
            FILE_FLAG_SEQUENTIAL_SCAN = 0x8000000,
            FILE_FLAG_WRITE_THROUGH = unchecked((int) 0x80000000)
        }

        #endregion

        #region MiniVersion enum

        public enum MiniVersion
        {
            TXFS_MINIVERSION_COMMITTED_VIEW = 0x0000,
            TXFS_MINIVERSION_DIRTY_VIEW = 0xFFFF,
            TXFS_MINIVERSION_DEFAULT_VIEW = 0xFFFE
        }

        #endregion

        #region ShareMode enum

        public enum ShareMode
        {
            FILE_SHARE_ND = 0,
            FILE_SHARE_DELETE = 0x4,
            FILE_SHARE_READ = 0x1,
            FILE_SHARE_WRITE = 0x2
        }

        #endregion

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateFileW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            DesiredAccess dwDesiredAccess,
            ShareMode dwShareMode,
            LPSECURITY_ATTRIBUTES lpSecurityAttributes,
            CreationDisposition dwCreationDisposition,
            FlagsAndAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateFileTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            DesiredAccess dwDesiredAccess,
            ShareMode dwShareMode,
            LPSECURITY_ATTRIBUTES lpSecurityAttributes,
            CreationDisposition dwCreationDisposition,
            FlagsAndAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile,
            IntPtr hTransaction,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pusMiniVersion,
            IntPtr lpExtendedParameter);

        #endregion

        #region WriteFile & ReadFile

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void LPOVERLAPPED_COMPLETION_ROUTINE(
            int dwErrorCode, int dwNumberOfBytesTransfered, LPOVERLAPPED lpOverlapped);

        #endregion

        //[System.Runtime.InteropServices.DllImport("kernel32.dll",SetLastError = true,CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //public extern static int WriteFileEx(System.IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, LPOVERLAPPED lpOverlapped, LPOVERLAPPED_COMPLETION_ROUTINE lpCompletionRoutine);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int WriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToWrite,
            ref int lpNumberOfBytesWritten, LPOVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead,
            ref int lpNumberOfBytesRead, LPOVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFileSize(IntPtr hFile, ref int lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OpenFile([In] [MarshalAs(UnmanagedType.LPStr)] StringBuilder lpFileName,
            ref OFSTRUCT lpReOpenBuff, Style uStyle);

        #region Nested type: LPOVERLAPPED

        [StructLayout(LayoutKind.Sequential)]
        public struct LPOVERLAPPED
        {
            public IntPtr Value;
        }

        #endregion

        #region Nested type: OFSTRUCT

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct OFSTRUCT
        {
            public byte cBytes;
            public byte fFixedDisk;
            public short nErrCode;
            public short Reserved1;
            public short Reserved2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public string szPathName;
        }

        #endregion

        #endregion

        #region Delete & Move File

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeleteFileTransactedW([MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            IntPtr hTransaction);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int MoveFileTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpExistingFileName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpNewFileName,
            LPPROGRESS_ROUTINE lpProgressRoutine,
            IntPtr lpData,
            int dwFlags,
            IntPtr hTransaction);

        #endregion

        #region Style enum

        public enum Style
        {
            OF_CANCEL = 0x800,
            OF_CREATE = 0x1000,
            OF_DELETE = 0x200,
            OF_EXIST = 0x4000,
            OF_PARSE = 0x100,
            OF_PROMPT = 0x2000,
            OF_READ = 0x0,
            OF_READWRITE = 0x2,
            OF_REOPEN = 0x8000,
            OF_SHARE_COMPAT = 0x0,
            OF_SHARE_DENY_NONE = 0x40,
            OF_SHARE_DENY_READ = 0x30,
            OF_SHARE_DENY_WRITE = 0x20,
            OF_SHARE_EXCLUSIVE = 0x10,
            OF_VERIFY = 0x400,
            OF_WRITE = 0x1
        }

        #endregion

        #region Folders

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateDirectoryTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpTemplateDirectory,
            [MarshalAs(UnmanagedType.LPWStr)] string lpNewDirectory,
            LPSECURITY_ATTRIBUTES lpSecurityAttributes,
            IntPtr hTransaction);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int RemoveDirectoryTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpPathName,
            IntPtr hTransaction);

        #endregion

        #region Transaction

        [DllImport("ktmw32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CreateTransaction(
            LPSECURITY_ATTRIBUTES lpTransactionAttributes,
            LPGUID UOW,
            int CreateOptions,
            int IsolationLevel,
            int IsolationFlags,
            int Timeout,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder Description);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CloseHandle(IntPtr hObject);

        [DllImport("ktmw32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CommitTransaction(IntPtr TransactionHandle);

        [DllImport("ktmw32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int RollbackTransaction(IntPtr TransactionHandle);

        #region Nested type: IKernelTransaction

        [Guid("79427A2B-F895-40e0-BE79-B57DC82ED231")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IKernelTransaction
        {
            int GetHandle(out IntPtr pHandle);
        }

        #endregion

        #region Nested type: LPGUID

        [StructLayout(LayoutKind.Sequential)]
        public struct LPGUID
        {
            public IntPtr Value;
        }

        #endregion

        #region Nested type: LPSECURITY_ATTRIBUTES

        [StructLayout(LayoutKind.Sequential)]
        public struct LPSECURITY_ATTRIBUTES
        {
            public IntPtr Value;
        }

        #endregion

        //[System.Runtime.InteropServices.DllImport("ktmw32.dll",SetLastError = true,CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        //public extern static System.IntPtr OpenTransaction(int dwDesiredAccess,LPGUID TransactionId);

        //public enum TransactionAccessMasks:int
        //{
        //    TRANSACTION_QUERY_INFORMATION = 0x1,
        //    TRANSACTION_SET_INFORMATION = 0x2,
        //    TRANSACTION_ENLIST = 0x4,
        //    TRANSACTION_COMMIT = 0x8,
        //    TRANSACTION_ROLLBACK = 0x10,
        //    TRANSACTION_PROPAGATE = 0x20,
        //    TRANSACTION_GENERIC_READ = 0x120001,
        //    TRANSACTION_GENERIC_WRITE = 0x12003E,
        //    TRANSACTION_GENERIC_EXECUTE = 0x120018,
        //    TRANSACTION_ALL_ACCESS = 0x1F003F,
        //    TRANSACTION_RESOURCE_MANAGER_RIGHTS = 0x120037
        //}

        #endregion

        #region Find Files

        #region FINDEX_INFO_LEVELS enum

        public enum FINDEX_INFO_LEVELS
        {
            FindExInfoStandard,
            FindExInfoBasic,
            FindExInfoMaxInfoLevel
        }

        #endregion

        #region FINDEX_SEARCH_OPS enum

        public enum FINDEX_SEARCH_OPS
        {
            FindExSearchNameMatch,
            FindExSearchLimitToDirectories,
            FindExSearchLimitToDevices,
            FindExSearchMaxSearchOp
        }

        #endregion

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstFileTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            FINDEX_INFO_LEVELS fInfoLevelId,
            out WIN32_FIND_DATAW lpFindFileData,
            FINDEX_SEARCH_OPS fSearchOp,
            IntPtr lpSearchFilter,
            int dwAdditionalFlags,
            IntPtr hTransaction);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindNextFileW(IntPtr hFindFile, out WIN32_FIND_DATAW lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindClose(IntPtr hFindFile);

        #region Nested type: FILETIME

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public int dwLowDateTime;
            public int dwHighDateTime;
        }

        #endregion

        #region Nested type: WIN32_FIND_DATAW

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATAW
        {
            public int dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)] public string cAlternateFileName;
        }

        #endregion

        #endregion

        #region Get and Set Attributes File

        #region FILE_ATTRIBUTE enum

        public enum FILE_ATTRIBUTE
        {
            FILE_ATTRIBUTE_ARCHIVE = 0x20,
            FILE_ATTRIBUTE_HIDDEN = 0x2,
            FILE_ATTRIBUTE_NORMAL = 0x80,
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x2000,
            FILE_ATTRIBUTE_OFFLINE = 0x1000,
            FILE_ATTRIBUTE_READONLY = 0x1,
            FILE_ATTRIBUTE_SYSTEM = 0x4,
            FILE_ATTRIBUTE_TEMPORARY = 0x100,

            //Not supported from TxF
            FILE_ATTRIBUTE_COMPRESSED = 0x800,
            FILE_ATTRIBUTE_DEVICE = 0x40,
            FILE_ATTRIBUTE_DIRECTORY = 0x10,
            FILE_ATTRIBUTE_ENCRYPTED = 0x4000,
            FILE_ATTRIBUTE_REPARSE_POINT = 0x400,
            FILE_ATTRIBUTE_SPARSE_FILE = 0x200,
            FILE_ATTRIBUTE_VIRTUAL = 0x10000
        }

        #endregion

        #region GET_FILEEX_INFO_LEVELS enum

        public enum GET_FILEEX_INFO_LEVELS
        {
            GetFileExInfoStandard,
            GetFileExMaxInfoLevel
        }

        #endregion

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetFileAttributesTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            GET_FILEEX_INFO_LEVELS fInfoLevelId,
            out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation,
            IntPtr hTransaction);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetFileAttributesTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            FILE_ATTRIBUTE dwFileAttributes,
            IntPtr hTransaction);

        [StructLayout(LayoutKind.Sequential)]
        public struct WIN32_FILE_ATTRIBUTE_DATA
        {
            public int dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
        }

        #endregion

        #region HardLink & SymbolicLink

        public enum TypeSymbolicLink
        {
            File = 0,
            SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int CreateHardLinkTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpExistingFileName,
            LPSECURITY_ATTRIBUTES lpSecurityAttributes,
            IntPtr hTransaction);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern byte CreateSymbolicLinkTransactedW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpSymlinkFileName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpTargetFileName,
            TypeSymbolicLink dwFlags,
            IntPtr hTransaction);

        #endregion

        #region SECONDARY_RM

        #region TXFS_ROLLFORWARD_REDO_INFORMATION_Flags enum

        public enum TXFS_ROLLFORWARD_REDO_INFORMATION_Flags
        {
            TXFS_ROLLFORWARD_REDO_FLAG_USE_LAST_REDO_LSN = 0x1,
            TXFS_ROLLFORWARD_REDO_FLAG_USE_LAST_VIRTUAL_CLOCK = 0x2
        }

        #endregion

        #region TXFS_START_RM_INFORMATION_Flags enum

        public enum TXFS_START_RM_INFORMATION_Flags
        {
            TXFS_START_RM_FLAG_LOG_CONTAINER_COUNT_MAX = 0x1,
            TXFS_START_RM_FLAG_LOG_CONTAINER_COUNT_MIN = 0x2,
            TXFS_START_RM_FLAG_LOG_CONTAINER_SIZE = 0x4,
            TXFS_START_RM_FLAG_LOG_GROWTH_INCREMENT_NUM_CONTAINERS = 0x8,
            TXFS_START_RM_FLAG_LOG_GROWTH_INCREMENT_PERCENT = 0x10,
            TXFS_START_RM_FLAG_LOG_AUTO_SHRINK_PERCENTAGE = 0x20,
            TXFS_START_RM_FLAG_LOG_NO_CONTAINER_COUNT_MAX = 0x40,
            TXFS_START_RM_FLAG_LOG_NO_CONTAINER_COUNT_MIN = 0x80,
            TXFS_START_RM_FLAG_RECOVER_BEST_EFFORT = 0x200,
            TXFS_START_RM_FLAG_LOGGING_MODE = 0x400,
            TXFS_START_RM_FLAG_PRESERVE_CHANGES = 0x800,
            TXFS_START_RM_FLAG_PREFER_CONSISTENCY = 0x1000,
            TXFS_START_RM_FLAG_PREFER_AVAILABILITY = 0x2000
        }

        #endregion

        #region TXFS_START_RM_INFORMATION_LoggingMode enum

        public enum TXFS_START_RM_INFORMATION_LoggingMode
        {
            TXFS_LOGGING_MODE_SIMPLE = 0x1,
            TXFS_LOGGING_MODE_FULL = 0x2
        }

        #endregion

        public static readonly int FILE_DEVICE_FILE_SYSTEM = 0x9;
        public static readonly int METHOD_BUFFERED = 0x0;
        public static readonly int FILE_WRITE_DATA = 0x2;
        public static readonly int FILE_READ_DATA = 0x1;

        public static readonly int FSCTL_TXFS_CREATE_SECONDARY_RM = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 90,
            METHOD_BUFFERED, FILE_WRITE_DATA);

        public static readonly int FSCTL_TXFS_START_RM = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 86, METHOD_BUFFERED,
            FILE_WRITE_DATA);

        public static readonly int FSCTL_TXFS_ROLLFORWARD_REDO = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 84, METHOD_BUFFERED,
            FILE_WRITE_DATA);

        public static readonly int FSCTL_TXFS_ROLLFORWARD_UNDO = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 85, METHOD_BUFFERED,
            FILE_WRITE_DATA);

        public static readonly int FSCTL_TXFS_SHUTDOWN_RM = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 87, METHOD_BUFFERED,
            FILE_WRITE_DATA);

        public static readonly int FSCTL_TXFS_LIST_TRANSACTIONS = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 121, METHOD_BUFFERED,
            FILE_READ_DATA);

        public static readonly int FSCTL_TXFS_TRANSACTION_ACTIVE = CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 99, METHOD_BUFFERED,
            FILE_READ_DATA);

        private static int CTL_CODE(int DeviceType, int Function, int Method, int Access)
        {
            return (((DeviceType) << 16) | ((Access) << 14) | ((Function) << 2) | (Method));
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeviceIoControl(
            IntPtr hDevice,
            int dwIoControlCode,
            IntPtr lpInBuffer,
            int nInBufferSize,
            IntPtr lpOutBuffer,
            int nOutBufferSize,
            ref int lpBytesReturned,
            LPOVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeviceIoControl(
            IntPtr hDevice,
            int dwIoControlCode,
            TXFS_ROLLFORWARD_REDO_INFORMATION lpInBuffer,
            int nInBufferSize,
            IntPtr lpOutBuffer,
            int nOutBufferSize,
            ref int lpBytesReturned,
            LPOVERLAPPED lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int DeviceIoControl(
            IntPtr hDevice,
            int dwIoControlCode,
            TXFS_START_RM_INFORMATION lpInBuffer,
            int nInBufferSize,
            IntPtr lpOutBuffer,
            int nOutBufferSize,
            ref int lpBytesReturned,
            LPOVERLAPPED lpOverlapped);

        #region Nested type: TXFS_ROLLFORWARD_REDO_INFORMATION

        [StructLayout(LayoutKind.Sequential)]
        public struct TXFS_ROLLFORWARD_REDO_INFORMATION
        {
            public LARGE_INTEGER LastVirtualClock;
            public long LastRedoLsn;
            public long HighestRecoveryLsn;
            public TXFS_ROLLFORWARD_REDO_INFORMATION_Flags Flags;
        }

        #endregion

        #region Nested type: TXFS_START_RM_INFORMATION

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TXFS_START_RM_INFORMATION
        {
            public TXFS_START_RM_INFORMATION_Flags Flags;
            public long LogContainerSize;
            public int LogContainerCountMin;
            public int LogContainerCountMax;
            public int LogGrowthIncrement;
            public int LogAutoShrinkPercentage;
            public int TmLogPathOffset;
            public short TmLogPathLength;
            public TXFS_START_RM_INFORMATION_LoggingMode LoggingMode;
            public short LogPathLength;
            public short Reserved;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)] public string LogPath;
        }

        #endregion

        #endregion

        public static readonly int INVALID_HANDLE_VALUE = unchecked((int) 0xFFFFFFFF);
        public static readonly int INVALID_FILE_SIZE = unchecked((int) 0xFFFFFFFF);
        public static readonly int HFILE_ERROR = unchecked((int) 0xFFFFFFFF);

        static apiwindows()
        {
        }

        #region Nested type: LARGE_INTEGER

        [StructLayout(LayoutKind.Explicit)]
        public struct LARGE_INTEGER
        {
            [FieldOffset(0)] public sType s;
            [FieldOffset(0)] public uType u;
            [FieldOffset(0)] public long QuadPart;

            [StructLayout(LayoutKind.Sequential)]
            public struct sType
            {
                public int LowPart;
                public int HighPart;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct uType
            {
                public int LowPart;
                public int HighPart;
            }
        }

        #endregion
    }
}