
export default {
  namespaced: true,

  state: {
    isReg: false,
    peopleCount: 0,
    peopleCapacity: 50,
    secondsUntilStart: -1,
  },

  mutations: {
    setData: function(state, data){
      state.isReg = data.isReg;
      state.peopleCount = data.peopleCount;
    },
    setDate: function(state, data){
      state.secondsUntilStart = data;
    }
  },
  actions: {
  },
  modules: {
  }
}