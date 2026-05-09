<template>
  <div class="payment-component">
    <div class="payment-type">
      <button
        style="width: 100%; position: relative;"
        class="payment-cash"
        :class="paymentType === 'cash' ? 'button-first' : 'button-second'"
        @click="setPaymentType('cash')"
      >
        <svg viewBox="0 0 24 24">
          <path
            fill="#fff"
            d="M2 6.75C2 5.784 2.784 5 3.75 5h13.5c.966 0 1.75.784 1.75 1.75v8.5A1.75 1.75 0 0 1 17.25 17H3.75A1.75 1.75 0 0 1 2 15.25v-8.5Zm3-.5v1a.75.75 0 0 1-.75.75h-1v1.5h1A2.25 2.25 0 0 0 6.5 7.25v-1H5Zm5.5 7.25a2.25 2.25 0 1 0 0-4.5 2.25 2.25 0 0 0 0 4.5Zm-7.25.5h1a.75.75 0 0 1 .75.75v1h1.5v-1a2.25 2.25 0 0 0-2.25-2.25h-1V14Zm12.75.75a.75.75 0 0 1 .75-.75h1v-1.5h-1a2.25 2.25 0 0 0-2.25 2.25v1H16v-1Zm0-7.5v-1h-1.5v1a2.25 2.25 0 0 0 2.25 2.25h1V8h-1a.75.75 0 0 1-.75-.75ZM4.401 18.5A2.999 2.999 0 0 0 7 20h10.25A4.75 4.75 0 0 0 22 15.25V10a3 3 0 0 0-1.5-2.599v7.85a3.25 3.25 0 0 1-3.25 3.25H4.401Z"
          />
        </svg>
        <div>Cash</div>
      </button>
      <button
        style="width: 100%; position: relative;"
        class="payment-bank"
        :class="paymentType === 'bank' ? 'button-first' : 'button-second'"
        @click="setPaymentType('bank')"
      >
        <svg viewBox="0 0 24 24">
          <path
            fill="#fff"
            d="M14.783 2.002a2.283 2.283 0 0 0-.46.072L3.785 4.621C2.54 4.925 1.76 6.172 2.066 7.41l1.478 5.985a2.304 2.304 0 0 0 1.405 1.586V13.54c0-2.12 1.738-3.846 3.875-3.846h9.761l-1.453-5.937a2.31 2.31 0 0 0-.86-1.292 2.34 2.34 0 0 0-1.49-.462Zm.944 3.317.63 2.62-2.616.624-.654-2.595 2.64-.649Zm-6.903 5.913A2.317 2.317 0 0 0 6.5 13.539v6.153A2.317 2.317 0 0 0 8.824 22h10.85A2.317 2.317 0 0 0 22 19.692V13.54a2.317 2.317 0 0 0-2.325-2.307H8.825Zm0 1.226h10.85c.598 0 1.09.488 1.09 1.081v.77H7.735v-.77c0-.593.493-1.081 1.09-1.081Zm-1.09 4.158h13.03v3.076c0 .593-.492 1.082-1.09 1.082H8.825a1.1 1.1 0 0 1-.769-.319 1.083 1.083 0 0 1-.32-.762v-3.077Z"
          />
        </svg>
        <div>Card</div>
      </button>
    </div>
    <div class="payment">
      <button class="price button-second">
        {{ price.toLocaleString('en-US') || 0 }} <span>$</span>
      </button>
      <button 
        class="buy button-second"
        @click="onBuy"
      >
        {{ loc('barber_12') }}
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'PaymentBlock',
  props: {
    price: Number,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  data: () => {
    return {
      /**
       * value: "bank" || "cash"
       */
      paymentType: 'cash',
    }
  },
  methods: {
    onBuy() {
      this.$emit('onBuy')
    },
    setPaymentType(value) {
      this.paymentType = value
    },
  },
}
</script>

<style lang="scss" scoped>
$color-primary: #301934 ;
$color-highlight: #301934 ;
$color-secondary: rgba(255, 255, 255, 0.05);
.payment-component {
  color: #fff;
  width: 100%;
  margin-top: 4.1vh;
  button {
    width: 100%;
    color: #fff;
    display: flex;
    justify-content: center;
    align-items: center;
    background: transparent;
    outline: none;
    border: 0.104vw solid transparent;
    transition: 0.4s;
    &.button-first {
      background: $color-primary;
      transition: 0.05s;
    }

    &.button-second {
      background: $color-secondary;
      border: 0.104vw solid rgba(255, 255, 255, 0.16);
    }
  }

  .payment-type {
    display: flex;
    width: 100%;
    button {
      width: 100%;
      font-family: 'Inter';
      font-weight: 600;
      font-size: 1.11vh;
      line-height: 1.388vh;
      flex-direction: column;
      height: 6.11vh;
      svg {
        width: 2.22vh;
        &.payment-cash {
          margin-top: 0.185vh;
        }
        &.payment-bank {
          margin-top: 0.37vh;
        }
      }
    }
  }

  .payment {
    margin-top: 2.315vh;
    display: flex;
    width: 100%;
    gap: 0.92vh;
    button {
      height: 5.65vh;
      font-family: 'Montserrat';
      font-style: normal;
      font-weight: 700;
      font-size: 1.85vh;
      line-height: 2.22vh;
      span {
        margin-left: .8vh;
        color: rgba(255, 255, 255, 0.22);
      }
      &.buy {
        border: 0.104vw solid rgba(255, 255, 255, 0.22);
        transition: 0.3s;
        font-family: 'Montserrat';
        &:hover {
          transition: 0s;
          background-color: $color-primary;
        }
      }
    }
  }
}
</style>
