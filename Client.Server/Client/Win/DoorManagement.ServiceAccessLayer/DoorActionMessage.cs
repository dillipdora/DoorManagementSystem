
namespace DoorManagement.ServiceAccessLayer
{
    public class DoorActionMessage
    {
        public int Id { get; set; }

        public DoorAction DoorAction
        {
            get; set;
        }
    }

    public enum DoorAction
    {
        UnLock,
        Lock,
        Open,
        Close
    }
}