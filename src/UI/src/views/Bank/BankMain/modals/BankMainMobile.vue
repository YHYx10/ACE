<template>
  <div class="bank-main_mobile bank-main_payforhouse bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
   BACK
      </button>
      <div class="bank-modal_title">Mobile communication</div>
      <div class="bank-modal_img">
        <img src="/img/bank/main/mobile.png" alt="" />
        <img src="/img/bank/main/mobile.png" alt="" />
      </div>
      <div class="bank-modal_descr">
        Telefonbalance<span>{{ configuration.Number }}</span>
      </div>
      <div class="bank-modal_value">
        <span
          ><span>$&nbsp;{{ phoneBalance.toLocaleString("ru") }}</span></span
        >
      </div>
      <div class="bank-modal_input">
        <div>The amount for replenishment</div>
        <div>
          <span>$</span>
          <input type="number" placeholder="Sum..." v-model="currentSumm" />
        </div>
      </div>
      <button class="bank-modal_action bank-btn" @click="payForMobile">Transfer</button>
    </div>
  </div>

  <!-- <div class="bank-main-mobile bank-main-modal">
    <div class="bank-main-mobile__wrap bank-main-modal__wrap">
      <div class="btn-close" @click="$emit('closeModal')"></div>
      <div class="bank-main-modal-title">{{ loc("bank:menu:85") }}</div>
      <div class="bank-main-modal-desc">{{ loc("bank:menu:86") }}</div>
      <div class="bank-main-mobile__info bank-main-modal__block-shadow">
        <div class="bank-main-mobile__info-col">
          <div class="bank-main-mobile__info-col-desc">
            {{ loc("bank:menu:87") }}
          </div>
          <div class="bank-main-mobile__info-col-value">
            {{ configuration.Number }}
          </div>
        </div>
        <div class="bank-main-mobile__info-col">
          <div class="bank-main-mobile__info-col-desc">
            {{ loc("bank:menu:88") }}
          </div>
          <div class="bank-main-mobile__info-col-value">
            $ {{ phoneBalance.toLocaleString("ru") }}
          </div>
        </div>
      </div>
      <div class="bank-main-modal-prompt">{{ loc("bank:menu:89") }}</div>
      <div class="bank-main-mobile__input bank-main-modal__input">
        <input type="number" v-model="currentSumm" />
      </div>
      <div class="bank-main-modal-btn btn-main" @click="payForMobile">
        {{ loc("bank:menu:90") }}
      </div>
    </div>
  </div> -->
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "BankMainMobile",
  data() {
    return {
      currentSumm: null,
    };
  },
  computed: {
    ...mapState("smartphone", ["configuration", "phoneBalance"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    payForMobile() {
      if (this.currentSumm && this.currentSumm > 0) {
        window.mp.triggerServer("bank:payPhoneBalance", this.currentSumm);
        this.$emit("closeModal");
      }
    },
  },
};
</script>

<style lang="scss">
.bank-main_mobile{
  .bank-modal_descr{
    span{
      color: rgba(255, 255, 255, 1);
    }
  }

  .bank-modal_img{
    img{
      transform: matrix(-1, 0, 0, 1, 0, 0);

      &:last-child{
        transform: matrix(-1, 0, 0, 1, 0, 0) translate(50%, -50%)
      }
    }
  }
}
/* .bank-main-mobile {
  &__wrap {
  }
  &__info {
    margin-bottom: 2rem;
    display: flex;
    &-col {
      display: flex;
      flex-direction: column;
      width: 50%;
      height: 100%;
      font-weight: normal;
      letter-spacing: 0.03em;
      &:last-child {
        border-left: 0.1rem solid rgba(0, 0, 0, 0.1);
        padding-left: 1.5rem;
      }
      &-desc {
        font-size: 1rem;
        line-height: 1.2rem;
        color: rgba(94, 55, 176, 0.4);
        margin-bottom: 0.15rem;
      }
      &-value {
        font-size: 1.2rem;
        line-height: 1.45rem;
        color: #5e37b0;
      }
    }
  }
  &__balance {
    font-weight: normal;
    font-size: 1.6rem;
    line-height: 1.9rem;
    letter-spacing: 0.03em;
    color: #000000;
    span {
      color: rgba(0, 0, 0, 0.3);
    }
  }
  &__input {
    &:before {
      content: "$";
      pointer-events: none;
      position: absolute;
      left: 0;
    }
    input {
      padding-left: 1.5rem;
    }
  }
} */
</style>
