export default {
  namespaced: true,

  state: {
    show: false,
    Body: {
      Header: 'Do you really want to buy this item?Do you really want to buy this item?',
      Buttons: [
        { Name: 'Confirm', Icon: 'confirm' },
        { Name: 'Cancel', Icon: 'cancel' },
        // { Name: 'В гараж', Icon: undefined }
      ]
    }
  },

  mutations: {
    setBody: function(state, payload) {
      state.Body = payload
    },

    setShow: function(state, payload) {
      state.show = payload
    }
  }
}
