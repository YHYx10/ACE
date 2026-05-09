import family from './modules/family.js'
import organization from './modules/organization.js'
import business from './modules/business.js'
import credit from './modules/credit.js'
import deposit from './modules/deposit.js'

export default {
  namespaced: true,
  state: {
    account: 0,
    cardNumber: 0,
    houseBalance: 0,
    houseBalanceMax: 0,
    transfersPerDay: 0,
    dailyTransferLimit: 0,
    transactionsList: [
      // {
      //   value: 11000,
      //   comment: 'The Walt',
      //   directFrom: true,
      //   tax: 100,
      //   date: 10002,
      // },
      // {
      //   value: 1200000,
      //   comment: 'The Walt Disney Company',
      //   directFrom: false,
      //   tax: 100,
      //   date: 10001,
      // },
    ],
    finesList: [
      // {
      //   id: 0,
      //   reason: 'bank:menu:1',
      //   cost: 9999,
      //   date: '24.07.2021',
      // },
    ]
  },
  mutations: {
    setData: function (state, data) {
      state.account = data.account;
      state.cardNumber = data.cardNumber;
      state.houseBalance = data.houseBalance;
      state.houseBalanceMax = data.houseBalanceMax;
      state.transactionsList = data.transactionsList;
      state.finesList = data.finesList;
      state.transfersPerDay = data.transfersPerDay;
      state.dailyTransferLimit = data.dailyTransferLimit;
    },
    updateBalanceWithTransfer: function (state, value) {
      state.transfersPerDay = value
    },
    updateHomeBalance: function (state, value) {
      state.houseBalance = value
    },
    addTransaction: function(state, value) {
      state.transactionsList.push(value)
    },
  },
  actions: {
  },
  modules: {
    family,
    organization,
    business,
    credit,
    deposit,
  }
}
