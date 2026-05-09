export default {
  namespaced: true,
  state: {
    ranksList: [
      // {
      //   rankId: 0,
      //   rankName: 'бегунок 0',
      //   accessHouse: 0,
      //   accessDoors: 1,
      //   accessFurniture: 2,
      //   accessClothes: true,
      //   accessWar: false,
      //   accessMembers: false,
      //   accessCars: [
      //     {
      //       carName: 'carname 1',
      //       carNumber: 'е999кх',
      //       key: 0,
      //       access: 2,
      //     },
      //     {
      //       carName: 'Lamborgini daiblo',
      //       carNumber: 'е999кх',
      //       key: 1,
      //       access: 2
      //     },
      //     {
      //       carName: 'carname 3',
      //       carNumber: 'е999кх',
      //       key: 2,
      //       access: 2
      //     },
      //     {
      //       carName: 'carname 4',
      //       carNumber: 'е999кх',
      //       key: 3,
      //       access: 2
      //     },
      //     {
      //       carName: 'carname 5',
      //       carNumber: 'е999кх',
      //       key: 4,
      //       access: 2
      //     },
      //   ]
      // },
      // {
      //   rankId: 1,
      //   rankName: 'бегунок 1',
      //   accessHouse: 2,
      //   accessDoors: 1,
      //   accessFurniture: 1,
      //   accessClothes: false,
      //   accessWar: false,
      //   accessMembers: true,
      //   accessCars: [
      //     {
      //       carName: 'carname 1',
      //       carNumber: 'е999кх',
      //       key: 0,
      //       access: 2
      //     },
      //     {
      //       carName: 'carname 2',
      //       carNumber: 'е999кх',
      //       key: 1,
      //       access: 0
      //     },
      //     {
      //       carName: 'carname 3',
      //       carNumber: 'е999кх',
      //       key: 2,
      //       access: 3
      //     },
      //     {
      //       carName: 'carname 4',
      //       carNumber: 'е999кх',
      //       key: 3,
      //       access: 2
      //     },
      //     {
      //       carName: 'Lamborgini aventador',
      //       carNumber: 'е999кх',
      //       key: 4,
      //       access: 2
      //     },
      //   ]
      // },
    ],
    accessHouseList: {
      choose: false,
      list: [
        'fam:menu:access:house1',
        'fam:menu:access:house2',
        'fam:menu:access:house3',
        'fam:menu:access:house4',
      ],
    },
    accessFurnitureList: {
      choose: false,
      list: [
        'fam:menu:access:furn1',
        'fam:menu:access:furn2',
        'fam:menu:access:furn3',
        'fam:menu:access:furn4',
      ],
    },
    accessCarsList: {
      choose: null,
      list: [
        'fam:menu:access:car1',
        'fam:menu:access:car2',
        'fam:menu:access:car3',
        'fam:menu:access:car4',
      ],
    },
    organizationName: {
      value: '',
      error: false
    },
    chatOptions: {
      currentColor: '#FF41E0',
      currentIcon: 1,
    }
  },

  mutations: {
    setChooseAccessHouse: function(state) {
      state.accessHouseList.choose = !state.accessHouseList.choose 
    },
    setChooseAccessFurniture: function(state) {
      state.accessFurnitureList.choose = !state.accessFurnitureList.choose 
    },
    setChooseAccessCars: function(state, index) {
      if (state.accessCarsList.choose === null) {
        state.accessCarsList.choose = index
      } else {
        state.accessCarsList.choose = null
      }
    },

    setOrganizationName: function(state, name) {
      state.organizationName.value = name      
    },
    setErrorName: function(state, value) {
      state.organizationName.error = value      
    },
    setChatOptions: function (state, value) {
      state.chatOptions = value
    },
    setRanksList: function (state, data) {
      state.ranksList = data;
    },
    deleteRank: function (state, data) {
      state.ranksList = data;
    },
    updateRank: function (state, data) {
      const index = state.ranksList.findIndex(item => item.rankId == data.rankId)
      if (index > -1) {
        state.ranksList.splice(index, 1, data)
      }
      else
        state.ranksList.push(data);
    },
    removeRank: function (state, id) {
      const index = state.ranksList.findIndex(item => item.rankId == id)
      if (index > -1)
        state.ranksList.splice(index, 1);
    },
    updateCar: function (state, value) {
      state.ranksList.forEach(rank => {
        let index = rank.accessCars.findIndex(item => item.key == value.key)
        if (index > -1)
          rank.accessCars[index] = value;
        else
          rank.accessCars.push(value);        
      });
    },
    removeCar: function (state, id) {
      state.ranksList.forEach(rank => {
        let index = rank.accessCars.findIndex(item => item.key == id)
        if (index > -1)
          rank.accessCars.splice(index, 1);     
      });
    }
  }
}