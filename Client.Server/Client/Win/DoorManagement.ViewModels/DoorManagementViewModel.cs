using DoorManagement.Common;
using DoorManagement.Common.Interfaces;
using DoorManagement.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoorManagement.ViewModels
{
    [Export(typeof(IDoorManagementViewModel))]
    public class DoorManagementViewModel : BaseNotifyPropertyChanged, IDoorManagementViewModel
    {
        public ObservableCollection<DoorBusinessObject> Doors { get; private set; }

        [Import(typeof(IDoorManagementServiceAccess))]
        public IDoorManagementServiceAccess DoorManagementServiceAccess { get; set; }

        public ICommand ExecuteDoorCommand { get; private set; }

        [ImportingConstructor]
        public DoorManagementViewModel()
        {
            Doors = new ObservableCollection<DoorBusinessObject>();
            //Doors.Add(new DoorBusinessObject(1, "door1", LockStatus.Locked, OpenStatus.Closed));
            //Doors.Add(new DoorBusinessObject(2, "door2", LockStatus.Locked, OpenStatus.Opened));
            //Doors.Add(new DoorBusinessObject(3, "door3", LockStatus.UnLocked, OpenStatus.Closed));
            //Doors.Add(new DoorBusinessObject(3, "door4", LockStatus.UnLocked, OpenStatus.Opened));

            CompositionHelper.Container.SatisfyImportsOnce(this);
            ExecuteDoorCommand = new RelayCommand(ExecuteDoorAction);

            FetchDoorsAsync();
        }

        private async void ExecuteDoorAction(object obj)
        {
            if (obj == null || !(obj is DoorBusinessObject))
                return;

            var doorBusinessObject = obj as DoorBusinessObject;
            bool result = false;
            switch (doorBusinessObject.SelectedAction)
            {
                case DoorAction.Open:
                    result = await Task.Run(() => DoorManagementServiceAccess.DoOpenAsync(doorBusinessObject.Id));
                    if (result == true)
                        doorBusinessObject.OpenStatus = OpenStatus.Opened;

                    break;
                case DoorAction.Close:
                    result = await Task.Run(() => DoorManagementServiceAccess.DoCloseAsync(doorBusinessObject.Id));
                    if (result == true)
                        doorBusinessObject.OpenStatus = OpenStatus.Closed;

                    break;
                case DoorAction.Lock:
                    result = await Task.Run(() => DoorManagementServiceAccess.DoLockAsync(doorBusinessObject.Id));
                    if (result == true)
                        doorBusinessObject.LockStatus = LockStatus.Locked;

                    break;
                case DoorAction.UnLock:
                    result = await Task.Run(() => DoorManagementServiceAccess.DoUnLockAsync(doorBusinessObject.Id));
                    if (result == true)
                        doorBusinessObject.LockStatus = LockStatus.UnLocked;

                    break;
                default:
                    break;
            }

            if(result == true)
            {
                doorBusinessObject.UpdateActionList();
                doorBusinessObject.UpdateUI();
            }
            else
            {
                //log and show errors
            }
        }

        private async void FetchDoorsAsync()
        {
            var doors = await Task.Run(() => DoorManagementServiceAccess.FetchDoorsAsync());
            var doorsBOList = doors.Select(d => new DoorBusinessObject(d.Id, 
                            d.Name, 
                            d.LockStatus == 1 ? LockStatus.Locked : LockStatus.UnLocked, 
                            d.OpenStatus == 1 ? OpenStatus.Opened : OpenStatus.Closed))
                            .ToList();

            Doors.Clear();
            foreach (var door in doorsBOList)
                Doors.Add(door);
        }

        public void RefreshDoors()
        {
            Doors.Clear();
            FetchDoorsAsync();
        }
    }
}
