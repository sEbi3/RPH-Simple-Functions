using Rage;
using LSPD_First_Response.Mod.API;

namespace RPH_Functions.GTA5_Services
{
    public class TowTruck
    {
        public static Ped _Mechanic;
        public static Model _MechanicModel = "s_m_y_construct_02";
        public static Vehicle _Flatbed;
        public static Model _FlatbedModel = "flatbed";
        public static Blip _VehicleBlip;
        public static float SpawnRadius = 100f;
        public static Vector3 _SpawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(SpawnRadius, SpawnRadius));
        public static Vector3 _FlatbedModifier = new Vector3(-0.5f, -5.75f, 1.005f);
        public static bool Notified = false;

        public static void TowTruckSetup()
        {
                    //Getting the nearest vehicle with the GetNearestValidVehicle(); function.
                    Vehicle NearestVehicle = DistanceFunctions.GetNearestVehicle.GetNearestValidVehicle();

                    if (NearestVehicle.Exists())
                    {
                        Game.DisplayHelp("The ~g~Tow Service~w~ is now on route to your ~y~current location~w~ to tow ~b~" + NearestVehicle.Model.Name + "~w~.", 8000);
                        Functions.PlayPlayerRadioAction(LSPD_First_Response.Mod.Menus.EPoliceRadioAction.Handheld, 4000);
                        
                        _Flatbed = new Vehicle(_FlatbedModel, _SpawnPoint, 0f);
                        _Flatbed.IsPersistent = true;
                        _Flatbed.IsInvincible = true;

                        _VehicleBlip = new Blip(_Flatbed);
                        _VehicleBlip.Color = Color.DarkBlue;
                        _VehicleBlip.Name = "Tow Service";

                        _Mechanic = new Ped(_MechanicModel, _SpawnPoint, 0f);
                        _Mechanic.BlockPermanentEvents = true;
                        _Mechanic.IsPersistent = true;
                        _Mechanic.CanBeDamaged = false;
                        _Mechanic.IsInvincible = true;
                        _Mechanic.WarpIntoVehicle(_Flatbed, -1);
                        _Mechanic.Tasks.DriveToPosition(_Flatbed, Game.LocalPlayer.Character.Position, 15f, VehicleDrivingFlags.FollowTraffic, 0f);
                        
                        while (_Mechanic && NearestVehicle)
                        {
                            GameFiber.Yield();
                            if (_Mechanic.DistanceTo(NearestVehicle) < 14f && NearestVehicle.IsCar && !Notified)
                            {
                                _Mechanic.KeepTasks = false;
                                _Mechanic.Tasks.CruiseWithVehicle(0, VehicleDrivingFlags.None);
                                _VehicleBlip.Delete();
                                Notified = true;
                                GameFiber.Wait(5000);
                                NearestVehicle.AttachTo(_Flatbed, 20, _FlatbedModifier, Rotator.Zero);
                                Game.DisplayHelp("~g~" + NearestVehicle.Model.Name + "~w~ has been ~b~towed~w~!", 8000);
                                GameFiber.Wait(3000);
                                _Mechanic.Tasks.CruiseWithVehicle(20f, VehicleDrivingFlags.FollowTraffic);
                                GameFiber.Wait(6000);
                                if (_Mechanic) _Mechanic.Dismiss();
                                if (_Flatbed) _Flatbed.Dismiss();
                                if (NearestVehicle) NearestVehicle.IsPersistent = false;
                            }
                        }
                    }
                    else
                    {
                        Game.DisplayHelp("Could not find a valid vehicle for the ~y~Tow Service~w~. Make sure that you are in the ~g~near~w~ of the ~b~vehicle~w~!", 8000);
                    }
        }
    }
}