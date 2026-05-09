import configs from './config'
export default {
  namespaced: true,

  state: {
    configs,
    playerMoney: 0,
    playerBank: 100,
    price: 2
  },

  mutations: {
    setPrice(state, price){
      state.price = price;
    },
    setMoney(state, playerdata){
      state.playerMoney = playerdata.money;
      state.playerBank = playerdata.bank;
    },
  }
}
