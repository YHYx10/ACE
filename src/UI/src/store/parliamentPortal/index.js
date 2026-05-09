export default {
  namespaced: true,
  state: {
    meetings: [
      {
        id: 0,
        // 0 - не завершено, 1 - завершено положительно, 2 - завершено отрицательно
        isCompleted: 1,
        date: '20.02.2021 | 14:06',
        spicker: 'radik_Khojabekyan',
        topic: 'Sandrexa_domina ammoats Vostin',
        desc: 'On the other hand, the established structure of the organization is an interesting experiment to verify the positions occupied by participants in relation to the tasks.On the other hand, the constant quantitative growth and the scope of our activity is an interesting experiment to verify the directions of progressive development.The significance of these problems is so obvious that the beginning of everyday work on the formation of a position provides a wide circle (specialists) participation in the formation of a personnel training system, corresponds to the pressing .',
      },
      {
        id: 1,
        isCompleted: 2,
        date: '27.02.2021 | 15:06',
        spicker: 'padik_Khojabekyan',
        topic: 'Sandrexa Switch Swets Supervisor',
        desc: 'On the other hand, the established structure in relation to the tasks.On the other hand, the quantitative growth and field activity is an interesting experiment of development areas.The significance of these problems is so obvious that the beginning of everyday work on the formation of a position for a wide circle (specialists) participation in the formation of a personnel training system corresponds...',
      },
      {
        id: 2,
        isCompleted: 0,
        date: '25.02.2021 | 12:06',
        spicker: 'huyadik_Khojayan',
        topic: 'Sandrexa Switch Swets Supervisor',
        desc: 'On the other hand, the established structure in relation to the tasks.On the other hand, a quantitative experiment of development directions.The significance of these problems is so obvious that the beginning of everyday work on the formation of a position for a wide circle (specialists) participation in the formation of a personnel training system corresponds...',
      },
    ],
    // Выбранное заседание, выбирается само мутацией setCurrentMeeting
    currentMeeting: null,
    // Информация конкретного голосования
    currentVoteData: {
      // id ответа, за который отдал голос (если проголосовал), если не проголосовал - значение null; 0 - голос против, 1 - голос нейтральный, 2 - голос за
      currentVote: null,
      partiesList: [
        {
          id: 0,
          name: 'Party1',
          members: [
            {
              nickname: 'Name_Surname',
              id: 0,
              vote: 0,
            }
          ]
        }
      ]
    }
  },
  mutations: {
    setCurrentMeeting: function(state, obj) {
      state.currentMeeting = obj
    },
    setMeetings: function(state, data) {
      state.meetings = data
    },
    setOneMeeting: function(state, data) {
      const index = state.meetings.findIndex(item => item.id == data.id)
      if (index >= 0)      
        state.meetings[index] = data
      else
        state.meetings.push(data);
    },
    setCurrentVoteData: function(state, data) {
      state.currentVoteData = data;
    }
  },
  actions: {
  },
  modules: {
  }
}
