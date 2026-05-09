export default {
  namespaced: true,

  state: {
    currentEventId: 0,
    events: [
      {
        eventId: 0,
        name: "St cascading races ",
        locationName: "Park on the outskirts",
        players: 200,
        maxPlayers: 400,
        startTime: '19:00',//время старта
        description: "asdfasda",
        bestTime: '07:32',//лучшее время за день в сек
        registered: true, 
        countStartTime: '20 mins',
        isStarted: false
      },
      {
        eventId: 1,
        name: "St cascading races ",
        locationName: "Park on the outskirts",
        players: 200,
        maxPlayers: 400,
        startTime: '19:00',//время старта
        description: "Likewise, the beginning of work on the formation of a position is an interesting experiment for verifying significant financial and administrative conditions. Everyday practice shows that the implementation of the planned planned tasks require determining and clarifying the personnel training system, corresponds to pressing needs. The attitude of the tasks.",
        registered: false, 
        countStartTime: '20 min',
        isStarted: false
      },
      {
        eventId: 2,
        name: "St cascading races ",
        locationName: "Park on the outskirts ",
        players: 200,
        maxPlayers: 400,
        startTime: '19:00',//время старта
        description: "In the same way, the beginning of an everyday position is an interesting experiment to verify financial and administrative conditions.Everyday practice shows that the implementation of planned planned tasks require determining and clarifying the personnel training system corresponds to pressing needs.attitude of tasks.",
        bestTime: '07:32',//лучшее время за день в сек
        registered: false, 
        countStartTime: '20 min',
        isStarted: false
      },
    ],
    notify: false,
  },

  mutations: {
    setEvents(state, payload) {
      state.events = payload
    },

    setCurrentEventId(state, id) {
      state.currentEventId = id
    },
    
    setCurrentEventsState(state, payload) {
      console.log('payload' + JSON.stringify(payload))
      payload.forEach(e => {
        const element = state.events.find(s => s.eventId === e.eventId)
        if (element) {
          element.players = e.players
          element.countStartTime = e.countStartTime
          element.bestTime = e.bestTime
          element.startTime = e.startTime
        }
      })
    },
    
    setPlayersCount(state, payload){
      const element = state.events.find(s => s.eventId === payload.id)
      element.players = payload.players
    },
    
    setRegistered(state, { id, registered }){
      console.log('registered' + JSON.stringify(id))
      const element = state.events.find(s => s.eventId === id)
      if (element) {
        element.registered = registered 
      }
    }
  }
}