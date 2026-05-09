export default {
  namespaced: true,
  state: {
    index: -1,
    slots: [
      {
        name: "Elon_Musk2",
        gender: 1, // 1 - м, 0 - ж
        level: 7,
        frac: "SpaceX",
        cash: 100398,
        bank: 500,
        ban: null,
        // ban: {
        //   admin: "Admin name",
        //   time: "2011-01-26 13:51:50Z",
        //   banTime: "2011-01-21 13:51:50Z",
        //   reason:
        //     "Клонирование Илона Маска, придумай другой никнейм, биографию, проходи заново регистрацию.",
        // },
        spawnPoints: [
          {
            key: "s1",
            name: "213",
            subname: "123123",
            x: 1000,
            y: 1000,
            spawnType: 0,
            active: 0,
          },
          {
            key: "s2",
            name: "123",
            subname: "123123",
            x: 1300,
            y: 1000,
            spawnType: 1,
            active: 0,
          },
          {
            key: "s3",
            name: "333",
            subname: "123123",
            x: 1000,
            y: 1300,
            spawnType: 2,
            active: 0,
          },
        ],
      },
      {
        name: "Elena_Musk",
        gender: 0,
        level: 37,
        frac: "SpaceXX",
        cash: 10000398,
        bank: 50000000,
        ban: null,
        spawnPoints: [
          {
            key: "s1",
            name: "213",
            subname: "123123",
            x: 1000,
            y: 1000,
            spawnType: 0,
            active: 0,
          },
          {
            key: "s2",
            name: "123",
            subname: "123123",
            x: 1300,
            y: 1000,
            spawnType: 1,
            active: 0,
          },
          {
            key: "s3",
            name: "333",
            subname: "123123",
            x: 1000,
            y: 1300,
            spawnType: 2,
            active: 0,
          },
        ],
      },
      null,
    ],
  },
  mutations: {
    setData(state, data) {
      state.slots = data;
    },
    setIndex(state, index) {
      state.index = index;
    },
    setSpawns(state, data) {
      state.slots[state.index].spawnPoints = data;
    },
  },
  actions: {},
  modules: {},
};
