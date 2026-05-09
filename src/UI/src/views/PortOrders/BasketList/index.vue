<!-- <template>
  <div class="basket-list">
    <div class="paper-check">
      <div class="header">
        <div class="title">{{loc('PortOrders_7')}}</div>
        <div 
          class="btn-clear"
          @click="clearBasket"
        ></div>
      </div>
      <div class="desc" v-if="this.basket.length === 0">
        <div class="text">{{loc('PortOrders_8')}}</div>
        <div class="icon"></div>
      </div>
      <div 
        v-else 
        class="basket-wrap"
      >
        <basket-list-item 
          v-for="(item, index) in this.basket"
          :key="item.i"
          :item="item"
          :index="index"
        />
      </div>
      <div class="result">
        <div class="result__desc">{{loc('PortOrders_9')}}:</div>
        <div class="result__value">${{basketTotal}}</div>
      </div>
    </div>
    <div
      class="btn-buy"
      @click="buyProducts()"
    >{{loc('PortOrders_10')}}</div>
  </div>
</template> -->
<template>
  <div class="cart-list">
    <div class="head-title">
Your order
    </div>
    <div class="list">
      <ShoppingCartItem
        v-for="(item, index) in this.basket.map((item) => ({
          ...item,
          unicName: item.name,
        }))"
        input
        :key="item.id"
        :item="item"
        :index="index"
        @deleteItem="deleteItem"
        @increase="(key) => increase(key)"
        @decrease="(key) => decrease(key)"
        @updateValue="updateCount([index, $event])"
      />
    </div>
    <div class="final-price">
      <div class="subtitle">
       The final price
      </div>
      <div class="price">$ {{ basketTotal }}</div>
    </div>
    <DefaultBtn @click="buyProducts">Pay</DefaultBtn>
    <div class="payment-type">
      <div class="payment-type">
        <div class="wrap">
          <button
            style="width: 100%; position: relative;"
            class="payment-cash"
            :class="!isPaymentBank ? 'button-first' : 'button-second'"
            @click="setPaymentType(false)"
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
            :class="isPaymentBank ? 'button-first' : 'button-second'"
            @click="setPaymentType(true)"
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
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import ShoppingCartItem from '../../UI/ShoppingCarts/components/ShoppingCartItem.vue'
// import BasketListItem from './BasketListItem'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'

export default {
  name: 'ProductsList',

  components: {
    ShoppingCartItem,
    DefaultBtn,
  },
  data() {
    return {
      isPaymentBank: false,
    }
  },

  computed: {
    ...mapState('portOrders', ['basket']),
    ...mapGetters('localization', ['loc']),
    basketTotal: function() {
      return this.basket.reduce(
        (total, basketItem) => total + basketItem.cost * basketItem.count,
        0
      )
    },
  },

  methods: {
    ...mapMutations('portOrders', ['clearBasket', 'increase', 'decrease', 'updateCount', 'deleteItem']),
    buyProducts: function() {
      if (this.basket.length == 0) return
      const items = []
      this.basket.forEach((item) => {
        items.push({ id: item.id, count: item.count })
        //items[item.id] = item.count
      })
      this.clearBasket()
      window.mp.trigger('portOrders:buy', JSON.stringify(items), !this.isPaymentBank)
    },
    setPaymentType(value) {
      this.isPaymentBank = value
    },
  },
}
</script>

<style lang="scss" scoped>
div,
button {
  text-transform: uppercase;
  font-family: 'Akrobat';
  color: #fff;
}
.cart-list {
  .head-title {
    font-weight: 700;
    font-size: 3.333vh;
    line-height: 3.981vh;
    margin-bottom: 2.778vh;
  }
  .list {
    width: 28.704vh;
    height: 27.963vh;
    overflow-y: scroll;
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    flex-shrink: 1;
    gap: 0.926vh;
    &::-webkit-scrollbar {
      width: 0.463vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.04);
    }
    &::-webkit-scrollbar-thumb {
      background: #301934 ;
    }
  }
  .final-price {
    margin-left: 1.852vh;
    margin-top: 4.907vh;
    margin-bottom: 3.426vh;
    .subtitle {
      font-size: 1.481vh;
      line-height: 1.852vh;
    }
    .price {
      font-size: 2.963vh;
      font-weight: 700;
      line-height: 3.704vh;
      text-transform: uppercase;
      margin-top: 0.278vh;
      color: #a0ff98;
    }
  }
  button {
    width: 100%;
    height: 6.944vh;
    font-weight: 700;
    font-size: 2.222vh;
    line-height: 2.778vh;
    color: #fff;
  }
}

.payment-type .wrap {
  display: flex;
  margin-top: 1.852vh;
  gap: 0.463vh;
  button {
    width: 100%;
    font-weight: 700;
    font-size: 1.667vh;
    line-height: 2.037vh;
    flex-direction: column;
    width: 13.426vh;
    height: 6.944vh;
    display: flex;
    justify-content: center;
    align-items: center;
    outline: none;
    svg {
      width: 2.778vh;
      &.payment-cash {
        margin-top: 0.185vh;
      }
      &.payment-bank {
        margin-top: 0.37vh;
      }
    }
  }
}

.button-first {
  background: #301934 ;
}

.button-second {
  background: rgba(255, 255, 255, 0.05);
  border: 0.16vh solid rgba(255, 255, 255, 0.16);
}
</style>
