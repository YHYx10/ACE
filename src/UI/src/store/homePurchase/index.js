export default {
  namespaced: true,

  state: {
    homeData: {
      Owner: "",
      Class: "",
      Roommates: 0,
      GarageSpace: 0,
      Price: 0,
      IsSelled: false,
      ID: 0,
      IsLocked: true,
      IsTarget: false,
      CanEnter: true
    },
    userMoney: 0,
    userBank: 0
  },

  mutations: {
    setHomeData: function(state, data) {
      state.homeData = data
    },
    setUserBalance: function(state, data) {
      state.userMoney = data.money,
      state.userBank = data.bank
    },
  }
}
