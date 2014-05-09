#region Using directives

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;
using TxF.WindowsApi;

#endregion

namespace TxF
{
    public class Transaction : IDisposable
    {
        public Transaction()
        {
            if (!IsSupported) {
                new FileTransactedException("TxF Transactional NTFS not supported in this version operating system.");
            }

            TransactionHandle = IntPtr.Zero;
            if (System.Transactions.Transaction.Current != null) {
                TransactionHandle = _GetPtrFromDtc();
            }
            else {
                Create();
            }
        }

        public Transaction(bool useTransactionScope)
        {
            if (!IsSupported) {
                new FileTransactedException("TxF Transactional NTFS not supported in this version operating system.");
            }

            TransactionHandle = IntPtr.Zero;
            if (useTransactionScope) {
                TransactionHandle = _GetPtrFromDtc();
            }
            else {
                Create();
            }
        }

        public static bool IsSupported
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if (Environment.OSVersion.Version.Major >= 6) {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            private set { }
        }

        public IntPtr TransactionHandle { get; private set; }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion

        private IntPtr _GetPtrFromDtc()
        {
            if (System.Transactions.Transaction.Current == null) {
                throw new FileTransactedException("TransactionScope not initialized.");
            }

            var dtcT =
                (apiwindows.IKernelTransaction)
                    TransactionInterop.GetDtcTransaction(System.Transactions.Transaction.Current);
            IntPtr result = IntPtr.Zero;
            dtcT.GetHandle(out result);
            return result;
        }

        private IntPtr Create()
        {
            var lpTransactionAttributes = new apiwindows.LPSECURITY_ATTRIBUTES();
            var UOW = new apiwindows.LPGUID();
            UOW.Value = IntPtr.Zero;
            int CreateOptions = 0;
            int IsolationLevel = 0;
            int IsolationFlags = 0;
            int Timeout = 0;
            var Description = new StringBuilder("ND");
            IntPtr transactionHandle = apiwindows.CreateTransaction(lpTransactionAttributes, UOW, CreateOptions,
                IsolationLevel, IsolationFlags, Timeout, Description);

            if (transactionHandle == IntPtr.Zero) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            TransactionHandle = transactionHandle;
            return transactionHandle;
        }

        public int Commit()
        {
            int result = apiwindows.CommitTransaction(TransactionHandle);

            if (result == 0) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return result;
        }

        public int Rollback()
        {
            int result = apiwindows.RollbackTransaction(TransactionHandle);

            if (result == 0) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return result;
        }

        public int Close()
        {
            int result = apiwindows.CloseHandle(TransactionHandle);

            if (result == 0) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            return result;
        }
    }
}