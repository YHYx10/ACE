export default {
  namespaced: true,
  state: {
    // вкл/выкл звук
    isVolume: true,
    // вкл/выкл демонстрации
    isStream: false,
    // Текущий работник
    workerId: 12345,
    // Цена за 1 символ объявления
    pricePerSymbol: 3,
    // Доп. цена за премиум объявление (с картинкой)
    pricePrePremium: 3000,
    name: 'Ivan_Ivan',
    // Объект для конкретного объявления, не трогать
    currentAd: {
    },
    currentPage: 'MainPage',
    // Объект для управления модальным окном
    modal: {
      show: false,
      currentModal: null,
      data: null,
    },
    adsList: [
      {
        Sender: 'Name Surname',
        SenderUUID: 124453,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'In a TTO, the strengthening and development of the structure allows us to evaluate the meaning of new sentences.The significance of these problems is so obvious that the further development of various forms of activity requires us in this way the strengthening and development of the structure allows us to evaluate the significance of new proposals.The significance of these problems is so obvious that the further development of various forms of activity requires us in this way ',
        /**
         * @param Status
         * @values 
         * 'Created' - объявление на проверке
         * 'Compleate' - объявление утверждено
         * 'Canceled' - объявление отклонено
         */
        Status: 'Created',
        Editor: '',
        PrimeAdvert: false,
      },
      {
        Sender: 'Name surname ',
        SenderUUID: 124453341,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Thus, the strengthening and development of the structure allows us to evaluate.The significance of these problems is really important, since the text is too long and you need to write something else.',
        Status: 'Compleate',
        Editor: 'Editor Name',
        PrimeAdvert: false,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 1244,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.This significance is important, since the text is too important, since the text is too long and you need to write something long and you need to write something else.',
        Status: 'Compleate',
        Editor: 'Editor Name',
        Picture: 'https://img1.goodfon.ru/wallpaper/nbig/1/d7/bmw-m5-f10-black-1922.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 2974301943,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.The significance is really important, since the text is too long and you need to write something this is important, since the text is too long and you need to write something else.',
        Status: 'Created',
        Editor: 'Editor Name',
        PrimeAdvert: true,
      },
      {
        Sender: 'Test Testovich',
        SenderUUID: 408244932,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'It allows you to evaluate the meaning of new ones is really important, since the text is too long and you need to write something this is important, since the text is too long and you need to write something else.',
        Status: 'Created',
        Editor: '',
        Picture: 'https://img1.goodfon.ru/wallpaper/nbig/1/d7/bmw-m5-f10-black-1922.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name surname ',
        SenderUUID: 1531,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'It allows you to evaluate the meaning of new ones is really important, since the text is too long and you need to write something sentences.This significance is important, since the text is really important, since the text is too long and you need to write something and you need to write something else.',
        Status: 'Canceled',
        Editor: 'Editor Name',
        PrimeAdvert: false,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 23,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new ones is really important, since the text is too long and important, since the text is too long and you need to write something else. ',
        Status: 'Compleate',
        Editor: 'Editor Name',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 1,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.This significance is important, since the text is too long and you need to write something else.',
        Status: 'Created',
        Editor: '',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name surname ',
        SenderUUID: 98765,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.This significance is important, really important, since the text is too long and you need to write something text too long and you need to write something else.',
        Status: 'Created',
        Editor: '',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 456789,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate really important, since the text is too long and you need to write something new sentences.This significance is important, since the text is too long and you need to write something else.',
        Status: 'Canceled',
        Editor: 'Editor Name',
        PrimeAdvert: false,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 987654321,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.This significance is important, since the text is too long and you need to write something else.',
        Status: 'Created',
        Editor: '',
        PrimeAdvert: false,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 496987,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'Allows you to evaluate the meaning of new sentences.The significance of these is really important, since the text is too long and you need to write something, because the text is too long and you need to write something else.',
        Status: 'Created',
        Editor: '',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name Surname',
        SenderUUID: 29847923,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'It allows you to really important, since the text is too long and you need to write something the meaning of new sentences.This significance is important, since the text is too long and you need to write something else.',
        Status: 'Compleate',
        Editor: 'Editor Name',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
      {
        Sender: 'Name surname',
        SenderUUID: 84529663,
        DateCreate: 1699999999,
        Simnum: 1818186654,
        Title: 'It allows you to evaluate really important, since the text is too long and you need to write something important, since the text is too long and you need to write something else.',
        Status: 'Canceled',
        Editor: 'Editor Name',
        Picture: 'https://img2.goodfon.ru/wallpaper/nbig/c/37/bmw-m5-e39-blue.jpg',
        PrimeAdvert: true,
      },
    ],

    workersList: [
      {
        workerId: 1,
        isOnline: true,
        workerName: 'Sandera_Pomina',
        workerRank: 1,
        editedPerDay: 100,
        editedPerMonth: 600,
        rating: 100
      },
      {
        workerId: 12345,
        isOnline: false,
        workerName: 'Sandrexa_Domina',
        workerRank: 2,
        editedPerDay: 5,
        editedPerMonth: 234,
        rating: 6400
      },
      {
        workerId: 3,
        isOnline: true,
        workerName: 'Sandera_Lomina',
        workerRank: 3,
        editedPerDay: 1,
        editedPerMonth: 645,
        rating: 9000
      },
      {
        workerId: 4,
        isOnline: true,
        workerName: 'Sandera_Fomina',
        workerRank: 4,
        editedPerDay: 93,
        editedPerMonth: 43524,
        rating: 99
      },
      {
        workerId: 5,
        isOnline: true,
        workerName: 'Sandera_Zomina',
        workerRank: 5,
        editedPerDay: 67,
        editedPerMonth: 4321,
        rating: 74327
      },
      {
        workerId: 6,
        isOnline: true,
        workerName: 'Sandera_Womina',
        workerRank: 6,
        editedPerDay: 45,
        editedPerMonth: 323,
        rating: 423
      },
      {
        workerId: 7,
        isOnline: false,
        workerName: 'Sandera_Momina',
        workerRank: 7,
        editedPerDay: 75857,
        editedPerMonth: 2732348,
        rating: 2943892734
      },
      {
        workerId: 8,
        isOnline: true,
        workerName: 'Sandera_Xomina',
        workerRank: 8,
        editedPerDay: 1,
        editedPerMonth: 423,
        rating: 42658762
      },
      {
        workerId: 9,
        isOnline: true,
        workerName: 'Sandera_Somina',
        workerRank: 9,
        editedPerDay: 100,
        editedPerMonth: 54222,
        rating: 53
      },
    ],

    ranksList: [
      {
        rankId: 1,
        rankName: 'Trainee',
        income: 980,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: false,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: false,
          },
          {
            id: 2,
            desc: 'Air output',
            value: false,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 2,
        rankName: 'Corrector',
        income: 1500,
        accessList: [
          {
            id: 0,
            desc: 'Editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: false,
          },
          {
            id: 2,
            desc: 'Air output',
            value: false,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 3,
        rankName: 'Radio host',
        income: 1500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'The editors of the premium announcement',
            value: false,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editorial',
            value: false,
          },
        ]
      },
      {
        rankId: 4,
        rankName: 'Employee Media Department',
        income: 1500,
        accessList: [
          {
            id: 0,
            desc: 'Editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: false,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 5,
        rankName: 'Correspondent',
        income: 2500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editor of premium ads',
            value: false,
          },
          {
            id: 2,
            desc: 'Direct Air',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 6,
        rankName: 'Reporter',
        income: 2500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Edition of the premium ads',
            value: false,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 7,
        rankName: 'Editor',
        income: 2500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editors',
            value: false,
          },
        ]
      },
      {
        rankId: 8,
        rankName: 'Journalist',
        income: 4500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Direct Air',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 9,
        rankName: 'Operator',
        income: 4500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editorial ',
            value: false,
          },
        ]
      },
      {
        rankId: 10,
        rankName: 'Senior editor',
        income: 4500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: false,
          },
        ]
      },
      {
        rankId: 11,
        rankName: 'Employee of the personnel department',
        income: 4500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc:'Hide the editorial ',
            value: true,
          },
        ]
      },
      {
        rankId: 12,
        rankName: 'Deputy Head of the etheric department',
        income: 6500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'A live broadcast ',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editorial ',
            value: true,
          },
        ]
      },
      {
        rankId: 13,
        rankName: 'Executive producer ',
        income: 6500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editorial',
            value: true,
          },
        ]
      },
      {
        rankId: 14,
        rankName: 'Deputy Head of the Editorial Department',
        income: 6500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Direct Air',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: true,
          },
        ]
      },
      {
        rankId: 15,
        rankName: 'Deputy Head of the personnel department',
        income: 6500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editorial ',
            value: true,
          },
        ]
      },
      {
        rankId: 16,
        rankName: 'Head of the etheric department',
        income: 8500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Direct Air',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editors',
            value: true,
          },
        ]
      },
      {
        rankId: 17,
        rankName: 'General producer',
        income: 8500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Edition of the premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Exit in the air ',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editors',
            value: true,
          },
        ]
      },
      {
        rankId: 18,
        rankName: 'The head of the editorial office',
        income: 8500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Edition of the premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editors',
            value: true,
          },
        ]
      },
      {
        rankId: 19,
        rankName: 'The head of the personnel department',
        income: 8500,
        accessList: [
          {
            id: 0,
            desc: 'The editors of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editors of premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Exit in the air ',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: true,
          },
        ]
      },
      {
        rankId: 20,
        rankName: 'Deputy General Director',
        income: 9500,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Edition of the premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Direct Air',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: true,
          },
        ]
      },
      {
        rankId: 21,
        rankName: 'General manager',
        income: 15000,
        accessList: [
          {
            id: 0,
            desc: 'Edition of ordinary ads',
            value: true,
          },
          {
            id: 1,
            desc: 'Editorial premium ads',
            value: true,
          },
          {
            id: 2,
            desc: 'Air output',
            value: true,
          },
          {
            id: 3,
            desc: 'Hide the editor',
            value: true,
          },
        ]
      },
    ]
  },

  mutations: {
    setCurrentPage: function(state, value) {
      state.currentPage = value
    },
    setCurrentAd: function(state, obj) {
      state.currentAd = obj
    },
    setModal: function(state, {show, currentModal, data}) {
      state.modal.show = show
      state.modal.currentModal = currentModal
      state.modal.data = data
    },
    setAdsList: function(state, data) {
      state.adsList = data
    },
    updateAdvert: function(state, value) {
      const index = state.adsList.findIndex(item => item.Id == value.Id);
      if (index >= 0)
        state.adsList.splice(index, 1, value)
      else
        state.adsList.push(value);
    },
    setName: function(state, value) {
      state.name = value
    }
  }
}
