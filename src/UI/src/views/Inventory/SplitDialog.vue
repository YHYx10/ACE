<template>
  <!-- <div class="split" v-if="from && to && item">
        <div class="split-dialog">
            <div class="split-tittle">
                <div class="split-tittle-type">{{loc("inv_action_split")}}</div>
                <div class="split-tittle-name">{{loc(item.getDisplayName())}}</div>
            </div>
            <div class="split-body">
                <div class="split-info">
                    <div class="split-info-tittle">{{loc("inv_count_short")}}</div>
                    <input type="number" class="split-info-value" :min="1" :max="item.count - 1" v-model="count">
                </div>
                <Slider v-model="count" :min="1" :max="item.count - 1"/>
                <div class="split-buttons">
                    <div class="split-button" @click="move">{{loc("inv_split_confirm")}}</div>
                    <div class="split-button" @click="close">{{loc("inv_split_cancel")}}</div>
                </div>
            </div>
        </div>
    </div> -->
  <div class="input-menu" v-if="from && to && item">
    <div class="input-menu-wrap">
      <div class="input-menu__field">
        <span class="field__title">{{ loc('inv_action_split') }}</span>
        <span class="field__subtitle">{{ loc(item.getDisplayName()) }}</span>
      </div>
      <div class="split-info">
        <div class="title">{{ loc('inv_count_short') }}</div>
        <input
          type="number"
          class="value"
          :min="1"
          :max="item.count - 1"
          v-model="count"
        />
      </div>
      <Slider v-model="count" :min="1" :max="item.count - 1" />
      <div class="input-menu__buttons">
        <ModalBtn
          class="primary"
          @click="move"
          :button="{ text: 'inv_split_confirm' }"
        />
        <ModalBtn
          class=""
          @click="close"
          :button="{ text: 'inv_split_cancel' }"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Slider from 'vue-slider-component'
import { mapGetters } from 'vuex'
import ModalBtn from '../UI/button/ModalBtn.vue'

export default {
  props: ['from', 'to', 'item'],
  data() {
    return {
      count: 1,
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    close() {
      this.$emit('onSplitClose')
    },
    move() {
      const count = parseInt(this.count)
      if (isNaN(count)) return
      if (count > this.item.count || count < 1) return
      this.$emit('onSpliteMove', count)
    },
  },
  components: {
    Slider,
    ModalBtn,
  },
}
</script>

<style lang="scss">
.input-menu {

  //------------------------------------------
        //          SLIDER
        //------------------------------------------
        .vue-slider {
            margin: 1.5vh;
        }
        .vue-slider-disabled {
            opacity: 0.5;
            cursor: not-allowed;
        }

        /* rail style */
        .vue-slider-rail {
            min-width: 25rem;
            height: 1vh;
            margin: auto;
            background-color: rgba(#fff, .1);
            border-radius: 1vh;
            margin: 0vh auto;
        }

        /* process style */
        .vue-slider-process {
        background: linear-gradient(96.5deg, #301934  0%, #f3521d 100%);
        border-radius: 1.5vh;
        }

        /* mark style */
        .vue-slider-mark {
        z-index: 4;
        }
        .vue-slider-mark:first-child .vue-slider-mark-step, .vue-slider-mark:last-child .vue-slider-mark-step {
        display: none;
        }
        .vue-slider-mark-step {
        width: 100%;
        height: 100%;
        border-radius: 50%;
        background-color: rgba(0, 0, 0, 1);
        }
        .vue-slider-mark-label {
        font-size: 1.4vh;
        white-space: nowrap;
        }
        .vue-slider-dot {
            position: absolute;
        }
        /* dot style */
        .vue-slider-dot-handle {
        cursor: pointer;
        width: 2vh;
        height: 2vh;
        border-radius: 50%;
        transform: translate(-50%, -50%);
        margin-top: 0px;
        position: absolute;
        top: 50%;
        left: 50%;
        background: #fff;
        box-shadow: 0 0 0 1vh rgba(194, 194, 194, 0.3);
        box-sizing: border-box;
        }

        .vue-slider-dot-tooltip-inner {
            opacity: 0;
        }
        .vue-slider-dot-tooltip-inner-bottom::after {
        bottom: 100%;
        left: 50%;
        transform: translate(-50%, 0);
        height: 0;
        width: 0;
        border-color: transparent;
        border-style: solid;
        border-width: 0.4vh;
        border-bottom-color: inherit;
        }
        .vue-slider-dot-tooltip-inner-left::after {
        left: 100%;
        top: 50%;
        transform: translate(0, -50%);
        height: 0;
        width: 0;
        border-color: transparent;
        border-style: solid;
        border-width: 5px;
        border-left-color: inherit;
        }
        .vue-slider-dot-tooltip-inner-right::after {
        right: 100%;
        top: 50%;
        transform: translate(0, -50%);
        height: 0;
        width: 0;
        border-color: transparent;
        border-style: solid;
        border-width: 5px;
        border-right-color: inherit;
        }

        .vue-slider-dot-tooltip-wrapper {
        opacity: 0;
        transition: all 0.3s;
        }
        .vue-slider-dot-tooltip-wrapper-show {
        opacity: 1;
        }

        //-----------------------------------------------------
        //      slider
        //-----------------------------------------------------
    
  width: 100%;
  height: 100%;
  position: fixed;
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  z-index: 1100;

  &,
  div,
  span,
  button {
    font-family: 'Akrobat';
  }

  &-wrap {
    display: flex;
    align-items: center;
    flex-direction: column;
    padding: 4.579vh 6.526vh;
    position: relative;
    background: rgba(0, 0, 0, 0.95);

    &::after {
      content: '';
      position: absolute;
      width: 24.1053vh;
      height: 24.1053vh;
      left: 50%;
      bottom: 0;
      transform: translate(-50%, 0);
      background: rgba(255, 255, 255, 0.3);
      opacity: 0.25;
      filter: blur(111px);
      border-radius: 50%;
      z-index: 1;
    }

    div {
      z-index: 2;
    }
  }

  &__field {
    display: flex;
    flex-direction: column;
    align-items: center;

    span {
      display: block;
      text-transform: uppercase;
      color: #ffffff;
      text-align: center;
    }
  }

  .split-info {
    display: flex;
    align-items: center;
    justify-content: space-between;
    color: #fff;
    min-width: 100%;
    font-size: 0.9vh;
    line-height: 1.1vh;
    margin: 1vh 0;
    .title {
        font-weight: bold;
        font-size: 2vh;
    }
    .value {
      width: 5vh;
      text-align: center;
      background: none;
      border: 1px solid rgba(#fff, 0.1);
      color: #fff;
      padding: 0.9vh;
      font-weight: bold;
        font-size: 2vh;
      &::-webkit-inner-spin-button {
        display: none;
      }
    }
  }

  .field {
    &__close {
      display: flex;
      align-items: center;
      background: none;
      outline: none;
      border: none;
      font-weight: 700;
      font-size: 1.053vh;
      line-height: 1.263vh;
      color: #ffffff;
      cursor: pointer;
      margin-bottom: 1.579vh;
      &:hover {
        div {
          background: rgba(255, 255, 255, 0.15);
        }
      }

      div {
        width: 2.474vh;
        height: 2.474vh;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-right: 0.737vh;
        background: rgba(255, 255, 255, 0.07);

        img {
          height: 0.737vh;
          width: auto;
        }
      }
    }

    &__title {
      font-weight: 700;
      font-size: 4.105vh;
      line-height: 4.526vh;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: 1.526vh;
    }

    &__subtitle {
      font-weight: 700;
      font-size: 3vh;
      line-height: 3.526vh;
      text-align: center;
      text-transform: uppercase;
      color: #6e6e6e;
      margin-bottom: 1vh;
      max-width: 28vw;
    }
  }

  &__buttons {
    width: 100%;
    margin-top: 1.053vh;
    display: flex;
    gap: 1vh;
    min-width: 25vh;
    button {
        height: 6vh !important;
    }
  }
}

// .split {
//   //------------------------------------------
//   //          SLIDER
//   //------------------------------------------
//   .vue-slider-disabled {
//     opacity: 0.5;
//     cursor: not-allowed;
//   }

//   /* rail style */
//   .vue-slider-rail {
//     width: 16vh;
//     margin: auto;
//     background-color: rgba(#fff, 0.1);
//     border-radius: 15px;
//     margin: 1rem auto;
//   }

//   /* process style */
//   .vue-slider-process {
//     background: linear-gradient(96.5deg, #3a244c 0%, #8e52aa 100%);
//     border-radius: 15px;
//   }

//   /* mark style */
//   .vue-slider-mark {
//     z-index: 4;
//   }
//   .vue-slider-mark:first-child .vue-slider-mark-step,
//   .vue-slider-mark:last-child .vue-slider-mark-step {
//     display: none;
//   }
//   .vue-slider-mark-step {
//     width: 100%;
//     height: 100%;
//     border-radius: 50%;
//     background-color: rgba(0, 0, 0, 1);
//   }
//   .vue-slider-mark-label {
//     font-size: 14px;
//     white-space: nowrap;
//   }
//   /* dot style */
//   .vue-slider-dot-handle {
//     cursor: pointer;
//     width: 1.2vh;
//     height: 1.2vh;
//     border-radius: 50%;
//     margin-top: -0.2vh;
//     background: #7a3e96;
//     box-shadow: 0 0.5rem 1rem #8e52aa;
//     border: 0.2rem solid #8e52aa;
//     box-sizing: border-box;
//   }

//   .vue-slider-dot-tooltip-inner {
//     opacity: 0;
//   }
//   .vue-slider-dot-tooltip-inner-bottom::after {
//     bottom: 100%;
//     left: 50%;
//     transform: translate(-50%, 0);
//     height: 0;
//     width: 0;
//     border-color: transparent;
//     border-style: solid;
//     border-width: 5px;
//     border-bottom-color: inherit;
//   }
//   .vue-slider-dot-tooltip-inner-left::after {
//     left: 100%;
//     top: 50%;
//     transform: translate(0, -50%);
//     height: 0;
//     width: 0;
//     border-color: transparent;
//     border-style: solid;
//     border-width: 5px;
//     border-left-color: inherit;
//   }
//   .vue-slider-dot-tooltip-inner-right::after {
//     right: 100%;
//     top: 50%;
//     transform: translate(0, -50%);
//     height: 0;
//     width: 0;
//     border-color: transparent;
//     border-style: solid;
//     border-width: 5px;
//     border-right-color: inherit;
//   }

//   .vue-slider-dot-tooltip-wrapper {
//     opacity: 0;
//     transition: all 0.3s;
//   }
//   .vue-slider-dot-tooltip-wrapper-show {
//     opacity: 1;
//   }

//   //-----------------------------------------------------
//   //      slider
//   //-----------------------------------------------------

//   position: fixed;
//   left: 0;
//   top: 0;
//   width: 100vw;
//   height: 100vh;
//   z-index: 1100;
//   background-color: rgba(0, 0, 0, 0.5);
//   display: flex;
//   justify-content: center;
//   align-items: center;
//   &-dialog {
//     width: 19vh;
//     border: 2px solid rgba(#fff, 0.2);
//     box-shadow: 0px 1rem 3rem rgba(0, 13, 18, 0.5);
//   }
//   &-tittle {
//     height: 4vh;
//     background: url(/img/inventory/common/otherHead.png) no-repeat center;
//     background-size: cover;
//     padding-top: 1vh;
//     font-size: 1.1vh;
//     line-height: 1.1vh;
//     color: #fff;
//     &-type {
//       margin-left: 1.2vh;
//       font-size: 0.8vh;
//       line-height: 0.85vh;
//       color: rgba(#fff, 0.5);
//     }
//     &-name {
//       margin-left: 1.2vh;
//       margin-top: 0.2vh;
//       font-size: 1.1vh;
//       line-height: 1.1vh;
//       font-weight: 300;
//     }
//   }
//   &-info {
//     display: flex;
//     align-items: center;
//     justify-content: space-between;
//     color: #fff;
//     margin: auto;
//     width: 16vh;
//     font-size: 0.9vh;
//     line-height: 1.1vh;
//     &-value {
//       width: 3vh;
//       text-align: center;
//       background: none;
//       border: 1px solid rgba(#fff, 0.1);
//       color: #fff;
//       padding: 0.2vh;
//       &::-webkit-inner-spin-button {
//         display: none;
//       }
//     }
//   }
//   &-body {
//     padding: 1.2rem 0 0.1vh;
//     background-color: #142126;
//     text-align: center;
//   }
//   &-buttons {
//     margin-top: 2.5vh;
//     display: flex;
//     align-items: center;
//     justify-content: space-around;
//   }
//   &-button {
//     height: 3.5vh;
//     width: 49%;
//     color: rgba(#fff, 0.5);
//     background-color: #000d12;
//     display: flex;
//     justify-content: center;
//     align-items: center;
//     &:hover {
//       background-color: #8e52aa;
//       color: #fff;
//     }
//   }
// }
</style>
