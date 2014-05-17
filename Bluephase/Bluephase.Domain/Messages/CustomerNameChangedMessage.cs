namespace Bluephase.Domain.Messages
{
    public class CustomerNameChangedMessage
    {
        public CustomerNameChangedMessage()
        {
        }

        public CustomerNameChangedMessage(int customerId, string newName)
        {
            CustomerId = customerId;
            NewName = newName;
        }

        public int CustomerId { get; set; }
        public string NewName { get; set; }
    }