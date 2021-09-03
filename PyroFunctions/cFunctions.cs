//These are some functions that I use my plugins. Feel free to use them or learn from them. I'd ask you give some credits but I can't force you.


        internal static Ped SetWanted(Ped wPed, bool isWanted) //Used to set a ped as wanted.
        {
            Persona thePersona = Functions.GetPersonaForPed(wPed);
            thePersona.Wanted = true;
            return wPed;
        }

        internal static bool IsWanted(Ped oPed) //Debugging: Used to check if the ped is wanted.
        {
            Persona persona = Functions.GetPersonaForPed(oPed);
            Game.LogTrivial("Ped is Wanted? = " + persona.Wanted);
            return persona.Wanted;
        }

        internal static void SetDrunk(Ped Bad, bool isDrunk) //Sets a ped as drunk in STP and gives them a drunk animation.
        {
            GameFiber.StartNew(delegate
            {
                GameFiber.Yield();
                Bad.Metadata.stpAlcoholDetected = isDrunk;
                var drunkAnimset = new AnimationSet("move_m@drunk@verydrunk");
                drunkAnimset.LoadAndWait();
                Bad.MovementAnimationSet = drunkAnimset;
                Rage.Native.NativeFunction.Natives.SET_PED_IS_DRUNK(Bad, isDrunk);
            });
            return;
        }

        internal static void Damage(Vehicle vehicle, float radius, float amount) //You can appy damage to vehicle using this.
        {
            var model = vehicle.Model;
            model.GetDimensions(out var vector31, out var vector32);
            var num = new Random().Next(10, 45);
            for (var index = 0; index < num; ++index)
            {
                var randomInt1 = MathHelper.GetRandomSingle(vector31.X, vector32.X);
                var randomInt2 = MathHelper.GetRandomSingle(vector31.Y, vector32.Y);
                var randomInt3 = MathHelper.GetRandomSingle(vector31.Z, vector32.Z);
                vehicle.Deform(new Vector3(randomInt1, randomInt2, randomInt3), radius, amount);
            }
        }

        internal static void FireControl(Vector3 position, int children, bool isGasFire) //Spawns fite at the position.
        {
            if (children > 25) return;
            NativeFunction.Natives.StartScriptFire(position.X, position.Y, position.Z, children, isGasFire);
        }