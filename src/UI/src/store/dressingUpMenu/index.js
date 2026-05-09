export default {
    namespaced: true,
  
    state: {
      show: true,
      dresses: [
        {id: 0, label: 'FBI'},
        {id: 1, label: 'Firefighters'},
        {id: 2, label: 'Police'},
        {id: 3, label: 'EMS'},
        {id: 4, label: 'Ministry of Health'},
        {id: 5, label: 'Special forces'}
      ]
    },
  
    mutations: {
      setDresses: function (state, value) {
        state.dresses = value
      },
      
    }
  }
