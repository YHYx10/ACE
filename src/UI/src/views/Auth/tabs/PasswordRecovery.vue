<template>
  <TabTemplate title='Forget password' subtitle='You will receive a new password by e -mail.' btn='Send e -mail' @click-action="send">
    <Input
      title='Eingetragen'
      placeholder='Email'
      v-model="email"
      type="email"
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
  data: function() {
    return {
      flood: 0,
      clickInterval: 5000,
    }
  },
  computed: {
    ...mapState('auth', ['notify']),
  },
  methods: {
    ...mapMutations('auth', ['setCurrentTab']),
    send: function() {
      if(this.flood > Date.now())return;
      this.flood = Date.now() + this.clickInterval;
      window.mp.triggerServer("auth:passRecovered", this.email)
    },
    enter(e) {
      if (e.keyCode == 13) this.send();
    },
  },
  mounted() {
    document.addEventListener("keyup", this.enter);
  },
  beforeDestroy() {
    document.removeEventListener("keyup", this.enter);
  }
};
</script>