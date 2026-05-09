export default {
  namespaced: true,
  state: {
    jobs: [
      {
        key: 'porter',
        img: 'porter',
        text: 'Loader',
        desc: 'Do you already have the first level and drivers license of class B?Forward!Draw orders at different points and get money for this.Just dont forget to pump up.Strength and endurance are needed to carry heavy loads.',
        requiredLevel: 1,
      },
      {
        key: 'porter1',
        img: 'porter',
        text: 'Loader1',
        desc: 'You already have the first level and driver’s license and get money for this.Just dont forget to pump up.I need strength and endurance to carry heavy loads.',
        requiredLevel: 1,
      },
      {
        key: 'porter2',
        img: 'porter',
        text: 'Loader2',
        desc: 'Forget the pump up.I need strength and endurance to carry heavy loads.',
        requiredLevel: 1,
      },
      {
        key: 'porter3',
        img: 'porter',
        text: 'Loader3',
        desc: 'You already have the first level.Direct orders at different points and get money for this.',
        requiredLevel: 1,
      },      
      {
        requiredLevel: 1,
        //busdriver
        img: 'busdriver',
        text: 'mm_info_w_t_1',
        desc: 'mm_info_w_d_1',
        point: 1,
        key: false
      },
      {
        requiredLevel: 1,
        //electric
        img: 'electric',
        text: 'mm_info_w_t_2',
        desc: 'This work is available from level 2.You just need the rights of the category [b] and the form of an electrician.Sit down in the car and go to power plants to fix them.',
        point: 2,
        key: false
      },
      {
        requiredLevel: 1,
        //loader
        img: 'loader',
        text: 'mm_info_w_t_3',
        desc: 'mm_info_w_d_3',
        point: 3,
        key: false
      },
      {
        requiredLevel: 1,
        //farmer
        img: 'farmer',
        text: 'mm_info_w_t_4',
        desc: 'mm_info_w_d_4',
        point: 4,
        key: false
      },
      {
        requiredLevel: 1,
        // carThief
        img: 'cartheif',
        text: 'mm_info_w_t_5',
        desc: 'mm_info_w_d_5',
        point: 5,
        key: false
      },
      {
        requiredLevel: 1,
        //hunter
        img: 'hunter',
        text: 'mm_info_w_t_6',
        desc: 'mm_info_w_d_6',
        point: 6,
        key: false
      },
      {
        requiredLevel: 25,
        //pilot
        img: 'pilot',
        text: 'mm_info_w_t_7',
        desc: 'mm_info_w_d_7',
        point: 7,
        key: false
      },
      {
        requiredLevel: 1,
        //trucker
        img: 'truckdriver',
        text: 'mm_info_w_t_8',
        desc: 'mm_info_w_d_8',
        point: 8,
        key: false
      },
      {
        requiredLevel: 1,
        //taxiDriver
        img: 'taxi',
        text: 'mm_info_w_t_9',
        desc: 'mm_info_w_d_9',
        point: 9,
        key: true
      },
      {
        requiredLevel: 1,
        //loader
        img: 'loader',
        text: 'mm_info_w_t_10',
        desc: 'mm_info_w_d_10',
        point: 10,
        key: true
      },
      {
        requiredLevel: 1,
        //sawmill
        img: 'loader',
        text: 'mm_info_w_t_11',
        desc: 'mm_info_w_d_11',
        point: 11,
        key: true
      },
      {
        requiredLevel: 1,
        //mainer
        img: 'loader',
        text: 'mm_info_w_t_12',
        desc: 'mm_info_w_d_12',
        point: 12,
        key: true
      }
    ]
  },
  mutations:{
  }
}
