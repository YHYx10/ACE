export default {
  namespaced: true,
  state: {
    money: 999999,
    productsSell: [
      {
        name: 'Axe',
        cost: 100,
        product: "axe",
        count: 1
      },
      {
        name: 'Bag',
        cost: 50,
        product: "mesh",
        count: 1
      },
      {
        name: 'Dynamite',
        cost: 30,
        product: "dynamite",
        count: 1
      },
      {
        name: 'Knife',
        cost: 250,
        product: "knife",
        count: 1
      },
      {
        name: 'Hatch',
        cost: 10,
        product: "lockpick",
        count: 1
      },
      {
        name: 'Metal detector',
        cost: 500,
        product: "metal-detector",
        count: 1
      },
      {
        name: 'Pick',
        cost: 1000,
        product: "pickaxe",
        count: 1
      },
      {
        name: 'Rematch',
        cost: 173,
        product: "repair-kit",
        count: 1
      },
      {
        name: 'Strainer',
        cost: 645,
        product: "screed",
        count: 1
      },
      {
        name: 'Stethoscope',
        cost: 645,
        product: "stetoskop",
        count: 1
      },
      
    ],
    basket: [],
  },
  mutations: {
    putInBasket: function (state, { name, cost, count, product }) {
      const basket = state.basket
      let newItemIndex = null
      for (let index = 0; index < basket.length; index++) {
        const item = basket[index]
        if (item.product === product) {
          newItemIndex = index
        }
      }
      if (newItemIndex === null) {
        basket.push({ name, cost, count, product })
      } else {
        const existingItem = basket[newItemIndex]
        existingItem.count += Number(count)
      }
    },
    deleteItem: function(state, {key}) {
      state.basket.splice(key, 1)
    },
    changeInQuantityUp: function(state, {key}) {
      state.basket[key].count++
    },
    changeInQuantityDown: function(state, {key}) {
      console.log(key)
      if (state.basket[key].count > 1) {
        state.basket[key].count--
      } else {
        state.basket.splice(key, 1)
      }
    },
    setProductsSell: function(state, value) {
      state.productsSell = value;
    }
  },
  actions: {
  },
  modules: {
  }
}
