<template>
  <TabTemplate title='Register' subtitle='Account' btn='Register' @click-action="register">
    <Input
      title='Registration'
      placeholder='Email'
      v-model="email"
      type="email"
    />
    <Input title='Registration' placeholder='Username' v-model="logins" type="text" />
    <Input
      title='Password'
      placeholder='Password'
      v-model="password"
      type="password"
    />
    <Input
      title='Confirm password'
      placeholder='Confirm Password'
      v-model="confirm"
      type="password"
    />
    <Input v-model="promo" title='Enter' placeholder='Promocode' val="" type="text" />
  </TabTemplate>
</template>

<script>
import TabTemplate from "./TabsTemplate.vue";
import Input from "./Input.vue";
import { mapState, mapMutations, mapGetters } from "vuex";

export default {
  components: { TabTemplate, Input },
  data() {
    return {
      email: "",
      logins: "",
      password: "",
      confirm: "",
      promo: "",
    };
  },
  computed: {
    ...mapState("auth", ["notify"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    ...mapMutations("auth", ["notifyAdd"]),
    getModels: function () {
      return this.labels.map((item) => item.model);
    },

    clearModels: function () {
      this.labels.forEach((item) => (item.model = ""));
    },

    register: function () {
      if (this.flood > Date.now()) return;
      this.flood = Date.now() + this.clickInterval;
      const login = this.logins;
      const password = this.password;
      const confirm = this.confirm;
      const email = this.email;

      if (this.checkEmail(email)) {
        this.notifyAdd({
          status: 1,
          head: 'Invalid',
          msg: 'Please enter a valid email address',
        });
        //this.$store.commit('notifyList/notify', {type: 1, position: 0, message: this.loc('auth_17'), time: 3000});
        return;
      }
      if (login.length < 4 || password.length < 4) {
        this.notifyAdd({
          status: 1,
          head: 'Invalid',
          msg: 'The password should be at least 3 characters',
        });
        //this.$store.commit('notifyList/notify', {type: 1, position: 0, message: this.loc('auth_15'), time: 3000});
        return;
      }
      if (password !== confirm) {
        this.notifyAdd({
          status: 1,
          head: 'Invalid',
          msg: 'Passwords do not match',
        });
        //this.$store.commit('notifyList/notify', {type: 1, position: 0, message: this.loc('auth_16'), time: 3000});
        return;
      }
      window.mp.triggerServer("signup", email, login, password, this.promo);
      //this.clearModels()
    },
    checkPwd(pwd) {
      return pwd.search(/^[a-zA-Z0-9_]+$/) === -1;
    },
    checkEmail(email) {
      return email.search(/^.+@.+\..+$/) == -1;
    },
    enter(e) {
      if (e.keyCode == 13) this.register();
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