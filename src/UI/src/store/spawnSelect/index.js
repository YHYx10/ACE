export default {
  namespaced: true,

  state: {
    spawns: ['Family name','The address of the last entry','','Russian mafia']
  },

  mutations: {
    setData(state, data){
      state.spawns = data
    }
  }
}
