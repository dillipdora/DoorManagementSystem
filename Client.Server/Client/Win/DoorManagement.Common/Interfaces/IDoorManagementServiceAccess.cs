using DoorManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoorManagement.Common.Interfaces
{
    public interface IDoorManagementServiceAccess
    {
        Task<List<Door>> FetchDoorsAsync();

        Task<bool> DoUnLockAsync(int id);

        Task<bool> DoLockAsync(int id);

        Task<bool> DoOpenAsync(int id);

        Task<bool> DoCloseAsync(int id);
    }
}
