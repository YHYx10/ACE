export default {
  namespaced: true,
  state: {
    showWarForEnterprice: true,
    currentTime: 0,
    currentEnterprice: 'Grain composition',
    keyCode: 48,
    captureList: [
    ]
  },
  mutations: {
    SetCurrentTime(state) {
      // state.captureList.forEach(element => {
      //   if (element.timePassed < element.time) {
      //     element.timePassed ++
      //   } 
      // });
      state.captureList.forEach(capt => {
        if (capt.timePassed < capt.time)
          capt.timePassed++;
      });
    },
    setWarForEnterprice(state, data) {
        state.currentTime = data.time;
        state.currentEnterprice = data.currentEnterprice
    },
    setCaptureList(state, data) {
      state.captureList = data;
    },
    addCapture(state, data) {
      const index = state.captureList.findIndex(item => item.id == data.id);
      if (index >= 0) {
        const item = state.captureList[index]
        item.time = data.time
        item.timePassed = data.timePassed
        item.key = data.key
        item.orgId = data.orgId
        item.orgName = data.orgName
      }
      else
        state.captureList.push(data);
    },
    deleteCapture(state, id) {
      const index = state.captureList.findIndex(item => item.id == id);
      if (index >= 0)
        state.captureList.splice(index, 1);
    }
  },
  actions: {
  },
  modules: {
  }
}
