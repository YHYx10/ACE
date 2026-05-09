import battlePage from './modules/battle-page';
import controlPage from './modules/control-page';
import eventsPage from './modules/events-page';
import membersPage from './modules/members-page';
import propertyPage from './modules/property-page';
import ratingPage from './modules/rating-page';

export default {
  namespaced: true,
  state: {
    navData: {
      show: true
    },
    currentPage: 'InfoPage',
    accountType: 'Premium',
    isLeader: true,
    currentRankId: 1,
    currentMemberId: 2,
    inFamily: true,
    infoPage: {
      nation: '',
      income: 0,
      members: 0,
      bankBalance: 0,
      houseType: '',
      houseAdress: '',
      biography: '',
      onlineMembers: 0,
      familyRank: 0,
      familyId: 0,
      businessControl: 0,
      tabooList: [
        // {
        //   text: 'строго запрещается воровать деньги'
        // },
        // {
        //   text: 'строго запрещается убивать членов семьи и очень длинное табу '
        // },
      ],
      rulesList: [
        // {
        //   text: 'всегда докладывать о происшествиях'
        // },
      ],
    },
    membersOnMap: true,
  },

  mutations: {
    setCurrentMemberId: function(state, value) {
      state.currentMemberId = value
    },
    updateHouseData: function(state, {type, address}) {
      state.infoPage.houseType = type
      state.infoPage.houseAdress = address
    },
    hideBattlePage: function(state) {
      state.battlePageState = false
    },
    setCurrentPage: function(state, value) {
      state.currentPage = value;
    },
    toggleNav: function(state, value) {
      state.navData.show = value;
    },
    setInfoPage: function(state, data) {
      state.infoPage = data;
      state.infoPage.tabooList = state.infoPage.tabooList.map(item => ({text: item}));
      state.infoPage.rulesList = state.infoPage.rulesList.map(item => ({text: item}));
    },
    setRank: function(state, value) {
      state.currentRankId = value;
      state.isLeader = value == 0;
    },
    setNation: function(state, value) {
      state.infoPage.nation = value;
    },
    setBiography: function(state, value) {
      state.infoPage.biography = value;
    },
    setRules: function(state, value) {
      state.infoPage.tabooList = value.taboo.map(item => ({text: item}));
      state.infoPage.rulesList = value.rules.map(item => ({text: item}));
    },
    setMembersOnMap: function(state, value) {
      state.membersOnMap = value;
    },
    setBankBalance: function(state, value) {
      state.infoPage.bankBalance = value;
    },
    setInFamily: function(state, value) {
      state.inFamily = value;
    }
    
  },
  modules: {
    battlePage,
    controlPage,
    eventsPage,
    membersPage,
    propertyPage,
    ratingPage
  }
}
