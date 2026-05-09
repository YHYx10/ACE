const StandState = require('./standState.js');
const FollowState = require('./followState.js');
const SitState = require('./sitState.js');
const HuntState = require('./huntState.js');

const states = {
    Sit: 0,
    Stay: 1,
    Follow: 2,
    Hunt: 3
}

function getState(state, pet) {
    switch (state) {
        case 0:
            return new SitState(pet);
        case 1:
            return new StandState(pet);
        case 2:
            return new FollowState(pet);
        case 3:
            return new HuntState(pet);
    }
};

module.exports.getState = getState;
module.exports.states = states;