<template>
  <div class="ammo-amount">
    <div class="header">
      <input
        v-model.number="count"
        @keypress="check($event)"
        type="number"
        class="rectangle"
      />
      <div class="title">Indicate the number of cartridges</div>
    </div>
    <Slider v-model="count" :min="1" :max="1000" />
  </div>
</template>

<script>
import Slider from 'vue-slider-component'
import { mapGetters } from 'vuex'

export default {
  data() {
    return {
      count: 1,
    }
  },
  watch: {
    count(newVal, oldVal) {
      if (!newVal) {
        this.count = 1
        return
      }
      this.count = newVal < 1 || newVal > 1000 ? oldVal : newVal
      this.$emit('onChange', this.count)
    },
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    check(e) {
      if (e.which < 48 || e.which > 57) e.preventDefault()
    },
  },
  components: {
    Slider,
  },
}
</script>

<style lang="scss">
.ammo-amount {
  width: 31.944vh;
  height: 14.074vh;
  padding: 2.037vh 3.333vh;
  display: flex;
  flex-direction: column;
  gap: 2.222vh;
  position: relative;
  background: url('./assets/img/ammoBg.png');
  background-repeat: no-repeat;
  background-size: cover;
  
  .header {
    display: flex;
    text-transform: uppercase;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    gap: 1.574vh;
    .title {
      font-size: 1.852vh;
      line-height: 2.222vh;
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: #ffffff;
    }
    .rectangle {
      width: 6.019vh;
      height: 6.019vh;
      background: rgba(217, 217, 217, 0.05);
      text-align: center;
      font-size: 2.222vh;
      line-height: 2.685vh;
      display: flex;
      align-items: center;
      text-align: center;
      color: rgba(255, 255, 255, 0.3);
      &::-webkit-outer-spin-button,
      &::-webkit-inner-spin-button {
        -webkit-appearance: none;
        margin: 0;
      }
    }
  }
}
//------------------------------------------
//          SLIDER
//------------------------------------------
.vue-slider-disabled {
  opacity: 0.5;
  cursor: not-allowed;
}
.vue-slider {
  height: 2.315vh;
}

/* rail style */
.vue-slider-rail {
  width: 100%;
  padding: 0.463vh;
  margin: auto;
  background: rgba(255, 255, 255, 0.05);
}

/* process style */
.vue-slider-process {
  background: #ee0f19;
}

/* mark style */
.vue-slider-mark {
  z-index: 4;
}
.vue-slider-mark:first-child .vue-slider-mark-step,
.vue-slider-mark:last-child .vue-slider-mark-step {
  display: none;
}
.vue-slider-mark-step {
  width: 100%;
  height: 100%;
  border-radius: 50%;
  background-color: rgba(0, 0, 0, 1);
}
.vue-slider-mark-label {
  font-size: 1.296vh;
  display: none;
  white-space: nowrap;
}
/* dot style */
.vue-slider-dot-handle {
  cursor: pointer;
  width: 1.62vh;
  height: 1.62vh;
  border-radius: 50%;
  margin-top: -0.185vh;
  background: #ffffff;
  border: 0.185vh solid #ffffff;
  box-shadow: 0vh 0vh 0 0.833vh rgba(0, 0, 0, 0.5);
  box-sizing: border-box;
  position: relative;
  &::before {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    box-sizing: border-box;
    box-shadow: 0vh 0.463vh 0.926vh 0.741vh rgba(255, 255, 255, 0.6);
  }
}
.vue-slider-dot-tooltip {
  display: none;
}
</style>
