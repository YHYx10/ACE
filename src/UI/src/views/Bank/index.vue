<template>
  <div class="bank">
    <!-- <div class="bank-decorate bank-decorate--small"></div>
    <div class="bank-decorate bank-decorate--middle"></div>
    <div class="bank-decorate bank-decorate--big"></div> -->
    <div class="bank-header">
      <div class="bank-header_logo">
        <span>Astro</span>
       Bank
      </div>
      <div class="bank-header_items">
        <div
          :class="[{ active: item.key === currentPage }, 'bank-header_item']"
          v-for="item in navigations"
          :key="item.key"
          @click="setCurrentPage(item.key)"
        >
          <div class="bank-header_item-icon">
            <img :src="`/img/bank/header/${item.img}`" alt="" />
          </div>
          <div class="bank-header_item-text">{{ loc(item.text) }}</div>
        </div>
      </div>
    </div>
    <transition name="bank-component" mode="out-in">
      <component
        class="bank-body"
        :is="currentPage"
        @setCurrentModal="setCurrentModal"
        @setCurrentPage="setCurrentPage"
      />
    </transition>
    <transition name="bank-modal">
      <component
        v-if="currentModal.component"
        :is="currentModal.component"
        :dataModal="currentModal.dataModal"
        @closeModal="closeModal"
      />
    </transition>
  </div>
</template>

<script>
import Main from "./BankMain";
import Deposits from "./BankDeposits";
import Business from "./BankBusiness";
import Family from "./BankFamily";
import Organization from "./BankOrganization";
import Credit from "./BankCredit";
import Withdraw from "./modals/BankModalWithdraw";
import Transfer from "./modals/BankModalTransfer";
import TopUp from "./modals/BankModalTopUp";
import PayForBiz from "./modals/BankModalPayForBiz";
import Bonus from "./modals/BankModalBonus";
import { mapGetters, mapState } from "vuex";
export default {
  name: "Bank",
  components: {
    Main,
    Deposits,
    Business,
    Family,
    Organization,
    Credit,
    Withdraw,
    Transfer,
    TopUp,
    PayForBiz,
    Bonus,
  },
  data() {
    return {
      currentModal: {
        component: null,
        dataModal: {},
      },
      currentPage: null,
      navList: [
        {
          key: "Main",
          text: "bank:menu:4",
          img: "main.png",
        },
        // {
        //   key: "Deposits",
        //   text: "bank:menu:5",
        //   img: "deposit.png",
        // },
        {
          key: "Business",
          text: "bank:menu:6",
          img: "biz.png",
        },
        {
          key: "Family",
          text: "bank:menu:7",
          img: "family.png",
        },
        {
          key: "Organization",
          text: "bank:menu:8",
          img: "org.png",
        },
        /* {
          key: "Credit",
          text: "bank:menu:9",
          img: "credit.png",
        }, */
      ],
    };
  },
  computed: {
    ...mapState("bank/business", ["bizName"]),
    ...mapState("bank/family", ["familyName"]),
    ...mapState("bank/organization", ["organizationName"]),
    ...mapGetters("localization", ["loc"]),
    navigations() {
      return this.navList.filter(
        (item) =>
          item.key == "Main" ||
          item.key == "Deposits" ||
          (item.key == "Business" && this.bizName != null) ||
          (item.key == "Family" && this.familyName != null) ||
          (item.key == "Organization" && this.organizationName != null) /*  ||
          item.key == "Credit" */
      );
    },
  },
  methods: {
    setCurrentPage(value) {
      this.currentPage = value;
    },
    setCurrentModal(obj) {
      this.currentModal.dataModal = obj.dataModal;
      this.currentModal.component = obj.component;
    },
    closeModal() {
      this.currentModal.component = null;
      this.currentModal.dataModal = {};
    },
  },
  mounted() {
    this.currentPage = this.navList[0].key;
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

.bank {
  display: grid;
  grid-template-rows: conv(205, true) 1fr;
  width: conv(1920);
  justify-self: center;
  /* padding: 2rem 5.5rem 0 5.5em; */
  /* overflow: hidden; */
  margin: 0 auto;
  height: conv(1080);
  position: relative;
  align-self: flex-start;

  &::after{
    content: '';
    position: absolute;
    z-index: -1;
    background: rgba(0, 0, 0, 0.98);
    width: 100vw;
    height: 100vh;
    left: 50%;
    transform: translateX(-50%);
    top: 0;
  }

  * {
    font-family: "Akrobat";
  }

  &-header {
    /* background: rgba(255, 255, 255, 0.02); */
    width: 100%;
    height: 100%;
    display: flex;
    align-items: center;
    position: relative;
    padding-left: conv(193);

    &_logo {
      display: flex;
      align-items: center;
      font-weight: 800;
      font-size: conv(40, true);
      line-height: conv(48, true);
      text-transform: uppercase;
      color: #ffffff;

      span {
        font-weight: 800;
        font-size: conv(40, true);
        line-height: conv(48, true);
        background: linear-gradient(
          180deg,
          #301934  0%,
          #ea0505 49.48%,
          #301934  100%
        );
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        text-fill-color: transparent;
        text-shadow: 0px 0px conv(49, true) rgba(255, 0, 0, 0.52);
      }
    }

    &_items {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      display: grid;
      grid-template-columns: repeat(5, conv(132.32, true));
      height: conv(132.32, true);
      column-gap: conv(19.68);
    }

    &_item {
      width: 100%;
      height: 100%;
      display: flex;
      align-items: flex-end;
      justify-content: center;
      background: rgba(255, 255, 255, 0.05);
      position: relative;
      transition: 0.2s ease;

      &:hover {
        background: rgba(255, 255, 255, 0.1);
      }

      &:first-child &-icon {
        height: conv(67, true);
        width: conv(67, true);
        top: conv(23, true);
      }

      &:nth-child(2) &-icon {
        top: conv(18, true);
        height: conv(72, true);
      }

      &:nth-child(3) &-icon {
        top: conv(28, true);
        height: conv(57, true);
      }

      &:nth-child(4) &-icon {
        top: conv(24, true);
        height: conv(64, true);
      }

      &:last-child &-icon {
        top: conv(23, true);
        height: conv(67, true);
      }

      &-icon {
        position: absolute;
        left: 50%;
        transform: translateX(-50%);
        //5 items width/heaight

        img {
          width: 100%;
          height: 100%;
        }
      }

      &-text {
        width: 100%;
        height: conv(42, true);
        font-weight: 700;
        font-size: conv(16, true);
        line-height: conv(19, true);
        display: flex;
        align-items: center;
        text-align: center;
        justify-content: center;
        text-transform: uppercase;
        color: #ffffff;
      }

      &.active {
        // background: linear-gradient(180deg, #db121b 0%, #591b87 100%);
        // box-shadow: 0px 0px conv(15, true) rgba(71, 44, 132, 0.5);
      }
    }
  }

  &-body {
    display: flex;
    width: 100%;
    height: 100%;
  }

  input::-webkit-outer-spin-button,
  input::-webkit-inner-spin-button {
    /* display: none; <- Crashes Chrome on hover */
    -webkit-appearance: none;
    margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
  }

  &-btn {
    transition: 0.2s ease;
    border: 0.093vmin solid rgba($color: #db121b, $alpha: 1);
      background: linear-gradient(
        rgba($color: #db121b, $alpha: 0.25),
        rgba($color: #591b87, $alpha: 0.25)
      );
      transition: 0.3s ease;
      box-shadow: inset 0 0 1.389vmin rgba($color: #dc2028, $alpha: 0.86);

      &:hover {
        transition: 0.3s ease;
        box-shadow: inset 0vh 0vh 13.889vh #db121b;
        filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
      }
  }
}

.bank {
  .range-slider {
    width: calc(100% - (2 * 1rem));
    height: 0.2rem;
    border-radius: 0.5rem;
    position: absolute;
    left: 1rem;
    bottom: 0;
    padding: 0;
    &-inner {
      position: initial;
    }
    &-rail,
    &-fill {
      height: 0.2rem;
      border-radius: 0.5rem;
      left: initial;
      transform: initial;
      bottom: 0;
      top: initial;
    }
    &-rail {
      background: rgba(255, 255, 255, 0.2);
    }
    &-fill {
      background: #5e37b0;
      box-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
    }
    &-knob {
      width: 1.15rem;
      height: 1.15rem;
      border: 0.35rem solid #fff;
      background: #5e37b0;
    }
  }
  &-prompt {
    position: absolute;
    top: -0.3rem;
    left: 0;
    transform: translateY(-100%);
    font-weight: normal;
    font-size: 1rem;
    line-height: 1.2rem;
    letter-spacing: 0.03em;
    color: #000000;
    padding: 1rem;
    background: #ffffff;
    box-shadow: 0 0.2rem 1rem rgba(0, 0, 0, 0.5);
    border-radius: 0.3rem 0.3rem 0.3rem 0;
    opacity: 0;
    max-width: 12.5rem;
    transition: 0.3s;
    pointer-events: none;
  }
  .replenishment,
  .completion {
    .bank-prompt {
      white-space: pre;
    }
  }
  .replenishment,
  .completion,
  .bank-prompt-wrap {
    &:hover {
      .bank-prompt {
        opacity: 1;
      }
    }
  }
  &-modal {
    position: fixed !important;
    width: 100vw !important;
    height: 100vh !important;
    left: 0;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    // background: radial-gradient(43.85% 77.96% at 50% 50%, #5E37B0 0%, rgba(94, 55, 176, 0) 100%), rgba(0, 0, 0, 0.9);
    background: rgba(0, 0, 0, 0.9) !important;
    z-index: 5;
    &__wrap {
      width: 24rem;
      padding: 3rem;
      background: #ffffff;
      border-radius: 0.8rem;
      display: flex;
      flex-direction: column;
      position: relative;
    }
    &-title {
      font-weight: bold;
      font-size: 2.5rem;
      line-height: 2.5rem;
      letter-spacing: 0.03em;
      color: #000000;
    }
    &-desc,
    &-prompt {
      font-weight: normal;
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.03em;
      color: rgba(0, 0, 0, 0.4);
    }
    &-desc {
      margin-bottom: 1.5rem;
      span {
        white-space: pre;
      }
    }
    &-prompt {
      margin-bottom: 0.3rem;
      display: flex;
      align-items: center;
      &.card {
        &:before {
          content: "";
          width: 1.6rem;
          height: 1.6rem;
          margin-right: 0.5rem;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          background-image: url("/img/bank/card-icon-gray.png");
        }
      }
    }
    &__block-shadow {
      background: #ffffff;
      box-shadow: 0 1.5rem 2.5rem rgba(49, 41, 184, 0.1);
      border-radius: 0.5rem;
      padding: 0.85rem 1.5rem;
      position: relative;
      &:before {
        width: 1.6rem;
        height: 1.6rem;
        position: absolute;
        left: 1.5rem;
        top: 0.9rem;
        margin-right: 0.5rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
      }
      &.house,
      &.cash,
      &.biz {
        &:before {
          content: "";
        }
        padding-left: 3.85rem;
      }
      &.house {
        &:before {
          background-image: url("/img/bank/house-icon.png");
        }
      }
      &.cash {
        &:before {
          background-image: url("/img/bank/card-icon.png");
        }
      }
      &.biz {
        &:before {
          background-image: url("/img/bank/biz-icon.png");
        }
      }
    }
    &-btn {
      height: 3rem;
      border-radius: 0.5rem;
    }
    &__input {
      position: relative;
      width: 100%;
      padding-bottom: 0.5rem;
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);
      display: flex;
      align-items: center;
      justify-content: flex-start;
      font-weight: bold;
      font-size: 2rem;
      line-height: 2rem;
      letter-spacing: 0.03em;
      color: #000000;
      margin-bottom: 1.2rem;
      input {
        width: 100%;
        font-weight: bold;
        font-size: 2rem;
        line-height: 2rem;
        letter-spacing: 0.03em;
        color: #000000;
        background-color: transparent;
        border: none;
        outline: none;
        &::-webkit-outer-spin-button,
        &::-webkit-inner-spin-button {
          /* display: none;*/
          -webkit-appearance: none;
        }
        &::placeholder {
          font-weight: normal;
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: rgba(0, 0, 0, 0.4);
        }
      }
    }
  }
  /* &:before,
  &:after {
    content: "";
    pointer-events: none;
    position: absolute;
    background: #5e37b0;
    border-radius: 50%;
    z-index: 2;
  } */
  // &:before{
  //   width: 30rem;
  //   height: 30rem;
  //   filter: blur(10rem);
  //   left: -15rem;
  //   bottom: -15rem;
  // }
  // &:after{
  //   width: 20rem;
  //   height: 20rem;
  //   right: -10rem;
  //   bottom: -10rem;
  //   filter: blur(12rem);
  //   animation: brownianMotion 36s linear infinite;
  // }
  .btn-main {
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    font-weight: normal;
    font-size: 1rem;
    border-radius: 0.2rem;
    background-color: #5e37b0;
    border: 1px solid transparent;
    transition: 0.2s;
    &:before {
      width: 1.5rem;
      height: 1.5rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      margin-right: 0.7rem;
    }
    &:hover {
      background-color: #452883;
    }
  }
  .btn-close {
    width: 2.5rem;
    height: 2.5rem;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgba(255, 255, 255, 0.1);
    border: 1px solid rgba(255, 255, 255, 0.1);
    position: absolute;
    right: -1rem;
    top: 0;
    transform: translateX(100%);
    transition: 0.2s;
    &:hover {
      background-color: rgba(255, 255, 255, 0.2);
    }
    &:after {
      content: "";
      width: 1rem;
      height: 1rem;
      position: absolute;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url("/img/bank/close.png");
    }
  }
  &-logo {
    width: 6.75rem;
    height: 2.05rem;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    background-image: url("/img/bank/logo.png");
    position: absolute;
    left: 1.5rem;
    top: 1.5rem;
    &:after {
      content: "";
      width: 6rem;
      height: 0.1rem;
      position: absolute;
      left: 50%;
      transform: translateX(-50%);
      bottom: -2rem;
      background: rgba(255, 255, 255, 0.14);
    }
  }
  &-decorate {
    border-radius: 50%;
    position: absolute;
    background: radial-gradient(
      50% 50% at 50% 50%,
      rgba(255, 255, 255, 0) 0%,
      rgba(255, 255, 255, 0.1) 100%
    );
    pointer-events: none;
    &--small {
      width: 3.7rem;
      height: 3.7rem;
      top: 3rem;
      left: 19rem;
      // animation: brownianMotion 11s linear infinite;
    }
    &--middle {
      width: 9rem;
      height: 9rem;
      top: 4.5rem;
      left: 10rem;
      // animation: brownianMotion 18s linear infinite;
    }
    &--big {
      width: 40rem;
      height: 40rem;
      left: 50%;
      top: 8.5rem;
      margin-left: -20rem;
      // animation: brownianMotion 36s linear infinite;
    }
  }
  &__nav {
    position: absolute;
    left: 0;
    top: 0;
    width: 9.5rem;
    min-width: 9.5rem;
    height: 100%;
    padding: 7.35rem 1.75rem 0 1.75rem;
    background: #030c20;
    &-item {
      width: 6rem;
      height: 6rem;
      background: rgba(255, 255, 255, 0.1);
      border-radius: 0.2rem;
      display: flex;
      align-items: center;
      justify-content: center;
      flex-direction: column;
      margin-bottom: 0.2rem;
      z-index: 3;
      &:last-child {
        margin-bottom: 0;
      }
      &:hover,
      &.active {
        background: #5e37b0;
        box-shadow: 0 0.5rem 5rem rgba(94, 55, 176, 0.6);
      }
      &__icon {
        width: 2rem;
        height: 2rem;
        font-size: 0.9rem;
        background-size: contain;
        background-repeat: no-repeat;
        background-position: center;
        margin-bottom: 0.5rem;
      }
    }
  }
  &__body {
    display: flex;
    min-width: 64rem;
    min-height: 43rem;
    z-index: 1;
  }
  .bank-component-enter-active,
  .bank-component-leave-active {
    transition: 0.1s;
  }
  .bank-component-enter, .bank-component-leave-to
  /* .bank-component-leave-active below version 2.1.8 */ {
    opacity: 0;
  }

  .bank-modal-enter-active {
    transition: all 0.2s ease;
  }
  .bank-modal-leave-active {
    transition: all 0.2s ease;
  }
  .bank-modal-enter, .bank-modal-leave-to
  /* .bank-modal-leave-active below version 2.1.8 */ {
    /* transform: translateY(1rem); */
    opacity: 0;
  }
  // @keyframes brownianMotion {
  //   0% {
  //     transform: translateX(0%) translateY(0%) scale(1) rotate(0deg);
  //   }
  //   20% {
  //     transform: translateX(5%) translateY(6%) scale(1.05) rotate(72deg);
  //   }
  //   40% {
  //     transform: translateX(-4%) translateY(-2%) scale(.9) rotate(144deg);
  //   }
  //   60% {
  //     transform: translateX(4%) translateY(3%) scale(1.07) rotate(216deg);
  //   }
  //   80% {
  //     transform: translateX(-3%) translateY(-1%) scale(.86) rotate(288deg);
  //   }
  //   100% {
  //     transform: translateX(0%) translateY(0%) scale(1) rotate(360deg);
  //   }
  // }
}
</style>
