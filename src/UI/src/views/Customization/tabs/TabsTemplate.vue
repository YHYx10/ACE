<template>
  <div class="tabs__content">
    <div class="content">
      <form class="form">
        <div class="form__body">
          <slot />
        </div>
        <div class="form__footer">
          <div class="btn btn--save" @click="click">
            Complete
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";

export default {
  data() {
    return {
      flood: 0,
      clickInterval: 1000,
    };
  },
  computed: {
    ...mapState("customization", ["firstName", "lastName", "firstCreate"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    click() {
      if (this.flood > Date.now()) return;
      this.flood = Date.now() + this.clickInterval;
      console.log(this.firstName);
      if (this.firstCreate) {
        if (
          this.firstName.length < 3 ||
          this.lastName.length < 3 ||
          this.firstName.length > 12 ||
          this.lastName.length > 18
        ) {
          this.$store.commit("notifyList/notify", {
            type: 1,
            position: 0,
            message: this.loc("chcr_18"),
            time: 3000,
          });
          return;
        }
        var regexp = /^[a-z]+$/i;
        if (!regexp.test(this.firstName) || !regexp.test(this.lastName)) {
          this.$store.commit("notifyList/notify", {
            type: 1,
            position: 0,
            message: this.loc("chcr_19"),
            time: 3000,
          });
          return;
        }
        window.mp.trigger("customization:save", this.firstName, this.lastName);
      } else window.mp.trigger("customization:save", "", "");
    },
  },
};
</script>