<template>
  <div class="car-tunning">
    <div class="content">
      <div class="header">
        <div class="left">
          <TitleComponent
            :titlePrimary="'Astro'"
            :titleSecondary="'Customs'"
            together
          />
          <MoneyBlock :balance="money" showTitle />
        </div>
        <div class="right">
          <button @click="toggleSound(randomBgSound)" class="volume">
            <img
              v-if="!isBgSoundPlaying"
              class="volume__icon"
              src="/img/carTunningMenu/mute.svg"
              alt="mute"
            />
            <img
              v-else-if="isBgSoundPlaying"
              class="volume__icon"
              src="/img/carTunningMenu/volume.svg"
              alt="mute"
            />
          </button>
          <button class="exit" @click="exitTunning">
            <img
              class="exit__icon"
              src="/img/carTunningMenu/close.svg"
              alt="close"
              @click="exitTunning()"
              @mouseenter="play({ name: 'hover', volume: 0.4 })"
            />
          </button>
        </div>
      </div>
      <div class="main">
        <div class="left-side">
          <Nav
            :list="navList"
            :selected="selectedKey"
            @select="select"
            @back="navBack"
            :isFirstNesting="currentTab === 'FirstTab'"
          />
          <color-picker
            v-if="secondTabData.parent === 'painting' && showPicker && selectedKey !== -1"
            :showPicker="showPicker"
            :title="loc('car_tunning_4')"
            :initialColor="thirdTabData.colorPickerMainColor"
          />
        </div>
        <div class="right-side">
          <Specification />
          <!-- <PaymentBlock class="payment-block" :price="7686" layout="" isPriceTop /> -->
          <PaymentBlock
            @onBuy="buy"
            v-if="isBuyEnable && selectedKey !== -1"
            :price="currentItemPrice"
          />
        </div>
        <!-- <ColorPalette :title="'SDDA'" /> -->
      </div>
      <!-- {{navList}} -->
      <ItemsList
        v-if="currentTab === 'ThirdTab'"
        :list="thirdTabData.items"
        @onSelect="setCurrentSelectItem"
      />
      <ItemsList
        v-if="currentTab === 'FourthTab'"
        :list="fourthTabData.items"
        @onSelect="setCurrentSelectItem"
      />
    </div>

    <component v-if="false" :is="currentTab" />
  </div>
</template>

<script>
import { mapState, mapMutations, mapGetters } from 'vuex'

import FirstTab from './components/FirstTab'
import SecondTab from './components/SecondTab'
import ThirdTab from './components/ThirdTab'
import FourthTab from './components/FourthTab'
import MoneyBlock from '../UI/components/MoneyBlock.vue'
import TitleComponent from '../UI/components/TitleComponent.vue'
import Specification from './components/Specification'
import ItemsList from './components/ItemsList'
import Nav from './components/Nav'
import PaymentBlock from './components/PaymentBlock.vue'
import colorPicker from './components/ColorPicker'
// import ColorPalette from './components/ThirdTab/ColorPalette.vue'

export default {
  name: 'CarTunningMenu',

  components: {
    FirstTab,
    SecondTab,
    ThirdTab,
    FourthTab,
    MoneyBlock,
    TitleComponent,
    Specification,
    Nav,
    PaymentBlock,
    ItemsList,
    colorPicker,
    // ColorPalette
  },

  data: function() {
    return {
      isBgSoundPlaying: true,
      // currentTab: 'fistTabItems',
      selectedKey: -1,
    }
  },

  computed: {
    ...mapState('carTunningMenu', [
      'fistTabItems',
      'secondTabData',
      'thirdTabData',
      'isBuyEnable',
      'fourthTabData',
      'currentTab',
      'currentItemPrice',
    ]),
    ...mapState('hud', ['money']),
    ...mapGetters('localization', ['loc']),
    navList() {
      return this.currentTab === 'FirstTab'
        ? this.fistTabItems
        : this.secondTabData.items
    },

    randomBgSound: function() {
      const items = [
        'tunningBg1',
        'tunningBg2',
        'tunningBg3',
        'tunningBg4',
        'tunningBg5',
        'tunningBg6',
        'tunningBg7',
        'tunningBg8',
        'tunningBg9',
        'tunningBg10',
        'tunningBg11',
        'tunningBg12',
        'tunningBg13',
      ]
      return items[Math.floor(Math.random() * items.length)]
    },
    showPicker(){
      switch (this.thirdTabData.parent) {
        case 'Color':
          return {
            colorType: [
              { title: "Basic", type: "Color", initColorProp: 'colorPickerMainColor'},
              { title: "Additional", type: "SecColor", initColorProp: 'colorPickerAdditionalColor'},
            ],
            showPicker: true,
            showPallette: true,
            initColorProp: 'colorPickerTyreSmokeColor'
          };
        case 'Neon':
          return {
            colorType: [
              { title: "Standard", type: "Neon", initColorProp: 'colorPickerNeonColor'},
              { title: "Additional", type: "Neon2", initColorProp: 'colorPickerNeon2Color'}
            ],
            showPicker: true,
            showPallette: true,
          };
        case 'TyreSmokeColor':
          return {
            colorType: [
              { title: "Standard", type: "TyreSmokeColor", initColorProp: 'colorPickerTyreSmokeColor'}
            ],
            showPicker: true,
            showPallette: false
            
          };
        
        case 'WheelsColor':
          return {
            colorType: null,
            showPicker: false,
            showPallette: true,
          }
      
        default:
          return null;
      }
    },
  },

  methods: {
    ...mapMutations('carTunningMenu', [
      'setCurrentTab',
      'setCurrentSelectItem',
    ]),
    ...mapMutations('sounds', ['play', 'stop']),

    select(item, key) {
      if (this.currentTab === 'FirstTab') {
        // this.currentTab = 
        this.setCurrentTab('SecondTab')
        window.mp.trigger('lsCustom:openListCat', item.key)
      } else if (this.currentTab === 'SecondTab') {
        this.selectedKey = key
        this.setCurrentTab('ThirdTab')
        window.mp.trigger('lsCustom:openListTun', item.key);
        // if (parent === 'FrontWheels' || parent === 'BackWheels') {
        //   this.setCurrentTab(this.currentTab)
        //   window.mp.trigger('lsCustom:chooseWheelType', key)
        // } else {
        //   window.mp.trigger('lsCustom:clickTun', parent, key)
        // }
      } else if (this.currentTab === 'ThirdTab'){
        this.setCurrentTab('SecondTab')
        window.mp.trigger('lsCustom:backPage', 'SecondTab');
        this.selectedKey = key
        this.setCurrentTab('ThirdTab')
        window.mp.trigger('lsCustom:openListTun', item.key);
      } 
    },
    navBack() {
      if (this.currentTab === 'ThirdTab') {
        this.selectedKey = -1
        this.setCurrentTab('SecondTab')
        window.mp.trigger('lsCustom:backPage', 'SecondTab');
      } else if (this.currentTab === 'FourthTab'){
        this.selectedKey = -1
        this.setCurrentTab('ThirdTab')
        window.mp.trigger('lsCustom:backPage', 'ThirdTab');
      }
      else {
        this.selectedKey = -1
        // this.currentTab = 
        this.setCurrentTab('FirstTab')
        window.mp.trigger('lsCustom:backPage', 'FirstTab');
      }
      
    },
    buy: function() {
      window.mp.trigger('lsCustom:buyTuning')
    },

    exitTunning: function() {
      window.mp.trigger('lsCustom:exitTun')
    },

    toggleSound: function(sound) {
      if (this.isBgSoundPlaying) {
        this.stop(sound)
        this.isBgSoundPlaying = false
      } else {
        this.play({ name: sound, volume: 0.04, loop: true })
        this.isBgSoundPlaying = true
      }
    },
  },

  mounted: function() {
    this.play({ name: this.randomBgSound, volume: 0.04, loop: true })
  },

  beforeDestroy: function() {
    this.stop(this.randomBgSound)
    this.isBgSoundPlaying = false
  },
}
</script>

<style lang="scss" scoped>
.car-tunning {
  width: 100%;
  height: 100%;
  position: relative;
  text-transform: uppercase;
  background: linear-gradient(90deg, rgba(0,0,0,0.95) 0%, rgba(0,0,0,0.5) 30%, rgba(254,163,181,0) 40%, rgba(254,163,181,0) 70%, rgba(0,0,0,0.5) 80%, rgba(0,0,0,0.95) 100%);
  .content {
    margin: 0 auto;
    width: calc(100% - 4vw);
    margin: 0 2vw;
  }
  .header {
    display: flex;
    justify-content: space-between;
    margin: 3.889vh 0vh 0 0vh;
    .left {
      display: flex;
      flex-direction: column;
      gap: 2.222vh;
    }
    .right {
      display: flex;
      height: min-content;
      gap: 1.852vh;
      button {
        background: transparent;
        border: none;
        outline: none;
      }
    }
  }

  .main {
    display: flex;
    justify-content: space-between;

    .left-side {
      margin-top: 5.556vh;
      display: flex;
      gap: 0.926vh;
    }
    .right-side {
      display: flex;
      flex-direction: column;
      justify-content: flex-end;
      align-items: flex-end;
      margin-top: -5.278vh;
      .payment-block {
        margin-top: 2.778vh;
      }
    }
  }

  .items-list {
    margin-top: 2.778vh;
  }

  &__actions {
    position: absolute;
    top: 2.5rem;
    right: 1.45rem;
    z-index: 99;
    display: flex;
    align-items: center;
    .volume {
      width: 2.3rem;
      height: 2.3rem;
      border-radius: 50%;
      display: flex;
      align-items: center;
      justify-content: center;
      background: transparent;
      margin: 0 1.7rem 0 0;
      &:hover {
        background: rgba($color: #000000, $alpha: 0.3);
      }
      &__icon {
        width: 90%;
        height: 90%;
      }
    }
    .exit {
      width: 2.5rem;
      height: 2.5rem;
      background: transparent;
      border-radius: 50%;
      &:hover {
        background: rgba($color: #000000, $alpha: 0.3);
      }
      &__icon {
        width: 100%;
        height: 100%;
      }
    }
  }
}
</style>
