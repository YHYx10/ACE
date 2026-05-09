<template>
  <div class="mask-shop">
    <div class="content">
      <ExitButton @click="close" shortStyle class="exit" /> 
      <TitleComponent
        :titlePrimary="'Mask'"
        :titleSecondary="'Store'"
        :about="'Masks are sold in this shop.'"
      />
      <MoneyBlock class="balance" :showTitle="true" :balance = "getCash()" />
      <SelectColor
        class="select-color"
        :img="`/img/maskShop/${configs[currentMaskIndex].Variation}.png`"
        :list="configs[currentMaskIndex].Colors"
        :value="colorIndex"
        @input="selectColor"
      />
      <div class="footer">
        <SelectMask :list="configs" :value="currentMaskIndex" @input="select" />
        <PaymentBlock @onBuy="buy" @onChangePay="onChangePay" class="payment-block" :price="getPrice(currentMaskIndex)" :layout="'vertical'">
          <div class="hints">
            <div class="hint">
              <img src="./assets/img/mouseLeft.png" :alt="'item.img'" />
              <div class="about">
                <div class="title">Scroll the character</div>
                <div class="subtitle">Mouse button on the left button</div>
              </div>
            </div>
            <div class="hint">
              <img src="./assets/img/mouseWheel.png" :alt="'item.img'" />
              <div class="about">
                <div class="title">Close / go away</div>
                <div class="subtitle">Mousewheel</div>
              </div>
            </div>
          </div>
        </PaymentBlock>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import TitleComponent from '../UI/components/TitleComponent.vue'
import MoneyBlock from '../UI/components/MoneyBlock.vue'
import SelectColor from './SelectColor.vue'
import SelectMask from './SelectMask.vue'
import PaymentBlock from '../UI/components/PaymentBlock.vue'
import ExitButton from '../UI/components/ExitButton.vue'

export default {
  name: 'MaskShop',
  data() {
    return {
      cashtype: 'cash',
      currentMaskIndex: 0,
      colorIndex: 0,
    }
  },
  computed: {
    ...mapState('maskShop', ['price', 'configs', 'playerMoney', 'playerBank']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    getPrice(index){
      return (this.configs[index].Price  *  +this.price / 100).toFixed();
    },
    select(index) {
      this.currentMaskIndex = index
      this.colorIndex = 0
      window.mp.trigger('setMask', this.configs[this.currentMaskIndex].Variation, this.colorIndex)
    },
    getCash(){
      if(this.cashtype == 'cash') return this.playerMoney;
      else return this.playerBank;
    },
    onChangePay(type){
      console.log(type)
      this.cashtype = type
    },
    close() {
      window.mp.trigger('closeMasks')
    },
    selectColor(color) {
      this.colorIndex = color
      window.mp.trigger('setMask', this.configs[this.currentMaskIndex].Variation, this.colorIndex)
    },
    buy() {
      var cashbool = false
      if(this.cashtype == 'cash') cashbool = true;
      else cashbool = false;
      window.mp.trigger('buyMasks', this.configs[this.currentMaskIndex].Variation, this.colorIndex, cashbool)
    },
  },
  components: {
    TitleComponent,
    MoneyBlock,
    SelectColor,
    SelectMask,
    PaymentBlock,
    ExitButton
},
}
</script>

<style lang="scss" scoped>
.mask-shop {
  color: #ffffff;
  width: 100vw;
  height: 100vh;
  background: radial-gradient(36.88% 61.71% at 48.39% 38.29%, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.8)100%);
  // background-image: url(./assets/img/bg.png);
  background-repeat: no-repeat;
  background-size: cover;
  display: flex;
  align-items: center;
  &::before {
    content: '';
    top: 0;
    left: 0;
    position: absolute;
    background: linear-gradient(90deg, rgba(3,4,6,1) 20%, rgba(3,4,6, .4) 80%, rgba(255,255,255,0) 100%);
    z-index: -1;
    width: 20vw;
    height: 100vh;
  }
  &::after {
    content: '';
    bottom: 0;
    left: 0;
    position: absolute;
    background: linear-gradient(0deg, rgba(3,4,6,1) 20%, rgba(3,4,6, .4) 80%, rgba(255,255,255,0) 100%);
    z-index: -1;
    width: 100vw;
    height: 28vh;
  }
  .content {
    width: 100%;
    margin-left: 3.796vh;
    margin-right: 1.204vh;
    .exit {
      position: absolute;
      right: 4.63vh;
      top: 4.63vh;
    }
    .select-color {
      height: 50.833vh;
      margin-bottom: -1.019vh;
    }
    .balance {
      margin-top: 5.648vh;
      margin-bottom: 5.093vh;
    }
    .payment-block {
      display: flex;
      flex-direction: column;
      width: 20.352vw;
      margin-top: 0;
      justify-content: space-between;
      .hints {
        margin-left: 1.296vh;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: space-around;
      }
      .hint {
        display: flex;
        img {
          width: 3.148vh;
          height: 3.148vh;
        }
        .about {
          display: flex;
          flex-direction: column;
          justify-content: space-around;
          align-items: flex-start;

          font-style: normal;
          font-weight: 600;
          font-size: 1.667vh;
          line-height: 2.037vh;
          margin-left: 1.296vh;
          .title {
            font-family: "Montserrat";
            display: flex;
            align-items: center;
            color: #ffffff;
          }
          .subtitle {
            font-family: 'Montserrat';
            font-weight: 300;
            font-size: 1.296vh;
            line-height: 1.574vh;
            color: rgba(255, 255, 255, 0.37);
            display: flex;
            align-items: center;
          }
        }
      }
    }
    .footer {
      position: relative;
      justify-content: space-between;
      display: flex;
      gap: 0.5vw;
    }
  }
}
</style>
