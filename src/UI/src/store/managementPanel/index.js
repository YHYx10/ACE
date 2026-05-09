export default {
  namespaced: true,
  state: {
    loading: false,
    error: '',
    loadedAt: '',
    dashboard: {
      onlinePlayers: 0,
      authenticatedAdmins: 0,
      authenticatedSenior: 0,
      openReports: 0,
      unassignedReports: 0,
      totalAdmins: 0,
      totalSeniorStaff: 0,
      managementAccess: 'LOCKED',
      requester: null
    },
    players: [],
    seniorStaff: [],
    server: {
      resource: 'Whistler',
      status: 'UNKNOWN',
      onlinePlayers: 0,
      openReports: 0,
      unassignedReports: 0,
      dangerousControls: 'DISABLED',
      notes: []
    },
    punishmentHistory: [],
    punishmentHistoryTarget: null,
    punishmentHistoryMessage: '',
    punishmentHistoryError: '',
    databaseSearchResults: [],
    databaseSearchQuery: '',
    databaseSearchMessage: '',
    databaseProfile: null,
    databaseError: ''
  },
  mutations: {
    setLoading(state, value) {
      state.loading = value;
      if (value) state.error = '';
    },
    setData(state, data) {
      state.loading = false;
      state.error = '';
      state.loadedAt = data.loadedAt || '';
      state.dashboard = Object.assign({}, state.dashboard, data.dashboard || {});
      state.players = Array.isArray(data.players) ? data.players : [];
      state.seniorStaff = Array.isArray(data.seniorStaff) ? data.seniorStaff : [];
      state.server = Object.assign({}, state.server, data.server || {});
    },
    setError(state, message) {
      state.loading = false;
      state.error = message || 'Failed to load management data.';
    },
    setPunishmentHistory(state, data) {
      state.punishmentHistoryError = '';
      state.punishmentHistoryTarget = {
        uuid: data && data.targetUuid ? data.targetUuid : 0,
        name: data && data.targetName ? data.targetName : ''
      };
      state.punishmentHistory = data && Array.isArray(data.history) ? data.history : [];
      state.punishmentHistoryMessage = data && data.message ? data.message : '';
    },
    setPunishmentHistoryError(state, message) {
      state.punishmentHistoryError = message || 'Failed to load punishment history.';
      state.punishmentHistory = [];
    },
    setDatabaseSearchResults(state, data) {
      state.databaseError = '';
      state.databaseSearchQuery = data && data.query ? data.query : '';
      state.databaseSearchResults = data && Array.isArray(data.results) ? data.results : [];
      state.databaseSearchMessage = data && data.message ? data.message : '';
    },
    setDatabaseProfile(state, data) {
      state.databaseError = '';
      state.databaseProfile = data || null;
    },
    setDatabaseError(state, message) {
      state.databaseError = message || 'Failed to load character database data.';
    }
  }
};
