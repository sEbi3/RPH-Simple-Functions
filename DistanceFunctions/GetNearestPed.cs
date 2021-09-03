using Rage;
using LSPD_First_Response.Mod.API;
using System.Linq;

namespace RPH_Functions.DistanceFunctions
{
    public class GetNearestPed
    {
        public static Ped GetNearestValidPed(float Radius = 100f)
        {
            if (Game.LocalPlayer.Character.GetNearbyPeds(1).Length == 0) { return null; }
            
            Ped NearestPed = Game.LocalPlayer.Character.GetNearbyPeds(1)[0];
            
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                      // Checking if the Ped is a cop. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (NearestPed.RelationshipGroup == "COP")
            {
                if (NearestPed.RelationshipGroup == "COP")
                {
                    return null;
                }
            }

            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            // Checking if the Ped is in a certain range/radius. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (Vector3.Distance(Game.LocalPlayer.Character.Position, NearestPed.Position) > Radius)
            {
                Game.DisplayHelp("The Ped is not close enough to you.");
                return null;
            }
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
               // Checking if the Ped is in a active pursuit. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (Functions.GetActivePursuit() != null)
            {
                if (Functions.GetPursuitPeds(Functions.GetActivePursuit()).Contains(NearestPed))
                {
                    return null;
                }
            }
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                     // Checking if the Ped is a human. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (!NearestPed.IsHuman)
            {
                Game.DisplayHelp("The Ped is not a human.");
                return null;
            }
            
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                   // Checking if the Ped is in a vehicle. \\
            //////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            if (!NearestPed.IsInAnyVehicle(true))
            {
                Game.DisplayHelp("The Ped is in a vehicle.");
                return null;
            }
 
            return NearestPed;
        }
    }
}