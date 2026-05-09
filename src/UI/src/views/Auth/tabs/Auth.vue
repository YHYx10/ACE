<template>
  <TabTemplate
    title='login'
    subtitle='Account'
    btn='Login'
    :isRemember=true
    @click-action="login"
    :pass="Boolean(passModel)"
    @click-change="(val) => (passModel = val)"
  >
    <Input
      title='login'
      placeholder='Username'
      v-model="logins"
      type="text"
    />
    <Input
      title='login'
      placeholder='Password'
      v-model="password"
      type="password"
    />
  </TabTemplate>
</template>

<script>
import TabTemplate from "./TabsTemplate.vue";
import Input from "./Input.vue";
import { mapState, mapMutations } from "vuex";

export default {
  components: {
    TabTemplate,
    Input,
  },
  data() {
    return {
      logins: "",
      password: "",
    };
  },
  computed: {
    ...mapState("auth", ["isRememberPass", "auth", "notify"]),
    passModel: {
      get: function () {
        return this.isRememberPass;
      },
      set: function (value) {
        console.log(value);
        this.setIsRememberPass(value);
      },
    },
  },
  methods: {
    ...mapMutations("auth", [
      "setIsRememberPass",
      "setCurrentTab",
      "notifyAdd",
    ]),

    getModels: function () {
      return this.labels.map((item) => item.model);
    },

    clearModels: function () {
      this.labels.forEach((item) => (item.model = ""));
    },

    login: function () {
      if (this.flood > Date.now()) return;
      this.flood = Date.now() + this.clickInterval;
      const login = this.logins;
      const password = this.password;
      if (login.length < 4 || password.length < 4) {
        this.notifyAdd({
          status: 1,
          head: "Invalid",
          msg: "The password should be atleast more than 3 characters",
        });
        //this.$store.commit('auth/notify', {status: '1', head: 'Error', msg: 'auth_15'});
        return;
      }
      window.mp.trigger("auth:save:pass", login, password, this.isRememberPass);
      window.mp.triggerServer("signin", login, password);
      //this.clearModels()
    },
    enter(e) {
      if (e.keyCode == 13) this.login();
    },
  },
  mounted() {
    document.addEventListener("keyup", this.enter);
  },
  beforeDestroy() {
    document.removeEventListener("keyup", this.enter);
  },
};
</script>