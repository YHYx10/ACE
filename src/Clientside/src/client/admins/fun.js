import { Me, Player } from '../../Entity/Player';
import { Client } from '../../Bridge/Client';

mp.game.streaming.requestAnimDict('mini@prostitutes@sexlow_veh');
mp.game.streaming.requestAnimDict('mini@prostitutes@sexnorm_veh');
mp.game.streaming.requestAnimDict('mini@prostitutes@sexlow_veh_first_person');
mp.game.streaming.requestAnimDict('misscarsteal2pimpsex');
mp.game.streaming.requestAnimDict('rcmpaparazzo_2');

/**
 * @type {Player | null}
 */
let targetPlayer = null;

Client.on('rape:target', (pos, id) => {
    targetPlayer = Player.fromId(id);

    targetPlayer.taskPlayAnimAdvanced('mini@prostitutes@sexlow_veh_first_person',
        'low_car_bj_to_prop_p1_player',
        pos.x,
        pos.y,
        pos.z,
        0,
        0,
        0,
        8,
        1,
        -1,
        39,
        0,
        2,
        1,
    );// 5642

    Me.taskPlayAnimAdvanced('mini@prostitutes@sexlow_veh_first_person',
        'low_car_bj_to_prop_p1_female',
        pos.x,
        pos.y,
        pos.z,
        0,
        0,
        0,
        8,
        1,
        -1,
        5641,
        0,
        2,
        1,
    );// 5642
});
Client.on('rape:king', (pos, id) => {
    targetPlayer = Player.fromId(id);

    targetPlayer.taskPlayAnimAdvanced('mini@prostitutes@sexlow_veh_first_person',
        'low_car_bj_to_prop_p1_female',
        pos.x,
        pos.y,
        pos.z,
        0,
        0,
        0,
        8,
        1,
        -1,
        39,
        0,
        2,
        1,
    );// 5642

    Me.taskPlayAnimAdvanced('mini@prostitutes@sexlow_veh_first_person',
        'low_car_bj_to_prop_p1_player',
        pos.x,
        pos.y,
        pos.z,
        0,
        0,
        0,
        8,
        1,
        -1,
        5641,
        0,
        2,
        1,
    );// 5642
});

Client.on('rape:off', () => {
    Me.clearTasksImmediately();

    if (targetPlayer) {
        targetPlayer.clearTasksImmediately();
    }
});
