export default {
  namespaced: true,
  state: {
    currentPage: 'FurniturePage',
    houseId: 0,
    houseType: 0, // 0 - personal, 1 - family
    houseCost: 0,
    houseLocked: false,
    paidBefore: '11-11-2020',
    furnitureList: [
      // {
      //   id: 0,
      //   name: 'какой-то диван',
      //   placed: true,
      //   price: 100,
      // },
      // {
      //   id: 1,
      //   name: 'какой-то стул',
      //   placed: false,
      //   price: 200,
      // },
    ],
    interiorList: [
      // {
      //   name: 'интерьер',
      //   bought: false,
      //   key: 1,
      // },
      // {
      //   name: 'интерьер',
      //   bought: false,
      //   key: 2,
      // },
    ],
    currentInteriorId: 1,
    totalVehicles: 2,
    occupiers: [
      // Жильцы
    ],
    rentCost: 125,
    currentGarage: {
      type: '',
      placesCount: 0,
    },
    typeOfGarages: [
      // {
      //   type: 1,
      //   desc: '1 место',
      //   placesCount: 1,
      //   cost: 0,
      // },
      // {
      //   type: 2,
      //   desc: '3 места',
      //   placesCount: 3,
      //   cost: 55000,
      // }
    ],
  },
  mutations: {
    setCurrentPage: function(state, page) {
      state.currentPage = page
    },
    setHouseLocked: function(state) {
      state.houseLocked = !state.houseLocked
    },
    uninstallFurniture: function(state, index) {
      state.furnitureList.find((e) => e.id === index).placed = false
      window.mp.trigger('homeMenu:uninstallFurniture', state.houseId, index)
    },
    uninstallAllFurniture: function(state) {
      state.furnitureList.forEach((element) => (element.placed = false))
      window.mp.trigger('homeMenu:uninstallAllFurniture', state.houseId)
    },
    setSafe: function(state, { item, uuid }) {
      const index = state.occupiers.findIndex((item) => item.uuid == uuid)
      state.occupiers[index].safeAccess = item
      window.mp.triggerServer(
        'house:changeAccess',
        state.houseId,
        0,
        item,
        uuid
      )
    },
    setGarage: function(state, { item, uuid }) {
      const index = state.occupiers.findIndex((item) => item.uuid == uuid)
      state.occupiers[index].garageAccess = item
      window.mp.triggerServer(
        'house:changeAccess',
        state.houseId,
        1,
        item,
        uuid
      )
    },
    deleteOccupier: function(state, uuid) {
      const index = state.occupiers.findIndex((item) => item.uuid == uuid)
      state.occupiers.splice(index, 1)
      window.mp.triggerServer('house:occupierDeleted', state.houseId, uuid)
    },
    deleteAllOccupiers: function(state) {
      window.mp.triggerServer('house:allOccupiersDeleted', state.houseId)
      state.occupiers = []
    },
    setRentCost: function(state, rentCost) {
      state.rentCost = rentCost
    },
    addOccupiers: function(state, payload) {
      state.occupiers.push(payload)
    },
    setFullState: function(state, payload) {
      state.rentCost = payload.rentCost
      state.houseId = payload.houseId
      state.houseType = payload.houseType
      state.houseCost = payload.houseCost
      state.houseLocked = payload.houseLocked
      state.paidBefore = payload.paidBefore
      state.furnitureList = payload.furnitureList
      state.interiorList = payload.interiorList
      state.currentInteriorId = payload.currentInteriorId
      state.totalVehicles = payload.totalVehicles
      state.occupiers = payload.occupiers
      state.typeOfGarages = payload.typeOfGarages
      state.currentGarage = payload.currentGarage
    },
  },
}