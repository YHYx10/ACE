export default {
  namespaced: true,

  state: {
    currentSchoolName: 'AutoSchool_26',
    currentSchoolWelcome: 'AutoSchool_32',
    currentSection: {
      section: 'GeneralSection',
      data: null,
    },
    practicePriceList: {
      0: 300,
      1: 700,
      2: 2700,
      3: 5000,
      4: 8000,
      5: 8000,
    },
    questions: [
      {
        key: 1,
        quest: 'school_1',
        answers: [
          { key: 0, title: 'school_2' },
          { key: 1, title: 'school_3' },
          { key: 2, title: 'school_4' },
        ],
        image: '',
        correctAnswer: 1
      },
      {
        key: 2,
        quest: 'school_5',
        answers: [
          { key: 0, title: 'school_6' },
          { key: 1, title: 'school_7' },
          { key: 2, title: 'school_8' },
        ],
        image: 'question-img.png',
        correctAnswer: 3
      },
    ],
    currentExamType: -1,
    currentAnswer: null,
    currentQuestionKey: null,
    currentQuestionIndex: 0,

    answersOnQuestions: {
      totalQuest: 10,
      correctQuest: 5,
      result: false,
      questions: [
        {
          quest: 'school_9',
          myAnswer: 'school_10',
          correctAnswer: 'school_11'
        },
        {
          quest: 'school_12',
          myAnswer: 'school_13',
          correctAnswer: 'school_13'
        }
      ]
    },
    practicResults: {
      time: '04:27',
      quality: 5,
      result: false
    },

    theoryList: {},

    theoryPrice: 100,
    practicPrice: 150,

    licVehicle: {
      bike: {
        lkey: 0,
        status: false,
        price: 300
      },
      auto: {
        lkey: 1,
        status: true,
        price: 700
      },
      truck: {
        lkey: 2,
        status: false,
        price: 2700
      },
      boat: {
        lkey: 3,
        status: true,
        price: 5000
      },
      helicopter: {
        lkey: 4,
        status: false,
        price: 8000
      },
      airplane: {
        lkey: 5,
        status: true,
        price: 8000
      }
    },
    licWork: [
      {
        lkey: 9,
        name: 'TAXI',
        key: 'LicenseName.Taxi',
        status: false,
        price: 5000
      },
      // {
      //   lkey: 11,
      //   name: 'РУДОКОП',
      //   key: 'LicenseName.MiningOre',
      //   status: true,
      //   price: 1000
      // },
      // {
      //   lkey: 12,
      //   name: 'ОХОТНИК',
      //   key: 'LicenseName.Hunting',
      //   status: true,
      //   price: 1000
      // },
      {
        lkey: 13,
        name: 'Truckman',
        key: 'LicenseName.Trucker',
        status: false,
        price: 8000
      },
      {
        lkey: 14,
        name: 'Rybolov',
        key: 'LicenseName.Fishing',
        status: false,
        price: 1500
      },
    ],
    licDop: [
      {
        lkey: 6,
        name: 'WEAPON',
        key: 'LicenseName.Weapon',
        status: false,
        price: 15000
      }
    ],
    licensesList: {
      [0]: {
        type: "vehicle",
        name: "A",
        img: 'bike',
        status: false,
      },
      [1]: {
        type: "vehicle",
        name: 'B',
        img: 'car',
        status: false,
      },
      [2]: {
        type: "vehicle",
        name: 'C',
        img: 'truck',
        status: false,
      },
      [3]: {
        type: "vehicle",
        name: 'D',
        img: 'ship',
        status: false,
      },
      [4]: {
          type: "vehicle",
          name: 'E',
          img: 'helicopter',
      status: false,
      },
      [5]: {
          type: "vehicle",
          name: 'F',
          img: 'flight',
      status: false,
      },
      [6]: {
          type: 'weapon',
          name: 'gun',
      status: false,
      },
      [7]: {  
          type: 'medical',
          name: 'med',
      status: false,
      },
      [8]: {
          type: 'military',
          name: 'cl:lic:mil',
      status: false,
      },
      [9]: {
          type: 'job',
          name: 'cl:lic:taxi',
          img: 'taxi',
      status: false,
      },
      [10]: {
          type: 'job',
          name: 'cl:lic:mgmw',
          img: 'weapon',
      status: false,
      },
      [11]: {
          type: 'job',
          name: 'cl:lic:miner',
          img: 'iron',
      status: false,
      },
      [12]: {
          type: 'job',
          name: 'cl:lic:hunter',
          img: 'hunting',
      status: true,
      },
      [13]: {
          type: 'job',
          name: 'cl:lic:truckdriver',
          img: 'truck',
      status: false,
      },
      [14]: {
          type: 'job',
          name: 'cl:lic:fish',
          img: 'fishing',
      status: true,
      },
      [15]: {
          type: 'job',
          name: 'cl:lic:metalPlant',
          img: 'iron',
      status: false,
      },
    }
  },

  mutations: {
    setCurrentSection: function (state, value) {
      state.currentSection.section = value.section
      state.currentSection.data = value.data
    },
    setCurrentExamType: function (state, value) {
      state.currentExamType = value
    },
    setAnswer: function (state, { answer, question }) {
      state.currentAnswer = answer;
      state.currentQuestionKey = question;
    },
    switchNextQuestion: function (store) {
      store.currentQuestionKey = null
      store.currentAnswer = null
      store.currentQuestionIndex++
    },
    switchDropQuestion: function (store) {
      store.currentQuestionKey = null
      store.currentAnswer = null
      store.currentQuestionIndex = 0
    },
    setQuestions: function (state, value) {
      state.questions = value;
    },
    setAnswersOnQuestions: function (state, value) {
      console.log('Update the value setAnswersOnQuestions', value)
      state.answersOnQuestions = value;
      if(value && value.result)  {
        state.theoryList[state.currentExamType] = value.result
      }
    },
    setPracticResults: function (state, value) {
      state.practicResults = value;
    },
    setCurrentSchoolName: function (state, value) {
      state.currentSchoolName = value;
    },
    setCurrentSchoolWelcome: function (state, value) {
      state.currentSchoolWelcome = value;
    },
    setLicensesData: function (state, value) {
      state.licensesList = value;
    }
  }
}