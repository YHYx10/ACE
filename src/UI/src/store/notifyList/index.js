export default {
  namespaced: true,

  state: {
    items: [],
    waitingItems: [],
    counter: 0,
    types: [
      {
        key: 'alert',
        title: 'ALERT',
        color: '#00ffe9',
        icon: 'megaphone.png'
      },
      {
        key: 'error',
        title: 'ERROR',
        color: '#ff1d35',
        icon: 'warning.png'
      },
      {
        key: 'success',
        title: 'SUCCESS',
        color: '#00ff6a',
        icon: 'check.png'
      },
      {
        key: 'info',
        title: 'INFO',
        color: '#fff000',
        icon: 'info.png'
      },
      {
        key: 'warning',
        title: 'WARNING',
        color: '#ff9f1a',
        icon: 'warning.png'
      }
    ]
  },

  mutations: {
    notify: function (state, { type, message, time }) {
      const items = state.items
      const notifyType = state.types[Number(type)] || state.types[3]

      const item = {
        type: notifyType,
        message,
        time: Number(time) || 3000,
        id: state.counter++
      }

      items.push(item)
    },

    addItem: function (state, item) {
      state.items.push(item)
    },

    removeItem: function (state, item) {
      state.items.splice(state.items.indexOf(item), 1)
    },

    removeWaitingItem: function (state, item) {
      state.waitingItems.splice(item, 1)
    }
  }
}
