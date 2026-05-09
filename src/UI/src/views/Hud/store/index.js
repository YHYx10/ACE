import Vue from "vue";
import Vuex from "vuex";
import reportMenu from "./modules/reportMenu";

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    reportMenu,
  },
});
