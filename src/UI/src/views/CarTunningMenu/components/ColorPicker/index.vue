<template>
  <div class="color-picker">
    
    <div class="color-picker__main">
      <!-- Color type, example: primary, secondary -->
      <ValueSwitches 
        v-if="showPicker.colorType && showPicker.colorType.length > 1" 
        :list="[...showPicker.colorType.map(v => v.title)]" 
        :title="'Category'" 
        :index="colorCategoryIndex" 
        @input="selectCategory" 
      />
      <!-- Pallette Color Type, example: Metallic, Matte ...  -->
      <ValueSwitches 
        v-if="showPicker.showPallette && thirdTabData.parent !== 'Neon'" 
        :list="[...thirdTabData.colorTypeItems.map(v => loc(v.title))]" 
        :title="'Type of color '" 
        :index="colorTypeIndex"
        @input="selectColorType"  
      />
      <!-- Pallette color list  -->
      <ValueSwitches 
        v-if="showPicker.showPallette && thirdTabData.parent === 'WheelsColor'" 
        :list="[...thirdTabData.sliderColors.map(v => `#${v.Number}`)]" 
        :title="'Number'" 
        :index="colorNumberIndex"
        @input="selectColorNumber" 
      />
      <box
        v-show="showPicker.showPicker"
        name="RGB"
        :color="modelRgb"
        @inputColor="inputRgb"
      />
      <div v-show="showPicker.showPicker" class="color-set">
        <saturation
          ref="saturation"
          :color="rgbString"
          :hsv="hsv"
          :size="240"
          @selectSaturation="selectSaturation"
        />
        <hue
          ref="hue"
          :hsv="hsv"
          :width="hueWidth * appWidthUnit"
          :height="240"
          @selectHue="selectHue"
        />
      </div>
      
      <type-select v-show="false" v-if="type !== 'Neon'" :type="type" />
    </div>
  </div>
</template>

<script>
import { mapMutations, mapGetters, mapState } from 'vuex'
import mixin from './mixin'

import Saturation from './Saturation'
import Hue from './Hue'
import Box from './Box'
import TypeSelect from './Select'
import ValueSwitches from './ValueSwitches.vue'

export default {
  name: 'ColorPicker',

  components: {
    Saturation,
    Hue,
    Box,
    TypeSelect,
    ValueSwitches
},

  props: [
    'title',
    // 'type',
    'showPicker',
    'initialColor'
  ],

  mixins: [mixin],

  data: function() {
    return {
      isPicker: false,
      type: '',
      colorCategoryIndex: 0,
      colorTypeIndex: 0,
      colorNumberIndex: 0,
      hueWidth: 1,
      hueHeight: 9.8,
      saturationHeight: 10,
      modelRgb: '',
      r: 0,
      g: 0,
      b: 0,
      h: 0,
      s: 0,
      v: 0
    }
  },

  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('carTunningMenu', [
      'thirdTabData',
    ]),
    appWidthUnit: function () {
      const appWidth = document.getElementById('app').offsetWidth / 100
      const widthUnit = Math.ceil(appWidth)
      return widthUnit;
    },

    totalWidth: function() {
      return this.hueHeight + (this.hueWidth + 8) * 2
    },
    

    rgb: function() {
      return {
        r: this.r,
        g: this.g,
        b: this.b
      }
    },

    hsv: function() {
      return {
        h: this.h,
        s: this.s,
        v: this.v
      }
    },

    rgbString: function() {
      return `rgb(${this.r}, ${this.g}, ${this.b})`
    },

    rgbStringShort: function() {
      return `${this.r}, ${this.g}, ${this.b}`
    }
  },

  methods: {
    ...mapMutations('sounds', ['play']),
    toggleIsPicker: function() {
      this.isPicker = !this.isPicker
      window.mp.trigger('lsCustom:changeToggleIsPicker', this.isPicker, this.type);
    },

    setText: function() {
      this.modelRgb = this.rgbStringShort
    },

    selectCategory(index) {
      this.type = this.showPicker.colorType[index].type
      this.colorCategoryIndex = index
      const propKey = this.showPicker.colorType[index].initColorProp
      this.inputRgb(this.thirdTabData[propKey])
    },
    selectColorType(index) {
      const item = this.thirdTabData.colorTypeItems[index]
      window.mp.trigger('lsCustom:changePaintType', item.id, this.type);
      this.colorTypeIndex = index
    },
    selectColorNumber(index) {
      const value = this.thirdTabData.sliderColors[index].Number
      window.mp.trigger('lsCustom:changeColorSlider', value)
      this.colorNumberIndex = index
    },

    selectSaturation: function(color) {
      const { r, g, b, h, s, v } = this.setColorValue(color)

      Object.assign(this, { r, g, b, h, s, v })
      this.setText()
    },

    selectHue: function(color) {
      const { r, g, b, h, s, v } = this.setColorValue(color)

      Object.assign(this, { r, g, b, h, s, v })
      this.setText()
      this.$nextTick(() => {
        this.$refs.saturation.renderColor()
        this.$refs.saturation.renderSlide()
      })
    },

    inputRgb: function(color) {
      const { r, g, b, h, s, v } = this.setColorValue(color)

      Object.assign(this, { r, g, b, h, s, v })
      this.modelRgb = color
      this.$nextTick(() => {
        this.$refs.saturation.renderColor()
        this.$refs.saturation.renderSlide()
        this.$refs.hue.renderSlide()
      })
    },

    selectColor: function(color) {
      const { r, g, b, h, s, v } = this.setColorValue(color)

      Object.assign(this, { r, g, b, h, s, v })
      this.setText()
      this.$nextTick(() => {
        this.$refs.saturation.renderColor()
        this.$refs.saturation.renderSlide()
        this.$refs.hue.renderSlide()
      })
    }
  },

  created: function() {
    Object.assign(this, this.setColorValue(this.color))
    this.setText()
    const propKey = this.showPicker.colorType ? this.showPicker.colorType[0].initColorProp : this.initialColor
    this.inputRgb(this.thirdTabData[propKey])

    this.$watch('rgb', () => {
      this.$emit('changeColor', {
        rgb: this.rgb,
        hsv: this.hsv
        
      })
      window.mp.trigger('lsCustom:changeColor',  this.rgb.r, this.rgb.g, this.rgb.b, this.type);
    })
  },
  mounted(){
    this.type = this.showPicker.colorType ? this.showPicker.colorType[0].type : this.thirdTabData.parent
  },
  updated(){
    this.type = this.showPicker.colorType ? this.showPicker.colorType[this.colorCategoryIndex].type : this.thirdTabData.parent 
  }
}
</script>

<style lang="scss" scoped>
.color-picker {
  z-index: 99;
  display: flex;
  align-self: flex-end;
  padding-top: 10px;
  flex-direction: column;
  // box-shadow: 0 0 0 2px red;
  align-items: center;
  margin: 0 1.5rem 0 0;
  // width: 11.7rem;
  // width: 285px;
  // height: 419px;
  background: rgba(7, 7, 7, 0.6);
  &__header {
    display: flex;
    align-items: center;
    margin: 0 0 0.85rem 0;
    .title {
      font-size: 1.05rem;
      line-height: 1.25rem;
      color: #fff;
    }
    .btn {
      background: transparent;
      &.active {
        &:after {
          transform: rotate(0);
        }
      }
      &:after {
        content: "";
        width: 1.4rem;
        height: 0.95rem;
        background: center / contain no-repeat url("/img/carTunningMenu/chevronUp.svg");
        display: block;
        margin: 0 0.8rem 0 0;
        transform: rotate(180deg);
      }
    }
  }
  &__main {
    // box-shadow: 0 0 0 1px red;
    min-width: 20vh;
    .value-switches {
      margin-bottom: 10px;
    }
    .color-set {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 10px;
    }
  }
}
</style>
