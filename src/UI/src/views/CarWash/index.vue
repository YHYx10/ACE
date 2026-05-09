<template>
  <div class="car-wash">
    <div class="content">
      <div class="header">
        <div class="title">
          <span class="title-primary">Astro</span>
          <span>Carwash</span>
          <!-- <div class="number">#{{washId}}</div> -->
        </div>
        <div class="about">
     Service choice
        </div>
      </div>
      <div class="list">
        <ServiceItem
          v-for="item in servicesList"
          :key="item.key"
          :item="item"
          :basketList="basketList"
          @interactionWidthBasketList="interactionWidthBasketList(item)"
        />
      </div>
      <div class="payment">
        <div class="total-amount">
          <div class="name">Total amount of the payment:</div>
          <div class="value">$ {{ totalAmount }}</div>
        </div>
        <div class="pay">
          <button
            class="type"
            :class="{ active: currentPayment === 0 }"
            @click="setCurrentPayment(0)"
          >
            <CashSVG class="ico" />
            <div>Cash</div>
          </button>
          <button
            class="type"
            :class="{ active: currentPayment === 1 }"
            @click="setCurrentPayment(1)"
          >
            <CardSVG class="ico" />
            <div>Card</div>
          </button>
          <DefaultBtn @click="payForWash" class="pay-btn">
            Pay
          </DefaultBtn>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DefaultBtn from '../UI/button/DefaultBtn.vue'
import { mapGetters, mapState } from 'vuex'
import ServiceItem from './components/ServiceItem.vue'
import CardSVG from './components/CardSVG.vue'
import CashSVG from './components/CashSVG.vue'

export default {
  name: 'CarWash',
  computed: {
    ...mapState('carWash', ['washId', 'servicesList']),
    ...mapGetters('localization', ['loc']),
    totalAmount() {
      let amount = 0
      this.basketList.forEach(function(item) {
        amount = amount + Number(item.cost)
      })
      return amount.toLocaleString('ru')
    },
  },
  data() {
    return {
      currentPayment: 0,
      basketList: [],
    }
  },
  components: { DefaultBtn, ServiceItem, CardSVG, CashSVG },
  methods: {
    setCurrentPayment(value) {
      this.currentPayment = value
    },
    interactionWidthBasketList(item) {
      if (!this.basketList.includes(item)) {
        this.basketList.push(item)
      } else {
        this.basketList = this.basketList.filter((element) => element !== item)
      }
    },
    payForWash: function() {
      if (this.basketList.length > 0) {
        window.mp.triggerServer(
          'carwash:buy',
          this.currentPayment,
          JSON.stringify(this.basketList.map((item) => item.key))
        )
        window.mp.trigger('carwash::exit')

        this.basketList = []
      }
    },
  },
  mounted() {
    this.currentPayment = 0
  },
}
</script>

<style lang="scss" scoped>
.car-wash {
  width: 1080vw;
  height: 100vh;
  display: flex;
  align-items: center;
  overflow: hidden;
  background: rgba(0, 0, 0, 0.9);
  &::after {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 100vh;
    height: 100vh;
    background: rgba(255, 255, 255, 0.2);
    opacity: 0.25;
    filter: blur(18.519vh);
  }
  span,
  div,
  button {
    font-family: 'Akrobat';
    color: #ffffff;
  }
  .content {
    position: relative;
    z-index: 1;
    margin: 0 auto;
    width: 122.315vh;
    .header {
      display: flex;
      align-items: flex-end;
      justify-content: space-between;
      .title {
        font-weight: 800;
        display: flex;
        align-items: center;
        font-size: 5.926vh;
        line-height: 7.13vh;
        .title-primary {
          background: linear-gradient(
            180deg,
            #301934  0%,
            #ea0505 49.48%,
            #301934  100%
          );
          text-shadow: 0vh 0vh 4.53vh rgba(255, 0, 0, 0.52);
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
          background-clip: text;
        }
        .number {
          margin-left: 1.944vh;
          font-size: 2.963vh;
          line-height: 3.519vh;
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.55);
        }
      }

      .about {
        font-weight: 800;
        font-size: 3.704vh;
        line-height: 4.444vh;
        text-transform: uppercase;
      }
    }

    .list {
      margin-top: 4.352vh;
      display: flex;
      justify-content: space-between;
    }

    .payment {
      display: flex;
      justify-content: space-between;
      .total-amount {
        display: flex;
        align-items: flex-end;
        font-weight: 700;
        gap: 1.852vh;
        margin-bottom: 0.926vh;
        .name {
          font-size: 2.963vh;
          line-height: 3.519vh;
          color: rgba(255, 255, 255, 0.55);
          text-transform: uppercase;
        }
        .value {
          font-size: 4.444vh;
          line-height: 5.37vh;
          color: #a0ff98;
          margin-bottom: -0.556vh;
        }
      }
      .pay {
        margin-top: 5.556vh;
        display: flex;
        width: fit-content;
        height: 6.944vh;
        color: #ffffff;
        text-transform: uppercase;
        gap: 0.463vh;
        .type {
          display: flex;
          flex-direction: column;
          justify-content: center;
          align-items: center;
          color: #ffffff;
          width: 13.426vh;
          background: rgba(0, 0, 0, 0.25);
          font-weight: 700;
          font-size: 1.667vh;
          display: flex;
          text-transform: uppercase;
          align-items: center;
          .ico {
            width: 2.778vh;
          }
          &.active {
            background: #301934 ;
          }
        }

        .pay-btn {
          margin-left: 0.463vh;
          width: 27.315vh;
          height: 100%;
          color: #ffffff;
          font-family: 'Akrobat';
          font-weight: 700;
          font-size: 2.222vh;
          line-height: 2.685vh;
          text-transform: uppercase;
        }
      }
    }
  }
}
</style>
