<template>
  <div class="payment-methods">
    <div
      :class="[{ active: currentPayment === 0 }, 'payment-methods__item']"
      @click="setCurrentPayment(0)"
    >
      <img src="/img/tattooShop/cash.png" alt="" />
      <div class="text">{{ loc("cityHallWeb_13") }}</div>
    </div>
    <div
      :class="[{ active: currentPayment === 1 }, 'payment-methods__item']"
      @click="setCurrentPayment(1)"
    >
      <img src="/img/tattooShop/bank.png" alt="" />
      <div class="text">{{ loc("cityHallWeb_14") }}</div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "Payment",

  props: {
    currentPayment: Number,
    setCurrentPayment: Function,
  },

  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.payment-methods {
  display: grid;
  height: conv(75);
  grid-template-columns: conv(145) conv(145);
  column-gap: conv(5);

  &__item {
    display: flex;
    align-items: center;
    justify-content: space-between;
    flex-direction: column;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.25);
    transition: 0.2s ease;
    position: relative;
    padding: conv(10) 0;
    cursor: pointer;

    img,
    div {
      z-index: 4;
    }

    &:hover {
      background: rgba(0, 0, 0, 0.4);
    }

    &::after {
      content: "";
      z-index: 3;
      background: #301934 ;
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      opacity: 0;
      transition: 0.2s ease;
    }

    &.active {
      &:after {
        opacity: 1;
      }
    }

    .text {
      font-weight: 700;
      font-size: conv(18);
      line-height: conv(22);
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
    }

    img{
      height: conv(30);
      width: conv(30);
    }
  }
}
</style>
