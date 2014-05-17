using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Bluephase.Data.EF6.Model;
using Transaction = TxF.Transaction;

namespace Bluephase.Domain.CommandHandlers
{
    public class CheckOutDocumentCommandHandler
    {
        public void Execute()
        {
            using (var transaction = new TransactionScope()) {
                try {
                    using (var entities = new BluephaseEntities()) {
                        var newAudit = new DocumentAudit() {
                            Action = "Checked out",
                            CreatedOn = DateTime.Now,
                            DocumentVersionId = 1,
                            EmployeeId = 1
                        };

                    }

                    using (var txfTransaction = new Transaction(true)) {
                        
                    }

                    transaction.Complete();
                }
                catch (Exception ex) {
                    transaction.Dispose();
                }
            }
        }
    }
}
