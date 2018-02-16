using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GTA;
using GTA.Native;

public class Riot : Script {

    private bool active = false;

    public Riot() {
        this.Tick += onTick;
        this.KeyDown += onKeyDown;
        this.KeyUp += onKeyUp;
    }

    private void onTick(object sender, EventArgs args) {
        if (active) {
            Ped player = Game.Player.Character;
            Ped[] peds = World.GetAllPeds();//World.GetNearbyPeds(player.Position, 500.0f);
            Ped[] ppeds = peds.Where(ped => ped.IsAlive && !ped.IsPlayer).ToArray();
            /*if (ppeds.Length < 30) {  Populate world with random peds
                for (int i = 0; i < 10; ++i) {
                    Ped ped = World.CreateRandomPed(player.Position.Around(10));
                }
            }*/
            foreach (Ped ped in peds) {
                if (!ped.IsPlayer && ped.IsAlive && !ped.IsInCombatAgainst(player)) {
                    if (!ped.Weapons.HasWeapon(WeaponHash.CarbineRifle)) {
                        ped.Weapons.Give(WeaponHash.CarbineRifle, 300, true, true);
                    }
                    ped.Task.ClearAllImmediately();
                    ped.Task.FightAgainst(player);
                }
            }
        }
    }

    private void onKeyDown(object sender, KeyEventArgs args) {

    }

    private void onKeyUp(object sender, KeyEventArgs args) {
        if (args.KeyCode == Keys.F4) {
            active = !active;
            if (active)
                UI.Notify("~r~Riot enabled");
            else
                UI.Notify("Riot disabled");
        }
    }


}