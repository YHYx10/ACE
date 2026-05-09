export default {
  namespaced: true,
  state: {
    show: false,
    orgList: [
      // {
      //   id: 0,
      //   name: 'Ballas',
      //   owner: 'Vladimir_Putin',
      //   victories: 4365,
      //   buissCount: 45,
      //   membersCount: 999,
      //   rating: 2644,
      //   rank: 1,
      // },
      // {
      //   id: 1,
      //   name: 'Groove',
      //   owner: 'Vladimir_Putin',
      //   victories: 4365,
      //   buissCount: 45,
      //   membersCount: 999,
      //   rating: 2644,
      //   rank: 1,
      // },
    ]
  },
  getters: {
    getFamilyRank: state => key => {
      const index = state.orgList.findIndex(item => item.id == key)
      if (index > -1)
        return state.orgList[index].rank;
      else
        return 999;
    },
  },
  mutations: {
    toggleRatingList: function(state, boolean) {
      state.RatingList.show = boolean
    },
    setOrgList: function(state, data) {
      state.orgList = data;
      state.orgList.sort((a, b) => a.rank - b.rank);
    },
    updateOrganization: function(state, data) {
      const index = state.orgList.findIndex(item => item.id == data.id)
      if (index > -1)
        state.orgList[index] = data;
      else
        state.orgList.push(data);
      state.orgList.sort((a, b) => a.rank - b.rank);
    }
  }
}
