export default {
  namespaced: true,
  state: {
    businessData: {
      // Name: 'General Electric',
      // Description: 'Fuel Station',
      // id: 122,
      // price: 12000000,
      // Price: 12000000,
      // Owner: 'Gov',
      // overseer: 'No',
      // Purchaseable: true,
      // type: 1,
    },
    userMoney: 50000,
    userBank: 50000
  },

  mutations: {
    setBusinessData: function(state, data) {
      state.businessData = data
    },
    setUserBalance: function(state, data) {
      state.userMoney = data.money
      state.userBank = data.bank
    },
  },
}
