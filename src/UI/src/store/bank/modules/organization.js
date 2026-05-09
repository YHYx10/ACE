export default {
  namespaced: true,
  state: {
    organizationName: '',
    organizationBalance: 0,
    transfersList: [],
    staffList: [ ],
    fractionList: [ ]
  },
  mutations: {
    setData: function (state, data) 
	{
		if (data) 
		{
			state.organizationName = data.name;
			state.organizationBalance = data.balance;
			state.transfersList = data.transfersList;
			state.staffList = data.staffList;
			state.fractionList = [];
		}
		else 
		{
			state.organizationName = null;
		}
    },
    updateFractionBalance: function (state, value) {
      state.organizationBalance = value;
    },
    updateStaffList: function (state, data) {
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
