export default {
    namespaced: true,
    state: {
      voteDone: true,
      currentVote: 1,
      finishDate: '06.02.2020, 18:00',
      debateDescFeature: 'chall:vote:desc',
      partiesList: [
        {
          id: 0,
          vote: false,
          name: 'Party',
          desc: 'Do you already have the first level and drivers license of class B?Forward!Direct orders at different points receive money for this.Just dont forget to pump up.I need strength and endurance to carry heavy loads.',
          countVote: 17
        },
        {
          id: 1,
          vote: false,
          name: 'Party 1',
          desc: 'Do you already have the first level and drivers license of class B?Direct orders at different points receive money for this.Just dont forget to pump up.Strength and endurance are needed to carry heavy loads.',
          countVote: 7
        },
        {
          id: 2,
          vote: false,
          name: 'Party 2',
          desc: 'Do you already have the first level and drivers license of class B?Forward!Just dont forget to pump up.Strength and endurance are needed to carry heavy loads.',
          countVote: 20
        },
        {
          id: 3,
          vote: false,
          name: 'Party 3',
          desc: 'Do you already have the first level and drivers license of class B?Forward!Direct orders at different points receive money for this.',
          countVote: 13
        },
        {
          id: 4,
          vote: false,
          name: 'Party 4',
          desc: 'Do you already have the first level and drivers license of class B?Forward!Direct orders at different points receive money for this.Just dont forget to pump up.I need strength and endurance to carry heavy loads.',
          countVote: 30
        },
        {
          id: 5,
          vote: false,
          name: 'Party 5',
          desc: 'Forward!Direct orders at different points receive money for this.Just dont forget to pump up.Strength and endurance are needed to carry heavy loads.',
          countVote: 21
        },
        {
          id: 6,
          vote: false,
          name: 'Party 6',
          desc: 'Do you already have the first level and drivers license of class B?Forward!Direct orders at different points receive money for this.Just dont forget to pump up.I need strength and endurance to carry heavy loads.',
          countVote: 19
        },
        {
          id: 7,
          vote: false,
          name: 'Party 7',
          desc: 'Do you have class B rights?Forward!Direct orders at different points receive money for this.Just dont forget to pump up.I need strength and endurance to carry heavy loads.',
          countVote: 8
        },
      ]
    },
    mutations:{
      setState: function(state, data) {
        state.partiesList = data.partiesList;
        state.voteDone = data.voteDone;
        state.currentVote = data.currentVote > 0 ? data.currentVote : null;
        state.finishDate = data.finishDate;
      },
      setCurrentVote:function (state, value) {
        state.currentVote = value;
      }
    }
}
  