export default {
  namespaced: true,
  state: {
    dialogData: {
      name: "john down",
      desc: "Farmer",
      text: "Good day, son!Get off to me on a rendezvous.Dialogue and conversation are fundamentally different things, even if they look like.",
      answers: [
        {
          id: 0,
          text: "Is my passport necessary"
        },
        {
          id: 1,
          text: "Where can you make money?"
        },
        {
          id: 2,
          text: "Initial quests"
        },
        {
          id: 3,
          text: "see you"
        }
      ]
    }
  },
  mutations: {
    setData(state, data) {
      state.dialogData = data;
    }
  }
}