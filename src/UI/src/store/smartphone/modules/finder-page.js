export default {
  namespaced: true,

  state: {
    profile: null,
    profiles: [],
    isProfileLoaded: false,
    isProfilesLoaded: false,
    error: null
  },

  mutations: {
    setProfile(state, profile) {
      state.profile = profile
      state.isProfileLoaded = true
      state.error = null
    },

    setProfiles(state, profiles) {
      state.profiles = Array.isArray(profiles) ? profiles : []
      state.isProfilesLoaded = true
      state.error = null
    },

    setError(state, error) {
      state.error = error
      state.isProfileLoaded = true
      state.isProfilesLoaded = true
    },

    resetLoadState(state) {
      state.isProfileLoaded = false
      state.isProfilesLoaded = false
      state.error = null
    }
  }
}
