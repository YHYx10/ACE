import WeaponModel from './WeaponModel'

export default {
    namespaced: true,
    state: {
        weapons: [],
        playerMoney: 0,
        playerBank: 100,
        loadingWeapon: true,
    },
    mutations:{
        setWeapons(state, weapons){
            state.weapons = [];
            weapons.forEach(weapon => {
                state.weapons.push(new WeaponModel(weapon))
            });
            state.loadingWeapon = false;
        },
        setMoney(state, playerdata){
            state.playerMoney = playerdata.money;
            state.playerBank = playerdata.bank;
        },
        loadWeapon(state, {id, slot, compIndex, model}){
            //state.loadingWeapon = true;
            window.mp.trigger("cef:wshop:update:model", id, slot, compIndex, model);
        },
        weaponLoaded(state){
            state.loadingWeapon = false;
        },
        close(state){
            state.weapons = [];
            state.loadWeapon = true;
        }
    }
}
  