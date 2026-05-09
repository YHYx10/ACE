export default {
  namespaced: true,
  state: {
    factor: 1,
    profit: 12000,
    owner: 0,
    ownerType: 1,

    enterpricesList: [
      {
        id: 0,
        key: "DavisQuartz",
        name: 'Grain composition',
        img: 'enterprice-1.png',
        captureisInProgress: false,
        orgId: 2,
        date: '10.08.2020 12:00'
      },
      {
        id: 1,
        name: 'Warehouse Velikov',
        key: "DavisQuartz",
        img: 'enterprice-2.png',
        captureisInProgress: true,
        orgId: 4,
        date: '10.08.2020 12:00'
      },
      {
        id: 2,
        name: 'dump',
        key: "DavisQuartz",
        img: 'enterprice-3.png',
        captureisInProgress: false,
        orgId: null,
        date: '10.08.2020 12:00'
      },
      {
        id: 3,
        name: 'Restaurant on the shore',
        key: "DavisQuartz",
        img: 'enterprice-4.png',
        captureisInProgress: false,
        orgId: 20,
        date: null
      },
      {
        id: 4,
        name: 'Warehouse with Aliexpress',
        key: "DavisQuartz",
        img: 'enterprice-5.png',
        captureisInProgress: false,
        orgId: 21,
        date: null
      },
      {
        id: 5,
        name: 'crashed',
        key: "DavisQuartz",
        img: 'enterprice-2.png',
        captureisInProgress: false,
        orgId: 24,
        date: null
      },
      {
        id: 6,
        name: 'Yar',
        key: "DavisQuartz",
        img: 'enterprice-2.png',
        captureisInProgress: false,
        orgId: 25,
        date: null
      },
      {
        id: 7,
        name: 'People',
        key: "DavisQuartz",
        img: 'enterprice-4.png',
        captureisInProgress: false,
        orgId: 8,
        date: '10.08.2020 12:00'
      },
      {
        id: 8,
        name: 'Warehouse of rubber women',
        key: "DavisQuartz",
        img: 'enterprice-1.png',
        captureisInProgress: false,
        orgId: 11,
        date: '10.08.2020 12:00'
      },

    ],

    fractionNames: {
      [1]: 'FAMILY',
      [2]:'BALLAS',
      [3]:'VAGOS',
      [4]:'MARABUNTA',
      [5]:'BLOOD',
      [16]: 'THELOST'

    }
  },
  mutations: {
    setEnterpricesList: function (state, data) {
      state.enterpricesList = data.companies;
      state.profit = data.profit
    },
    updateEnterpricesList: function (state, value) {
      const index = state.enterpricesList.findIndex(item => item.id == value.id)
      if (index >= 0) {
        state.enterpricesList[index] = value;
      }
      else {
        state.enterpricesList.push(value);
      }
    },
    setOwner: function (state, data) {
      state.owner = data.owner;
      state.ownerType = data.ownerType;
    }
  },
  actions: {
  },
  modules: {
  }
}
