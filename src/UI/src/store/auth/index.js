export default {
  namespaced: true,
  state: {
    currentTab: 'LoginTab',
    socialClub: 'no account',
    soundOnStart: true,
    isRememberPass: false,
    auth: {
      login: "",
      password: ""
    },
    notify: {
      status: -1,
      head: "",
      msg: ""
    }
  },

  mutations: {
    setCurrentTab: function(state, { page }) {
      state.currentTab = page
    },
    updateSoundState(state, toggle){
      state.soundOnStart = toggle;
    },
    setSocialClub: function(state, { name }) {
      state.socialClub = name
    },
    
    setIsRememberPass: function(state, payload) {
      state.isRememberPass = payload
    },

    setPass(state, {login, password, save}){
      state.auth.login = login;
      state.auth.password = password;
      state.isRememberPass = save;
    },

    notifyAdd: function(state, { status, head, msg }) {
      state.notify.status = status;
      state.notify.head = head;
      state.notify.msg = msg;

      setTimeout(() => {
        state.notify.status = -1;
        state.notify.head = "";
        state.notify.msg = "";
      }, 3000)

    }
  }
}
