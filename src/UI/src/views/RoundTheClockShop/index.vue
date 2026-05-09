<template>
  <div class="round-the-clock-shop">
    <ExitCross @click="close" class="exit-cross" />
    <div class="content">
      <div class="block-products">
        <div class="header">
          <div class="name">store <span> 24/7</span></div>
          <div class="balance">
            <div class="balance-title">
        Your balance
            </div>
            <div class="balance-value">$ {{ money.toLocaleString('ru') }}</div>
          </div>
        </div>
        <ProductsList />
      </div>
      <div class="block-basket">
        <shopping-cart
          :list="basket"
          @deleteItem="(key) => deleteItem({ key })"
          @increase="(key) => changeInQuantityUp({ key })"
          @decrease="(key) => changeInQuantityDown({ key })"
          @onBuy="buyProducts"
        />
      </div>
    </div>
  </div>
</template>

<script>
import { mapMutations, mapState, mapGetters } from 'vuex'
import ExitCross from '../UI/components/ExitCross'
import ProductsList from './ProductsList'
import ShoppingCart from '../UI/ShoppingCarts/ShoppingCarts.vue'

export default {
  name: 'RoundTheClockShop',

  components: {
    ProductsList,
    ShoppingCart,
    ExitCross
  },

  computed: {
    ...mapState('roundTheClockShop', ['basket']),
    ...mapState("hud", [
      "money",
    ]),
    ...mapGetters('localization', ['loc']),
    color() {
      return this.basketTotal > this.money ? '#FF0000' : '$FFFFFF'
    },
    basketTotal: function() {
      return this.basket.reduce(
        (total, basketItem) => total + basketItem.cost * basketItem.count,
        0
      )
    },
  },

  methods: {
    ...mapMutations('roundTheClockShop', [
      'clearBasket',
      'deleteItem',
      'changeInQuantityUp',
      'changeInQuantityDown',
    ]),
    buyProducts: function(isPaymentBank) {
      const items = {}
      this.basket.forEach((item) => {
        items[item.product] = item.count
      })
      this.clearBasket()
      window.mp.trigger('shop24:buy', JSON.stringify(items), !isPaymentBank)
    },
    close() {
      this.clearBasket()
      window.mp.trigger('shop24:close')
    },
  },
}
</script>

<style lang="scss" scoped>
.round-the-clock-shop {
  background: rgba(0, 0, 0, 0.98);
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1;
  color: #fff;
  font-family: 'Akrobat';
  font-weight: 700;
  text-transform: uppercase;
  display: flex;
  justify-content: center;
  align-items: center;
  .exit-cross {
    position: absolute;
    right: 3.704vh;
    top: 3.704vh;
  }
  .content {
    display: flex;
    gap: 8.981vh;
  }
  .block-products {
    display: flex;
    flex-direction: column;
    gap: 3.981vh;
  }
  .header {
    display: flex;
    .name {
      font-weight: 800;
      font-size: 5.093vh;
      line-height: 4.44vh;
      color: #ffffff;
      & span {
        text-transform: none;
        background: linear-gradient(
          180deg,
          #301934  0%,
          #ea0505 49.48%,
          #301934  100%
        );
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        text-shadow: 0px 0px 4.53vh rgba(255, 0, 0, 0.52);
      }
    }
  }
  .balance {
    display: flex;
    flex-direction: column;
    margin-left: 36.667vh;
    &-title {
      color: rgba(255, 255, 255, 0.55);
      font-size: 1.296vh;
      line-height: 1.296vh;
    }
    &-value {
      color: #ffffff;
      font-size: 2.963vh;
      line-height: 2.963vh;
    }
  }
}

</style>
