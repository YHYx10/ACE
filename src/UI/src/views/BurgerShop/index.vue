<template>
  <div class="burger-shop">
    <div class="burger-shop-body">
      <ExitCross class="exit-cross" @click="close" />
      <div class="burger-shop__body">
        <div class="left-col">
          <TitleComponent class="title-component" :titlePrimary="'BURGER'" :titleSecondary="'STORE'" together />
          <products-list/>
        </div>
        <shopping-cart 
          :list="shoppingCart" 
          @deleteItem="key => deleteItem({key})"
          @increase="key => changeInQuantityUp({key})"
          @decrease="key => changeInQuantityDown({key})"
          @onBuy="buyProducts"
        />
      </div>
    </div>    
  </div>
</template>

<script>
import { mapMutations, mapGetters, mapState } from 'vuex';
import ProductsList from './ProductsList'
import ShoppingCart from '../UI/ShoppingCarts/ShoppingCarts.vue'
import ExitCross from '../UI/components/ExitCross'
import TitleComponent from '../UI/components/TitleComponent'

export default {
  name: 'BurgerShop',

  components: {
    ProductsList,
    ShoppingCart,
    ExitCross,
    TitleComponent
  },

  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('burgerShop', ['items', 'shoppingCart']),
  },

  methods: {
    ...mapMutations('burgerShop', ['clearShoppingCart']),
    ...mapMutations('burgerShop', [
      'deleteItem',
      'changeInQuantityUp',
      'changeInQuantityDown',
    ]),
    close(){
      this.clearShoppingCart();
      window.mp.trigger("burgerShot::close")
    },
    buyProducts: function(isPaymentBank) {
      const items = {};
      this.shoppingCart.forEach(item => {
        items[item.unicName] = item.count;
      });
      this.clearShoppingCart();
      window.mp.trigger('burgerShot::buy', JSON.stringify(items), !isPaymentBank);
    },
  }
}
</script>

<style lang="scss" scoped>
  .burger-shop{
    width: 100%;
    height: 100%;
    position: absolute;
    top: 0;
    left: 0;
    background: radial-gradient(50% 99.27% at 50% 50%, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.8) 75%, rgba(0, 0, 0, 0.9) 100%);
    background-position: bottom;
    background-size: contain;
    background-repeat: no-repeat;
    .exit-cross {
      position: absolute;
      right: 3.704vh;
      top: 3.704vh;
    }
    &__body{
      display: flex;
      align-items: center;
      justify-content: space-between;
      height: 100%;
      margin-top: 6.852vh;
      padding: 0 4.63vh 0 3.704vh;
      .left-col {
        .title-component {
          margin-bottom: 1.852vh;
        }
      }
    }
  }
</style>
