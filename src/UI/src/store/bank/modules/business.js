export default {
  namespaced: true,
  state: {
    bizName: '',
    bizProfit: {
      Expenses: 0,
      Income: 0,
      TotalExpenses: 0,
      TotalIncome: 0,
      RecordSince: '',
      RecordUntil: ''
    },
    bizBalance: 0,
    bizTaxBalance: 0,
    bizTaxMax: 0,
    accessList: {
      topUp: true,
      withdraw: true,
      bonus: false,
      payForBiz: true,
    },
    isCredit: true,
    currentCredit: {
      isTaken: false,
      rate: 0.14,
      approvedAmount: 0,
      date: '21.04.2021 21:45'
    },
    transfersList: [
      // {
      //   name: 'Marvin Testovich',
      //   comment: 'Money_AtmIn',
      //   // withdrawal replenishment bonus tax
      //   value: 0,
      //   directFrom: false,
      //   recipient: 0,
      //   tax: 0,
      //   date: 0
      // },
    ],
    staffList: [
      // {
      //   id: 1,
      //   name: 'Esther Govard',
      //   payment: 'Последняя выплата: 19.06.3030',
      // },
      // {
      //   id: 2,
      //   name: 'Esther Howard',
      //   payment: 'Последняя выплата: 22.06.3030',
      // },
    ]
  },
  mutations: {
    setData: function (state, data) {
      if (data)
      {
        state.bizName = data.bizName;
        state.bizBalance = data.bizBalance;
        state.bizProfit = data.bizProfit || {
          Expenses: -1,
          Income: -1,
          TotalExpenses: -1,
          TotalIncome: -1,
          RecordSince: '',
          RecordUntil: '',
        }
        state.bizTaxBalance = data.bizTaxBalance;
        state.bizTaxMax = data.bizTaxMax;
        state.isCredit = data.isCredit;
        state.transfersList = data.transfersList;
      }
      else
        state.bizName = null;
    },
    updateBizBalance: function (state, value) {
      state.bizBalance = value;
    },
    updateBizTaxBalance: function (state, value) {
      state.bizTaxBalance = value;
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
