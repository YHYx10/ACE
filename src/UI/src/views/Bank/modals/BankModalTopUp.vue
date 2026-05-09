<template>
  <div class="bank-top-up bank-modal">
    <div class="bank-modal_wrap">
      <button class="bank-modal_close" @click="$emit('closeModal')">
        <div><img src="/img/inputMenu/arrow.svg" alt="" /></div>
   BACK
      </button>
      <div class="bank-modal_title">Fill out the remaining amount</div>
      <div class="bank-modal_descr">Pay the amount to fill up the account </div>
      <div class="bank-modal_input">
        <div>The amount for replenishment</div>
        <div>
          <span>$</span>
          <input type="number" placeholder="Sum..." v-model="currentSumm" />
        </div>
      </div>
      <button class="bank-modal_action bank-btn" @click="topUp">
     Transfer
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "BankModalTopUp",
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
    topUp() {
      if (!this.currentSumm || this.currentSumm <= 0) return;
      switch (this.dataModal.type) {
        case "personal":
        case "family":
        case "business":
        case "fraction":
          window.mp.triggerServer(
            "bank:topUp",
            this.currentSumm,
            this.dataModal.type,
            0
          );
          break;
        case "deposit":
          window.mp.triggerServer(
            "bank:topUp",
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
  * {
    font-family: "Akrobat";
  }

  &_wrap {
    display: flex;
    flex-direction: column;
    align-items: center;
    background: linear-gradient(
      180deg,
      rgba(14, 14, 14, 0.95) 0%,
      rgba(14, 14, 15, 0.95) 100%
    );
    z-index: 100;
    position: relative;

    span,
    img,
    button,
    div {
      z-index: 8;
    }
  }

  &_close {
    cursor: pointer;
    display: flex;
    align-items: center;
    font-weight: 700;
    font-size: conv(20, true);
    line-height: conv(24, true);
    color: #ffffff;
    background: none;
    outline: none;
    border: none;
    transition: 0.2s ease;

    &:hover {
      div {
        background: rgba(255, 255, 255, 0.15);
      }
    }

    div {
      margin-right: conv(14);
      width: conv(47, true);
      height: conv(47, true);
      background: rgba(255, 255, 255, 0.07);
      position: relative;

      img {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        height: conv(14, true);
      }
    }
  }

  &_title,
  &_descr {
    text-align: center;
    text-transform: uppercase;
    font-weight: 700;
  }

  &_title {
    font-size: conv(40, true);
    line-height: conv(48, true);
    color: #ffffff;
  }

  &_descr {
    font-size: conv(16, true);
    line-height: conv(19, true);
    color: rgba(255, 255, 255, 0.55);
  }

  &_input {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: conv(310);
    position: relative;

    div:first-child {
      font-weight: 700;
      font-size: conv(16, true);
      line-height: conv(31, true);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
    }

    &:not(.bank-modal_select)div:last-child {
      width: 100%;
      height: conv(65, true);
      position: relative;
      background: rgba(217, 217, 217, 0.05);
      display: flex;
      align-items: center;

      span {
        position: absolute;
        top: 0;
        left: 0;
        width: conv(49, true);
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        font-weight: 700;
        font-size: conv(24, true);
        line-height: conv(29, true);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;
      }

      input {
        position: relative;
        z-index: 100;
        width: 100%;
        height: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        background: none;
        border: none;
        outline: none;

        &,
        &::placeholder {
          text-align: center;
          font-weight: 700;
          font-size: conv(14, true);
          line-height: conv(17, true);
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.5);
        }
      }
    }
  }

  &_action {
    width: 100%;
    height: conv(75, true);
    cursor: pointer;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    border: none;
    outline: none;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: 700;
    font-size: conv(24, true);
    line-height: conv(29, true);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
    background: linear-gradient(
      rgba($color: #301934 , $alpha: 0.25),
      rgba($color: #591b87, $alpha: 0.25)
    );
    transition: 0.3s ease;
    box-shadow: inset 0 0 1.389vmin rgba($color: #dc2028, $alpha: 0.86);

    &:hover {
      transition: 0.3s ease;
      box-shadow: inset 0vh 0vh 13.889vh #301934 ;
      filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
    }
  }
}

.bank-top-up {
  .bank-modal_wrap {
    padding: conv(35, true) conv(97) conv(39, true);
  }

  .bank-modal_close {
    margin-bottom: conv(26, true);
  }

  .bank-modal_title {
    margin-bottom: conv(10, true);
  }

  .bank-modal_descr {
    margin-bottom: conv(33, true);
  }

  .bank-modal_input {
    margin-bottom: conv(20, true);
  }
}
</style>
