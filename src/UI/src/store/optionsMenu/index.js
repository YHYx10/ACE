import keyTags from './keyTags'
import auth2f from './2fa'

export default {
  namespaced: true,

  state: {
    currentTab: null,
    userLogin: null,
    keyTags,
    bp: 0,
    primeDays: 100,
    donatePoints: 0,
    canUsePromo: true,
    statistics: {
      level: 0,
      username: '',
      exp: 0,
      phoneNumber: 0,
      passportNumber: 0,
      licenses: '', // 'A / B / C / LV',
      bankCount: 0,
      organization: null,
      rank: 'no',
      work: '',
      maritalStatus: {
        sex: 1,
        secondHalf: '',
      },
      warns: 0,
      bans: 0,
      registrationDate: 1607024975000,
      family: null,
      // house: 'Эконом плюс',
    },
    donate: null,
    shop: {
      premiumPrice: 1000,
      money: [
        // { id: 0, name: '$ 150.000', price: 100, imgid: 1 },
        // { id: 1, name: '$ 250.000', price: 100, imgid: 1 },
        // { id: 2, name: '$ 350.000', price: 100, imgid: 1 },
        // { id: 3, name: '$ 450.000', price: 100, imgid: 1 },
        // { id: 4, name: '$ 550.000', price: 100, imgid: 1 },
        // { id: 5, name: '$ 650.000', price: 100, imgid: 1 },
      ],
      exclusive: {
        count: 1,
        maxcount: 2,
        price: 1000,
        pictures: [
          {
            id: 1,
            shirt: '/img/Shop/shirt-modal.png',
            sneacker: '/img/Shop/sneackers-modal.png',
            pants: '/img/Shop/pants-modal.png',
            title: 'first',
          },
          {
            id: 2,
            shirt: '/img/Shop/shirt-modal-2.png',
            sneacker: '/img/Shop/sneackers-modal-2.png',
            pants: '/img/Shop/pants-modal-2.png',
            title: 'second',
          },
          {
            id: 3,
            shirt: '/img/Shop/shirt-modal-3.png',
            sneacker: '/img/Shop/sneackers-modal-3.png',
            pants: '/img/Shop/pants-modal-3.png',
            title: 'third',
          },
        ],
      },
    },
    property: {
      house: null,
      transport: [
        // { id: 0, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 1, name: 'Sentinel', numbers: 'V25JIN', price: 2000000 },
        // { id: 2, name: 'Marvin', numbers: 'C15FIN', price: 5000000 },
        // { id: 3, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 4, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 5, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 6, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 7, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 8, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
        // { id: 9, name: 'Sentinel', numbers: 'V25JIN', price: 5000000 },
      ],
      // transport: null,
      business: null,
      // business: {
      //   number: 112,
      //   type: 1,
      //   name: 'MyBiz',
      //   tax: 10,
      //   taxCount: 240000,
      //   price: 5000000,
      //   products: [
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 300000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 200000,
      //       curCount: 150000,
      //       price: 12000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //     {
      //       title: 'product',
      //       maxCount: 300000,
      //       curCount: 150000,
      //       price: 120000,
      //     },
      //   ],
      // },
      family: null,
    },
    balance: 99000,
    settings: {
      noMicro: true,
      hint: false,
      showNames: false,
      showHud: false,
      voiceValue: 0,
      showMiniMap: false,
      muteLowLevel: false,
      muteLowLevelValue: 3,
      showDrift: false,
      showFamilyMembers: false,
      trafficOff: true,
      censore: true,
    },
    buttons: [
      { text: 'optmenu:setts:btn:1', key: 27, name: 'openMap', lock: false },
      { text: 'optmenu:setts:btn:2', key: 66, name: 'microphone', lock: false },
      { text: 'optmenu:setts:btn:3', key: 9, name: 'inventory', lock: false },
      { text: 'optmenu:setts:btn:4', key: 84, name: 'chat', lock: false },
      { text: 'optmenu:setts:btn:5', key: 74, name: 'belt', lock: false },
      { text: 'optmenu:setts:btn:6', key: 76, name: 'lock', lock: false },
      { text: 'optmenu:setts:btn:7', key: 50, name: 'engine', lock: false },
      { text: 'optmenu:setts:btn:8', key: 53, name: 'reduce', lock: false },
    ],
    reportRaiting: false,
    reports: [],
    // fraction: null,
    fraction: {
      id: 1,
      bank: 0,
      canInvite: false,
      canKick: false,
      isLeader: false,
      canWhithdraw: false,
      canAccess: false,
      lastHour: 0,
      lastDay: 0,
      members: [],
      access: {},
      businesses: [
        // { ID: 0, TypeName: 'Заправка', Mafia: 'Yakudza' }
      ],
    },
    dialog: null,
    captAttack: false,
    // всего денег заработано
    moneyEarned: 0,
    myContracts: [
      //{
      //ContractName: "1",
      //CountCompleted: 0,
      //CurrentLevel: 19,
      //InProgress: true,
      //ExpirationDate: new Date(Date.now() + 1000*300)
      //},
    ],
    familyContracts: [
      // {
      //   ContractName: '0',
      //   CountCompleted: 0,
      //   CurrentLevel: 19,
      //   InProgress: true,
      //   ExpirationDate: new Date(Date.now() + 1000 * 300),
      //   PointsVisited: [],
      // },
    ],
    myAchievements: {
      // '0': {
      //   CurrentLevel: 19,
      //   GivenReward: false,
      //   DateCompleted: 213213213,
      // },
      // '20': {
      //   CurrentLevel: 19,
      //   GivenReward: false,
      //   DateCompleted: 213213213,
      // },
    },
    referals: {
      total: 0,
      code: 0,
    },
  },
  getters: {
    fracMembers(state) {
      return state.fraction.members
    },
  },
  mutations: {
    setCanUsePromo(state, value) {
      state.canUsePromo = value
    },
    updateReferalsData(state, { key, value }) {
      state.referals[key] = value
    },
    updateBonusPoints(state, bp) {
      state.bp = bp
    },
    updateDonate(state, donatePoints) {
      state.donatePoints = donatePoints
    },
    updatePrimeData(state, primedays) {
      state.primeDays = primedays
    },
    updateActionKey(state, { key, val }) {
      const el = state.buttons.find((b) => b.name == key)
      if (!el) return
      el.key = val
      //state.buttons = {...state.buttons};
    },
    setFractionMembers(state, members) {
      state.fraction.members = members
      state.fraction = { ...state.fraction }
    },
    setFractionBusinesses(state, biz) {
      state.fraction.businesses = biz
      state.fraction = { ...state.fraction }
    },
    setFractionData(
      state,
      {
        id,
        bank,
        lastHour,
        lastDay,
        canInvite,
        canKick,
        canRank,
        canWhithdraw,
        canAccess,
      }
    ) {
      state.fraction.id = id
      state.fraction.bank = bank
      state.fraction.canInvite = canInvite
      state.fraction.canKick = canKick
      state.fraction.canRank = canRank
      state.fraction.canWhithdraw = canWhithdraw
      state.fraction.canAccess = canAccess
      state.fraction.lastHour = lastHour
      state.fraction.lastDay = lastDay
      state.fraction = { ...state.fraction }
      if (id == 0) state.currentTab = null
    },
    setFractionAccess(state, access) {
      state.fraction.access = access
      state.fraction = { ...state.fraction }
    },
    resetFraction(state) {
      state.fraction.bank = 0
      state.fraction.canInvite = false
      state.fraction.canKick = false
      state.fraction.isLeader = false
      state.fraction = { ...state.fraction }
    },
    setAttack(state, attack) {
      state.captAttack = attack
    },
    setCurrentTab: function(state, payload) {
      state.currentTab = payload
    },
    selectLang(state, tag) {
      state.settings.langs.cur = tag
    },
    setSetting(state, { name, status }) {
      state.settings[name] = status
      window.mp.trigger('mmenu:setting:set', name, status)
    },
    setSettings(state, data) {
      state.settings = data
    },
    addAnswer(state, { id, name, text }) {
      state.reports.push({ type: 1, id, name, text, time: Date.now() })
    },
    closeReport(state) {
      state.reportRaiting = true
      state.reports = []
      window.mp.triggerServer('report:player:close')
    },
    sendReport(state, msg) {
      state.reports.push({
        type: 0,
        id: 0,
        name: 'you',
        text: msg,
        time: Date.now(),
      })
      window.mp.triggerServer('report:player:send', msg)
    },
    sendRaiting(state, rait) {
      state.reportRaiting = false
      state.reports = []
      window.mp.triggerServer('report:player:raiting', rait)
    },
    setStats(state, data) {
      state.statistics = data
    },
    setLogin(state, data) {
      state.userLogin = data
    },
    //userLogin
    setDonateMoney(state, data) {
      // data.forEach(el => {
      //   if(el.Type == 1){
      //     state.shop.premium.price = el.Price;
      //   }else if(el.Type == 2){
      //     state.shop.money.push({ id: el.Id, name: el.Name, price: el.Price })
      //   }
      // });
      state.shop.money = data
    },
    setDonatePremium(state, data) {
      // data.forEach(el => {
      //   if(el.Type == 1){
      //     state.shop.premium.price = el.Price;
      //   }else if(el.Type == 2){
      //     state.shop.money.push({ id: el.Id, name: el.Name, price: el.Price })
      //   }
      // });
      state.shop.premiumPrice = data
    },
    setExclusivePrice(state, data) {
      state.shop.exclusive.price = data
    },
    setExclusiveCount(state, data) {
      state.shop.exclusive.count = data
    },
    setExclusiveMaxCount(state, data) {
      state.shop.exclusive.maxcount = data
    },
    setProps(state, data) {
      if (data.business) data.business.products = []
      state.property = data
    },
    updateBizTaxCount(state, data) {
      if (state.property.business) state.property.business.taxCount = data
    },
    setProducts(state, data) {
      if (state.property.business) state.property.business.products = data
      state.property = { ...state.property }
    },
    setBalance(state, balance) {
      state.balance = balance
    },
    setDialog(state, dialog) {
      state.dialog = dialog
    },
    updateAchievements(state, data) {
      state.myAchievements[data.key] = data.value
    },
    setMyContracts(state, data) {
      state.myContracts = data
      state.myContracts.forEach((item) => {
        item.ExpirationDate = new Date(item.ExpirationDateMS)
      })
    },
    setFamilyContracts(state, data) {
      state.familyContracts = data
      state.familyContracts.forEach((item) => {
        item.ExpirationDate = new Date(item.ExpirationDateMS)
      })
    },
    updateMyContracts(state, data) {
      data.ExpirationDate = new Date(data.ExpirationDateMS)
      const index = state.myContracts.findIndex(
        (c) => c.ContractName == data.ContractName
      )
      if (index >= 0) state.myContracts.splice(index, 1, data)
      else state.myContracts.push(data)
    },
    updateFamilyContracts(state, data) {
      data.ExpirationDate = new Date(data.ExpirationDateMS)
      const index = state.familyContracts.findIndex(
        (c) => c.ContractName == data.ContractName
      )
      if (index >= 0) state.familyContracts.splice(index, 1, data)
      else state.familyContracts.push(data)
    },
  },
  modules: {
    auth2f,
  },
}
