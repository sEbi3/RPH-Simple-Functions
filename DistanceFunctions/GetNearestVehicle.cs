using Rage;
using LSPD_First_Response.Mod.API;
using System.Linq;

namespace RPH_Functions.DistanceFunctions
{
    public class GetNearestVehicle
    {
        public static Vehicle GetNearestValidVehicle(float Radius = 100f)
        {
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                  // Getting the nearest vehicle to tow. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (Game.LocalPlayer.Character.GetNearbyVehicles(1).Length == 0) { return null; }
            
            Vehicle NearestVehicle = Game.LocalPlayer.Character.GetNearbyVehicles(1)[0];

            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                // Checking if the vehicle is a police car. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (NearestVehicle.IsPoliceVehicle)
            {
                return null;
            }
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // Checking if the vehicle is in a certain range/radius. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (Vector3.Distance(Game.LocalPlayer.Character.Position, NearestVehicle.Position) > Radius)
            {
                return null;
            }
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                 // Checking if the vehicle is a vehicle? \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (!NearestVehicle.IsCar && !NearestVehicle.IsBicycle)
            {
                return null;
            }
            
            return NearestVehicle;
        }
    }
}