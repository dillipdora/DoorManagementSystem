using DoorManagement.Models;
using System;
using System.Collections.Generic;

namespace DoorManagement.Common
{
    public class DoorBusinessObject : BaseNotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public LockStatus LockStatus { get; set; }

        public OpenStatus OpenStatus { get; set; }

        public bool IsLocked
            => LockStatus == LockStatus.Locked;

        public bool IsOpened
            => OpenStatus == OpenStatus.Opened;

        public List<DoorAction> ActionList { get; private set; }

        public DoorAction SelectedAction { get; set; }

        public DoorBusinessObject(int id, string name, LockStatus lockStatus, OpenStatus openStatus)
        {
            Id = id;
            Name = name;
            LockStatus = lockStatus;
            OpenStatus = openStatus;
            UpdateActionList();
        }

        public void UpdateActionList()
        {
            ActionList = new List<DoorAction>();
            if (LockStatus == LockStatus.Locked && OpenStatus == OpenStatus.Opened)
            {
                ActionList.Add(DoorAction.UnLock);
            }
            else if(LockStatus == LockStatus.Locked && OpenStatus == OpenStatus.Closed)
            {
                ActionList.Add(DoorAction.UnLock);
            }
            else if (LockStatus == LockStatus.UnLocked && OpenStatus == OpenStatus.Opened)
            {
                ActionList.Add(DoorAction.Lock);
                ActionList.Add(DoorAction.Close);
            }
            else if(LockStatus == LockStatus.UnLocked && OpenStatus == OpenStatus.Closed)
            {
                ActionList.Add(DoorAction.Lock);
                ActionList.Add(DoorAction.Open);
            }
            UpdateUI();
        }

        public void UpdateUI()
        {
            NotifyPropertyChanged(nameof(ActionList));
            NotifyPropertyChanged(nameof(IsLocked));
            NotifyPropertyChanged(nameof(IsOpened));
        }
    }
}
