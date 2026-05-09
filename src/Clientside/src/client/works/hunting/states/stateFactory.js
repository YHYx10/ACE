const StandState = require('./standState.js');
const WalkState = require('./walkState.js');
const DeadState = require('./deadState.js');
const SpawnState = require('./spawnState.js');

function getState(state, animal) {
    switch (state) {
        case -1:
            return new SpawnState(animal);
        case 0:
            return new StandState(animal);
        case 1:
            return new WalkState(animal);
        case 2:
            return new DeadState(animal);
    }
};

module.exports.getState = getState;