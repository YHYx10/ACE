const state = {
    reportsInQueue: [], // Array of reports
  };
  
  const mutations = {
    setReportsInQueue(state, reports) {
      state.reportsInQueue = reports;
    },
    addReport(state, report) {
      state.reportsInQueue.push(report);
    },
    clearReports(state) {
      state.reportsInQueue = [];
    },
  };
  
  const actions = {
    addNewReport({ commit }, report) {
      commit("addReport", report);
    },
  };
  
  const getters = {
    reportsInQueue: (state) => state.reportsInQueue,
  };
  
  export default {
    namespaced: true,
    state,
    mutations,
    actions,
    getters,
  };
  