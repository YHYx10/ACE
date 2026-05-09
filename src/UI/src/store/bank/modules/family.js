export default {
  namespaced: true,
  state: {
    familyName: '',
    familyBalance: 0,
    houseTaxBalance: 0, 
    houseTaxBalanceMax: 0, 
    transfersList: [
      // {
      //   recipient: 'Marvin Testovich',
      //   comment: 'Пополнил баланс',
      //   // withdrawal replenishment bonus work
      //   value: 1200000,
      //   tax: 100,
      //   directFrom: false,
      //   date: 10001
      // },
    ],
    staffList: [
      // {
      //   uuid: 1,
      //   name: 'Esther Govard',
      //   money: 500,
      //   date: '06:08:2021'
      // },
    ]
  },
  mutations: {
    setData: function (state, data) {
      if (data) {
        state.familyName = data.name
        state.familyBalance = data.balance
        state.transfersList = data.transfersList
        state.houseTaxBalance = data.houseTaxBalance
        state.houseTaxBalanceMax = data.houseTaxBalanceMax
        state.staffList = data.staffList;
      }
      else {
        state.familyName = null;
      }

    },
    updateFamilyBalance: function(state, value) {
      state.familyBalance = value;
    },
    updateFamilyHomeBalance: function(state, value) {
      state.houseTaxBalance = value;
    },
    updateStaffList: function(state, data) {
      const index = state.staffList.findIndex(item => item.uuid == data.uuid)
      if (index >= 0)
        state.staffList.splice(index, 1, data);
      else
        state.staffList.push(data)
    },
    addTransaction: function(state, value) {
      state.transfersList.push(value)
    },
  },
  actions: {
  },
  modules: {
  }
}
