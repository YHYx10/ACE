export default {
  namespaced: true,
  state: {
    members: [
      // {
      //   nickname: 'Kek_Kekovich',
      //   id: 2,
      //   online: false,
      //   rank: 0,
      //   up: 10,
      //   down: 5,
      //   rating: 327
      // },
      // {
      //   nickname: 'Kek_Lekovich',
      //   id: 352,
      //   online: false,
      //   rank: 0,
      //   up: 10,
      //   down: 5,
      //   rating: 4285
      // },
    ]
  },

  mutations: {
    setMembers: function (state, value) {
      state.members = value;
      state.members.sort((a, b) => {
        if (a.online && !b.online)
          return -1;
        else if (b.online && !a.online)
          return 1;
        else if (a.rank - b.rank != 0)
          return a.rank - b.rank;
        else if (a.nickname < b.nickname)
          return -1
          else return 1;
      });
    },
    updateMember: function (state, value) {
      const index = state.members.findIndex(item => item.id == value.id)
      if (index > -1)
        state.members[index] = value;
      else {
        state.members.push(value);
      }
      state.members.sort((a, b) => {
        if (a.online && !b.online)
          return -1;
        else if (b.online && !a.online)
          return 1;
        else if (a.rank - b.rank != 0)
          return a.rank - b.rank;
        else if (a.nickname < b.nickname)
          return -1
          else return 1;
      });
    },
    removeMember: function (state, memberId) {
      const index = state.members.findIndex(item => item.id == memberId)
      if (index > -1)
        state.members.splice(index, 1);
    }
  }
}
