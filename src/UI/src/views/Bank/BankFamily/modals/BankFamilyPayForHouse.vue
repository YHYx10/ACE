<template>
  <div class="bank-main_payforhouse bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
BACK
      </button>
      <div class="bank-modal_title">Fill out the remaining amount</div>
      <div class="bank-modal_img">
        <img src="/img/bank/main/house.png" alt="" />
        <img src="/img/bank/main/house.png" alt="" />
      </div>
      <div class="bank-modal_descr">House tax paid</div>
      <div class="bank-modal_value">
        <span
          ><span>{{ houseTaxBalance.toLocaleString("ru") }} /</span>&nbsp;<span
            >{{ houseTaxBalanceMax.toLocaleString("ru") }}</span
          ></span
        >
      </div>
      <div class="bank-modal_input">
        <div>The amount for replenishment</div>
        <div>
          <span>$</span>
          <input type="number" placeholder="Sum..." v-model="currentSumm" />
        </div>
      </div>
      <button class="bank-modal_action bank-btn" @click="payForHouse">Transfer</button>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "BankMainPayForHouse",
  data() {
    return {
      currentSumm: null,
    };
  },
  computed: {
    ...mapState("bank/family", ["houseTaxBalance", "houseTaxBalanceMax"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    payForHouse() {
      if (this.currentSumm && this.currentSumm > 0) {
        window.mp.triggerServer("bank:payHouseTax", this.currentSumm, false);
        this.$emit("closeModal");
      }
    },
  },
};
</script>

<style lang="scss">

</style>
