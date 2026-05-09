export default {
  namespaced: true,
  state: {
    propertyList: [
      // {
      //   name: 'Body Art',
      //   id: 28494,
      //   img: 0,
      //   stats: [
      //     {
      //       desc: 'Дата захвата',
      //       value: '10.01.2021',
      //     },
      //     {
      //       desc: 'Последний сбор денег',
      //       value: '1 час назад',
      //     },
      //     {
      //       desc: 'Прибыль за 1 день',
      //       value: 92738,
      //     }
      //   ],
      //   owner: 'Alexey Volcov'
      // },
      // {
      //   name: 'Бургерная',
      //   id: 347523,
      //   img: 1,
      //   stats: [
      //     {
      //       desc: 'Дата захвата',
      //       value: '11.01.2021',
      //     },
      //     {
      //       desc: 'Последний сбор денег',
      //       value: '3 часа назад',
      //     },
      //     {
      //       desc: 'Прибыль за 1 день',
      //       value: 638,
      //     }
      //   ],
      //   owner: 'Alexey Volcov'
      // },
    ]
  },

  mutations: {
    setPropertyList: function (state, value){
      state.propertyList = value;
    },
    updateProperty: function (state, value){
      const index = state.propertyList.findIndex(item => item.id == value.id);
      if (index > -1)
        state.propertyList[index] = value;
      else
        state.propertyList.push(value);
    },
    removeProperty: function (state, bizId){
      const index = state.propertyList.findIndex(item => item.id == bizId);
      if (index > -1)
      state.propertyList.splice(index, 1);
    }
  }
}
