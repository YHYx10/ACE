import vote from './vote'
import letter from './letter'
import complaint from './complaint'
import recording from './recording'
import notary from './notary'
import jobs from './jobs'
import nameEdit from './nameEdit'
import licenses from './licenses'
import certificate from './certificate'
import donations from './donations'
import events from './events'
import news from './news'
import community from './community'

export default {
  namespaced: true,
  state: {
    isRegistered: true,
    name: 'Alexey Pupkin',
    day: 10,
    donationConsent: true,
    tax: 0,
    socialStatus: [
      {
        desc: 'Status',
        values: [
          'Unemployed',
          'In marriage withSandexa_Domina'
        ]
      },
      {
        desc: 'Harvesting',
        values: [
          '25.02.2021 - 25.02.2021, murder',
          '25.02.2021 - 25.02.2021, theft',
          '25.02.2020 - 25.02.2020, murder',
          '25.02.2019 - 25.02.2019, murder',
        ]
      },
      {
        desc: 'Information about employment',
        values: [
          '25.02.2021 - 25.02.2021, hunter',
          '25.02.2021 - 25.02.2021, Truckman',
        ]
      },
      {
        desc: 'Licenses',
        values: [
          'Category A, 25.02.2021 - 25.02.2021',
          'Category B, 25.02.2021 - 25.02.2021',
          'Category C, 25.02.2021 - 25.02.2021',
          'Category D, 25.02.2021 - 25.02.2021',
        ]
      },
    ],
    month: {
      number: 9,
      desc: 'September'
    },
    year: 1993,
    cardId: 12345678,
    modalData: {
      show: false,
      title: 'Necessary title',
      desc: 'Some kind of description'
    }
  },
  mutations: {
    setName: function(state, value){
      state.name = value;
    },
    setUuid: function(state, value){
      state.cardId = value;
    },
    setSocialStatus: function(state, data){
      state.socialStatus = data;
    },
    closeModal: function(state){
      state.modalData.show = false
      state.modalData.title = null
      state.modalData.desc = null
    },
    openModal: function(state, data){
      state.modalData.title = data.title
      state.modalData.desc = data.desc
      state.modalData.show = true
    }
  },
  actions: {
  },
  modules: {
    vote,
    letter,
    complaint,
    recording,
    notary,
    jobs,
    nameEdit,
    licenses,
    certificate,
    donations,
    events,
    news,
    community
  }
}
  