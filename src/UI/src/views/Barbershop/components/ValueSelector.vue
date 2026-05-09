<template>
  <div class="rectangle">
    <div class="wrap">
      <button @click="slide(-1)">
        <svg fill="none" viewBox="0 0 10 18">
          <path stroke="inherit" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 1 1 9l8 8"/>
        </svg>
      </button>
      <div>
        <div class="title">
          {{ currentTitle }}
        </div>
        <div class="value">
          {{ value.style }}
        </div>
      </div>
      <button @click="slide(1)">
        <svg style="transform: scale(-1)" fill="none" viewBox="0 0 10 18">
          <path stroke="inherit" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 1 1 9l8 8"/>
        </svg>
      </button>
    </div>
    
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'ValueSelector',

  props: ['currentTitle', 'currentTypes', 'value'],

  data: function() {
    return {
      direction: 1,
      transitionName: '',
    }
  },
  watch: {
    currentTypes() {
      this.value.style = 0
      this.value.color = 0
    },
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    slide(dir) {
      this.value.style += dir
      if (this.currentTypes.hair) {
        if (this.value.style == this.currentTypes.hair + 1)
          this.value.style = 500
        else if (this.value.style == 499)
          this.value.style = this.currentTypes.hair
      }

      if (
        this.currentTypes.exclude > 0 &&
        this.currentTypes.exclude.indexOf(this.value.style) !== -1
      ) {
        this.slide(dir)
        return
      }

      if (this.value.style > this.currentTypes.max)
        this.value.style = this.currentTypes.min
      else if (this.value.style < this.currentTypes.min)
        this.value.style = this.currentTypes.max
      this.$emit('onChange')
    },
  },
}
</script>

<style lang="scss" scoped>
.rectangle {
  width: 100%;
  height: 6.11vh;
  display: flex;
  align-items: center;
  position: relative;
  border-radius: 0.260417vw;
  background: radial-gradient(
    96.54% 3386.79% at 100% 50%,
    rgba(0, 0, 0, 0) 0%,
    rgba(35, 35, 35, 0.6) 100%
  );

  &::before {
    content: '';
    position: absolute;
    inset: 0;
    border-radius: 0.46vh;
    border: 0.104vw solid transparent;
    background: linear-gradient(90deg, rgba(0, 0, 0, 0.35), rgba(14, 15, 20, 0))
      border-box;
    -webkit-mask-composite: xor;
    mask-composite: exclude;
    -webkit-mask: linear-gradient(rgb(255, 255, 255) 0 0) padding-box,
      linear-gradient(#fff 0 0);
  }

  .wrap{
    display: flex;
    width: 100%;
    position: relative;
    justify-content: space-between;
    align-items: center;

    div {
      text-align: center;
      font-family: 'Montserrat';
      font-style: normal;
      color: #FFFFFF;
      display: flex;
      flex-direction: column;
      justify-content: center;
      gap: 0.74vh;
      .title {
        font-weight: 500;
        font-size: 1.3vh;
        line-height: 1.57vh;
      }
      .value {
        font-weight: 700;
        font-size: 1.67vh;
        line-height: 2.037vh;
      }
    } 

    button {
      width: 3.70vh;
      height: 3.70vh;
      display: flex;
      justify-content: center;
      align-items: center;
      background: transparent;
      outline: 0.052vw solid #fff;
      border-radius: 0.313vw;
      outline-offset: -0.1vw;
      svg{
        width: 0.92vh;
        stroke: #fff;
      }
      &:hover{
        background: #fff;
        svg{
          stroke: #000;
        }
      }
      &:nth-child(1){
        margin-left: 0.78125vw;
      }
      &:nth-child(2){
        margin-right: 0.78125vw;
      }
    }
  }
}
</style>
