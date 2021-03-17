using System.Collections.ObjectModel;

namespace DoorManagement.Common.Interfaces
{
    public interface IDoorManagementViewModel
    {
        ObservableCollection<DoorBusinessObject> Doors { get; }
    }
}
