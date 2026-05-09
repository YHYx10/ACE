<template>
  <div class="barbershop">
    <div class="left-side">
      <title-component
        :titlePrimary="'Astro'"
        :titleSecondary="'Barbershop'"
        :about="'If you love your hair, trust our experienced hairdresser !'"
      />
      <select-category
        :categories="categories"
        :currentType="currentCategory.type"
        :currentTitle="currentCategory.title"
        @setCurrentCategory="setCurrentCategory"
      />
      <value-selector
        style="margin-bottom: 3.481vh;"
        :currentTitle="loc(currentCategory.title)"
        :value="value"
        :currentTypes="getConfig(currentCategory.type)"
        @onChange="change"
      />
      <select-color
        :value="value"
        :currentColors="getConfig(currentCategory.type).colors"
        @onChange="change"
      />
      <payment-block
        :price="(getConfig(currentCategory.type).price / 100) * price"
        @onBuy="buy"
        @onChangePay="onChangePay"
      />
      <!-- <exit-button /> -->
    </div>
    <div class="right-side">
      <balance-block showTitle :balance = "getCash()" />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import SelectCategory from './components/SelectCategory.vue'
import SelectColor from './components/SelectColor.vue'
import ValueSelector from './components/ValueSelector.vue'
import TitleComponent from '../UI/components/TitleComponent.vue'
import PaymentBlock from '../UI/components/PaymentBlock.vue'
import BalanceBlock from '../UI/components/MoneyBlock.vue'
// import ExitButton from './components/ExitButton.vue'

export default {
  name: 'Barbershop',

  components: {
    SelectCategory,
    SelectColor,
    ValueSelector,
    TitleComponent,
    PaymentBlock,
    BalanceBlock,
    // ExitButton,
  },

  data: function() {
    return {
      cashtype: 'cash',
      currentCategory: { type: 'hairstyle', title: 'barber_5' },
      categories: [
        { type: 'hairstyle', title: 'barber_5' },
        { type: 'eyebrows', title: 'barber_6' },
        { type: 'beard', title: 'barber_13' },
        { type: 'torso', title: 'barber_7' },
        { type: 'lenses', title: 'barber_8' },
        { type: 'pomade', title: 'barber_9' },
        { type: 'blush', title: 'barber_10' },
        { type: 'shadows', title: 'barber_11' },
      ],
      value: {
        style: 0,
        color: 0,
      },
    }
  },

  computed: {
    ...mapState('barbershop', ['gender', 'price', 'config', 'playerMoney', 'playerBank']),
    ...mapGetters('localization', ['loc']),
    currentTypes: function() {
      return this.types.filter(
        (item) => item.parent === this.currentCategory.type
      )
    },

    currentColors: function() {
      return this.colors.filter(
        (item) => item.parent === this.currentCategory.type
      )
    },

    hairs() {
      return this.gender ? this.config.hairM : this.config.hairF
    },
    hairColors() {
      return this.config.hairColor
    },
  },

  methods: {
    getConfig(key) {
      return this.$store.getters['barbershop/config'](key)
    },
    getCash(){
      if(this.cashtype == 'cash') return this.playerMoney;
      else return this.playerBank;
    },
    setCurrentCategory: function(item) {
      this.currentCategory = item
    },
    change() {
      window.mp.trigger(
        'changeBarber',
        this.currentCategory.type,
        this.value.style,
        this.value.color
      )
    },
    onChangePay(type){
      this.cashtype = type
    },
    buy() {
      // var cashbool = false
      // if(this.cashtype == 'cash') cashbool = true;
      // else cashbool = false;
      window.mp.trigger(
        'buyBarber',
        this.currentCategory.type,
        this.value.style,
        this.value.color,
        this.cashtype == 'cash' ? true : false
      )
    },
  },
  watch: {},
}
</script>

<style lang="scss" scoped>
  
// @import url(./fonts/AtomicAge/AtomicAge-Regular.ttf);
@import url(../../styles/fonts/Inter/Inter.scss);
@import url(../../styles/fonts/Montserrat/Montserrat.scss);

.barbershop {
  background: linear-gradient(270deg, rgba(0, 0, 0, 0) 59.71%, #000000 81.74%, #000000 100%);
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: space-between;
  padding-left: 1.354167vw;
  padding-right: 2.96875vw;
  &::after {
    content: '';
    width: 14.323vw;
    height: 14.323vw;
    position: absolute;
    left: 92.135vw;
    top: 11.822vw;
    background: #301934 ;
    filter: blur(53.842vh)
  }
  &::before {
    content: '';
    position: absolute;
    z-index: -1;
    left: 0;
    width: 100%;
    height: 100%;
    transform: scale(1.2);
    background: radial-gradient(31.41% 50% at 59.69% 50%, rgba(0, 0, 0, 0) 0%, #000000 100%);
    opacity: 0.8;
  }
  .left-side {
    min-width: 30.925vh;
    margin-top: 1.75vh;
    .rectangle {
      width: 30.925vh;
    }
    .categories{
      margin-top: 1.851vh;
      margin-bottom: 2.407vh;
    }
    .payment-component {
      width: 30.925vh;
      margin-top: 4.907vh;
    }
    .exit {
      width: 30.925vh;
      margin-top: 2.5vh;
    }
  }
  .right-side {
    align-self: flex-start;
    margin-top: 3.98vh;
  }
}
</style>
