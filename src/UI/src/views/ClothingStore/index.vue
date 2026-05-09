<template>
  <div class="clothes-shop">
    <div class="content">
      <div class="clothes-header">
        <TitleComponent
          :titlePrimary="'Clothing'"
          :titleSecondary="'Store'"
          :about="'Fashion and quality at an optimal price!'"
        />
        <ExitButton @click="close" />
      </div>
      <SelectCategory
        :list="getCategories()"
        :currentIndex="currentCategoryIndex"
        :currentTitle="categoriesText[getCategories()[currentCategoryIndex].key]"
        @setCurrent="setCurrentCategory"
      />
      <div class="list">
        <Card
          v-for="(item, index) of getClothes()"
          :key="index"
          :title="getClothingName(item)"
          :price="getPrice(item)"
          :isActive="index === currentClothesIndex"
          :img="getCategories()[currentCategoryIndex].imgPath"
          :imgSize="0.5"
          @onSelect="setCurrentOutfit(index)"
        />
      </div>
      <PaymentBlock
        :layout="'vertical'"
        :price="getPrice(currentItem)"
        @onBuy="buy"
        @onChangePay="onChangePay"
      >
        <ValueSwitches
          :index="colorIndex"
          @input="setColor"
          :title="'Color'"
          :list="getClothes()[currentClothesIndex].Colors"
        />
        <MoneyBlock :balance = "getCash()" :showTitle="true" />
      </PaymentBlock>
    </div>
    <Hints v-if="false" />
  </div>
</template>

<script>
import PaymentBlock from '../UI/components/PaymentBlock.vue'
import MoneyBlock from '../UI/components/MoneyBlock.vue'
import ValueSwitches from '../UI/components/ValueSwitches.vue'
import SelectCategory from './SelectCategory.vue'
import { mapGetters, mapState } from 'vuex'
import Card from './Card.vue'
import BootPng from './assets/ico/boot.png'
import TitleComponent from '../UI/components/TitleComponent.vue'
import ExitButton from '../UI/components/ExitButton.vue'
import Hints from './Hints.vue'
import {categoriesText} from './content'

export default {
  name: 'ClothingStore',
  computed: {
    currentItem() {
      return this.getClothes()[this.currentClothesIndex]
    },
    ...mapState('clothingStore', [
      'types',
      'currentTypeId',
      'price',
      'currentColorId',
      'currentItemId',
      'gender',
      'configs',
      'playerMoney',
      'playerBank'
    ]),
    ...mapGetters('localization', ['loc']),
  },
  data() {
    return {
      cashtype: 'cash',
      currentCategoryIndex: 0,
      currentClothesIndex: 0,
      colorIndex: 0,
      BootPng,
      categoriesText
    }
  },
  methods: {
    getCategories() {
      return this.types.map((v) => ({
        key: v.key,
        imgPath: `img/clothingStore/icon_${v.key}.svg`,
      }))
    },
    getPrice(item){
      return (item.Price * this.price / 100).toFixed();
    },
    getCash(){
      if(this.cashtype == 'cash') return this.playerMoney;
      else return this.playerBank;
    },
    onChangePay(type){
      console.log(type)
      this.cashtype = type
    },
    getClothingName(item) {
      const variation =
        this.currentCategoryIndex == 2 ? item.Top : item.Variation
      return this.$store.getters['inventory/getClothingName']([
        this.gender,
        this.currentCategoryIndex,
        variation,
      ])
    },
    getClothes() {
      return Object.values(this.types[this.currentCategoryIndex].config)[
        this.gender
      ]
    },
    setCurrentCategory(index) {
      this.colorIndex = 0
      this.currentClothesIndex = 0
      this.currentCategoryIndex = index
      this.updateClothes()
    },
    setCurrentOutfit(index) {
      this.colorIndex = 0
      this.currentClothesIndex = index
      this.colorIndex = 0
      this.updateClothes()
    },
    setColor(value) {
      this.colorIndex = value
      this.updateClothes()
    },
    getCurrentColor() {
      return this.currentItem.Colors[this.colorIndex]
    },
    componentVariation(clotheId, variation, color) {
      window.mp.trigger('componentVariation', clotheId, variation, color)
    },
    propVariation(clotheId, variation, color) {
      window.mp.trigger('propVariation', clotheId, variation, color)
    },
    updateClothes() {
      let torso
      switch (this.currentCategoryIndex) {
        case 0:
          this.propVariation(
            0,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 1:
          this.componentVariation(
            11,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          torso = this.configs['validTorsos'][this.gender][
            this.currentItem.Variation
          ]
          if (torso) this.componentVariation(3, torso, 0)
          break
        case 2:
          this.componentVariation(
            11,
            this.currentItem.Top,
            this.getCurrentColor()
          )
          torso = this.configs['validTorsos'][this.gender][this.currentItem.Top]
          if (torso != undefined) this.componentVariation(3, torso, 0)
          break
        case 3:
          this.componentVariation(
            4,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 4:
          this.componentVariation(
            6,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 5:
          this.componentVariation(
            3,
            this.configs.correctGloves[this.gender][
              this.currentItem.Variation
            ][15],
            this.getCurrentColor()
          )
          break
        case 6:
          this.propVariation(
            6,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 7:
          this.propVariation(
            1,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 8:
          this.componentVariation(
            7,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
        case 9:
          this.componentVariation(
            5,
            this.currentItem.Variation,
            this.getCurrentColor()
          )
          break
      }
    },
    buy() {
      const variation =
        this.currentCategoryIndex == 2
          ? this.currentItem.Top
          : this.currentItem.Variation

      var cashbool = false
      if(this.cashtype == 'cash') cashbool = true;
      else cashbool = false;
      window.mp.trigger(
        'buyClothes',
        this.currentCategoryIndex,
        variation,
        this.getCurrentColor(),
        cashbool
      )
    },
    close() {
      window.mp.trigger('closeClothes')
    },
  },
  components: {
    PaymentBlock,
    MoneyBlock,
    ValueSwitches,
    SelectCategory,
    Card,
    TitleComponent,
    ExitButton,
    Hints,
  },
}
</script>

<style lang="scss">
.clothes-shop {
  color: aliceblue;
  width: 100vw;
  height: 100vh;
  background: url(./assets/ico/bg.png);
  background-repeat: no-repeat;
  background-size: cover;
  display: flex;
  align-items: center;
  .hints {
    position: absolute;
    right: 2.778vh;
    bottom: 5.093vh;
  }
  .content {
    margin-left: 2.685vh;
    width: min-content;
    .clothes-header {
      padding-right: 1.111vh;
      margin-bottom: 2.593vh;
      .exit {
        width: min-content;
        white-space: nowrap;
        margin-top: 1.852vh;
      }
      width: 100%;
      display: flex;
      justify-content: space-between;
    }
    .list {
      margin-top: 1.759vh;
      width: 51.0vh;
      padding-right: 1.111vh;
      height: 49.167vh;
      display: flex;
      flex-wrap: wrap;
      gap: 1.389vh;
      overflow-y: scroll;
      .card {
        width: 15.556vh;
        height: 15.463vh;
      }

      &::-webkit-scrollbar {
        width: 0.28vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.23);
      }
      &::-webkit-scrollbar-thumb {
        background: #ff0000;
      }
    }
    .payment-component {
      width: 49.259vh;
      margin-top: 2.222vh;
    }
    .slot {
      width: 42.222vh;
      margin-left: 2.037vh;
      .balance {
        margin-top: 0.556vh;
      }
    }
  }
}
</style>
