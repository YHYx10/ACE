export default {
  namespaced: true,

  state: {
    userName: 'Alexey Volkov',
    buissName: 'Gun Shop',
    productsList: [
      {
        id: 0,
        name: 'Fresh tuna',
        desc: 'Small description of the goods',
        cost: 2000000,
      },
      {
        id: 1,
        name: 'Fresh tuna',
        desc: 'A small description of the product',
        cost: 2525010,
      },
      {
        id: 2,
        name: 'Fresh tuna',
        desc: 'Small description of the product',
        cost: 2342550,
      },
      {
        id: 3,
        name: 'Fresh tuna',
        desc: 'Small description of the product',
        cost: 700,
      },
      {
        id: 4,
        name: 'Fresh tuna',
        desc: 'Small description of the goods',
        cost: 4500000,
      },
      {
        id: 5,
        name: 'Fresh tuna',
        desc: 'A small description of the product',
        cost: 130000,
      },
      {
        id: 6,
        name: 'Fresh tuna',
        desc: 'Small description of the goods',
        cost: 200400,
      },
      {
        id: 7,
        name: 'Fresh tuna',
        desc: 'A small description of the product',
        cost: 2053000,
      },
      {
        id: 8,
        name: 'Fresh tuna',
        desc: 'Small description of the product',
        cost: 2030000,
      },
      {
        id: 9,
        name: 'Fresh tuna',
        desc: 'Small description of the product',
        cost: 20100,
      },
      {
        id: 10,
        name: 'Fresh tuna',
        desc: 'A small description of the product',
        cost: 20000,
      },
      {
        id: 11,
        name: 'Fresh tuna',
        desc: 'A small description of the product',
        cost: 20000000,
      },
      {
        id: 12,
        name: 'Fresh tuna',
        desc: 'Small description of the product',
        cost: 200000,
      },
    ],
    basket: [],
    basketTotal: 0,
    showModal: false,
    currentItem: null,
  },
  mutations: {
    setCurrentItem: function(state, { id, name, desc, cost, count }) {
      state.showModal = true
      state.currentItem = { id, name, desc, cost, count }
    },
    putInBasket: function(state, { id, name, desc, cost }) {
      const basket = state.basket
      let newItemIndex = null
      for (let index = 0; index < basket.length; index++) {
        const item = basket[index]
        if (item.id === id) {
          newItemIndex = index
        }
      }
      if (newItemIndex === null) {
        // state.currentItem.count = Number(count)
        basket.push({ id, name, desc, cost, count: 1 })
      } else {
        const existingItem = basket[newItemIndex]
        existingItem.count += Number(1)
      }
      state.showModal = false
      state.currentItem = null
    },
    clearBasket(state) {
      state.basket.splice(0, state.basket.length)
    },
    updateCount: function(state, [key, value]) {
      state.basket[key].count = value
    },

    increase: function(state, key) {
      state.basket[key].count++
    },
    decrease: function(state, key) {
      state.basket[key].count--
      if (state.basket[key].count < 1) state.basket.splice(key, 1)
    },
    deleteItem: function(state, { key }) {
      state.basket.splice(key, 1)
    },
    pushData(state, data) {
      state.productsList = data.productsList
      state.buissName = data.buissName
      state.userName = data.userName
    },
    setData(state, data, money) {
      state.money = money
      state.productsList.forEach((item) => {
        const index = data.findIndex((i) => i[0] == item.id)
        if (index === -1) item.cost = 0
        else item.cost = data[index][1]
      })
    },
    closeModal(state) {
      state.showModal = !state.showModal
    },
  },
  actions: {},
  modules: {},
}