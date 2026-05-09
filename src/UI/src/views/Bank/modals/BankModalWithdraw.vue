<template>
  <div class="bank-withdraw bank-top-up bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
       BACK
      </button>
      <div class="bank-modal_title">{{ loc("bank:menu:10") }}</div>
      <div class="bank-modal_descr">{{ loc("bank:menu:11") }}</div>
      <div class="bank-modal_input">
        <div>{{ loc("bank:menu:13") }}</div>
        <div>
          <span>$</span>
          <input type="number" placeholder="Sum..." v-model="currentSumm" />
        </div>
      </div>
      <button class="bank-modal_action bank-btn" @click="withdraw">
        {{ loc("bank:menu:14") }}
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "BankModalWithdraw",
  props: {
    dataModal: Object,
  },
  data() {
    return {
      currentSumm: null,
    };
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    withdraw() {
      if (!this.currentSumm || this.currentSumm <= 0) return;
      switch (this.dataModal.type) {
        case "personal":
        case "business":
        case "family":
        case "fraction":
          window.mp.triggerServer(
            "bank:withdraw",
            this.currentSumm,
            this.dataModal.type,
            0
          );
          break;
        case "deposit":
          window.mp.triggerServer(
            "bank:withdraw",
            this.currentSumm,
            this.dataModal.type,
            this.dataModal.id
          );
          break;
      }
      this.$emit("closeModal");
    },
  },
};
</script>

<style lang="scss">
</style>
