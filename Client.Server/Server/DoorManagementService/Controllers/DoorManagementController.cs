using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DoorManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoorManagementController : ControllerBase
    {
        [HttpGet("FetchAllDoors")]
        public ActionResult<List<Door>> FetchAllDoors()
        {
            var result = DoorManagementDbContext.Instance.DoorDbSet.ToList();
            return result;
        }

        [HttpPost("PerformDoorAction")]
        public ActionResult<bool> PerformDoorAction(DoorActionMessage message)
        {
            if (message == null || message.Id <= 0)
                return BadRequest();

            try
            {
                var door = DoorManagementDbContext.Instance.DoorDbSet.FirstOrDefault(d => d.Id == message.Id);
                
                if (door == null)
                    return NotFound();

                if(message.DoorAction == DoorAction.Open)
                {
                    var openStatusId = DoorManagementDbContext.Instance.OpenStatusDbSet.FirstOrDefault(o => o.Status == "Opened").Id;
                    door.OpenStatus = openStatusId;
                }
                else if(message.DoorAction == DoorAction.Close)
                {
                    var closeStatusId = DoorManagementDbContext.Instance.OpenStatusDbSet.FirstOrDefault(o => o.Status == "Closed").Id;
                    door.OpenStatus = closeStatusId;
                }
                else if(message.DoorAction == DoorAction.Lock)
                {
                    var lockStatusId = DoorManagementDbContext.Instance.LockStatusDbSet.FirstOrDefault(o => o.Status == "Locked").Id;
                    door.LockStatus = lockStatusId;
                }
                else if(message.DoorAction == DoorAction.UnLock)
                {
                    var unlockStatusId = DoorManagementDbContext.Instance.LockStatusDbSet.FirstOrDefault(o => o.Status == "UnLocked").Id;
                    door.LockStatus = unlockStatusId;
                }

                DoorManagementDbContext.Instance.SaveChanges();
                return true;
            }
            catch (SystemException e)
            {
                //log
                return false;
            }
        }
    }
}