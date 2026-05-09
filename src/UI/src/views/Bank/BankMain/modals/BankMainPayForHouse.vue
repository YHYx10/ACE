<template>
  <div class="bank-main_payforhouse bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
      BACK
      </button>
      <div class="bank-modal_title">Pay tax</div>
      <div class="bank-modal_img">
        <img src="/img/bank/main/house.png" alt="" />
        <img src="/img/bank/main/house.png" alt="" />
      </div>
      <div class="bank-modal_descr">House tax paid</div>
      <div class="bank-modal_value">
        <span
          ><span>{{ houseBalance.toLocaleString("ru") }} /</span>&nbsp;<span>{{
            houseBalanceMax.toLocaleString("ru")
          }}</span></span
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
    ...mapState("bank", ["houseBalance", "houseBalanceMax"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    payForHouse() {
      if (this.currentSumm && this.currentSumm > 0) {
        window.mp.triggerServer("bank:payHouseTax", this.currentSumm, true);
        this.$emit("closeModal");
      }
    },
  },
};
</script>

<style lang="scss">
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / 20) + rem;
  } @else {
    @return ($px / 20) + rem;
  }
}

.bank-modal {
  &_value {
    display: flex;
    align-items: center;
    justify-content: center;

    & > span {
      font-weight: 700;
      font-size: conv(32, true);
      line-height: 95%;
      text-transform: uppercase;

      span {
        &:first-child {
          color: white;
        }

        &:nth-child(2) {
          color: rgba($color: #ffffff, $alpha: 0.55);
        }
      }
    }
  }
  &_img {
    position: relative;
    width: conv(74, true);
    height: conv(74, true);

    img {
      &:first-child {
        position: relative;
        width: 100%;
        height: 100%;
        z-index: 11;
      }

      &:last-child {
        z-index: 10;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        height: conv(148, true);
        width: conv(148, true);
        opacity: 0.07;
      }
    }
  }
}

.bank-main_payforhouse {
  .bank-modal_wrap {
    padding: conv(34, true) conv(97) conv(40, true);
    width: conv(504);
  }

  .bank-modal_close {
    margin-bottom: conv(46, true);
  }

  .bank-modal_title {
    margin-bottom: conv(59, true);
  }

  .bank-modal_descr {
    margin-top: conv(56, true);
    margin-bottom: conv(8, true);
  }

  .bank-modal_input {
    margin-top: conv(37, true);
    margin-bottom: conv(20, true);
  }
}
</style>
